/**
 * Carga el certificado público (desde el servidor) y la llave privada .key en el navegador.
 */
async function leerArchivoComoArrayBuffer(archivo) {
    return await archivo.arrayBuffer();
}

async function leerArchivoComoTexto(archivo) {
    return await archivo.text();
}

function asegurarLlaveRsaFirmante(llavePrivada) {
    if (!llavePrivada || typeof llavePrivada.sign !== 'function') {
        throw new Error('La llave privada no es compatible con firma RSA.');
    }
    return llavePrivada;
}

function desencriptarLlaveDer(bytes, password) {
    const buffer = forge.util.createBuffer(new Uint8Array(bytes));
    const asn1 = forge.asn1.fromDer(buffer);
    const decryptedInfo = forge.pki.decryptPrivateKeyInfo(asn1, password);
    return asegurarLlaveRsaFirmante(forge.pki.privateKeyFromAsn1(decryptedInfo));
}

function desencriptarLlavePem(pem, password) {
    const llavePrivada = forge.pki.decryptRsaPrivateKey(pem, password);
    if (!llavePrivada) {
        throw new Error('No se pudo desencriptar la llave privada. Verifica el archivo .key y la contraseña.');
    }
    return asegurarLlaveRsaFirmante(llavePrivada);
}

function cargarCertificadoDesdeDerBase64(base64) {
    if (!base64) {
        throw new Error('No tienes un certificado registrado en el sistema.');
    }

    try {
        const bytes = forge.util.decode64(base64);
        if (!bytes || bytes.length < 300) {
            throw new Error('El certificado registrado está incompleto. El administrador debe cargar el archivo .cer real en la base de datos.');
        }

        const buffer = forge.util.createBuffer(bytes);
        const asn1 = forge.asn1.fromDer(buffer);
        return forge.pki.certificateFromAsn1(asn1);
    } catch (error) {
        if (error.message?.includes('incompleto')) {
            throw error;
        }
        throw new Error('El certificado registrado no es válido. Verifica que el administrador haya cargado tu archivo .cer correctamente.');
    }
}

async function cargarLlavePrivadaDesdeArchivo(archivoKey, password) {
    const texto = await leerArchivoComoTexto(archivoKey);

    try {
        if (texto.includes('BEGIN')) {
            return desencriptarLlavePem(texto, password);
        }

        const bytes = await leerArchivoComoArrayBuffer(archivoKey);
        return desencriptarLlaveDer(bytes, password);
    } catch (error) {
        if (error.message?.includes('compatible con firma RSA')) {
            throw error;
        }
        throw new Error('No se pudo desencriptar la llave privada. Verifica el archivo .key y la contraseña.');
    }
}

async function cargarMaterialFirma(llaveKey, password, certificadoDerBase64) {
    if (!llaveKey || !password) {
        throw new Error('Llave privada y contraseña son obligatorios para firmar.');
    }

    const certificado = cargarCertificadoDesdeDerBase64(certificadoDerBase64);
    const llavePrivada = await cargarLlavePrivadaDesdeArchivo(llaveKey, password);
    return { certificado, llavePrivada };
}