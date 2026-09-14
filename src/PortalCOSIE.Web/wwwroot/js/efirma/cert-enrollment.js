/**
 * Genera llave privada cifrada, CSR y solicita emisión del certificado al servidor.
 */
async function enrolarCertificado({ contrasena, url, cn }) {
    if (!window.forge) {
        throw new Error('No se cargó la librería de criptografía.');
    }

    const keys = forge.pki.rsa.generateKeyPair(2048);
    const csr = forge.pki.createCertificationRequest();
    csr.publicKey = keys.publicKey;
    csr.setSubject([{ name: 'commonName', value: cn }]);
    csr.sign(keys.privateKey, forge.md.sha256.create());

    const csrDer = forge.asn1.toDer(forge.pki.certificationRequestToAsn1(csr)).getBytes();
    const csrBase64 = forge.util.encode64(csrDer);

    await executeFetch(url, 'POST', { csrBase64 });
    descargarLlavePrivada(keys.privateKey, contrasena, cn);
}

async function enrolarCertificadoAlumno(opciones) {
    return enrolarCertificado({ ...opciones, cn: 'PortalCOSIE Alumno' });
}

function descargarLlavePrivada(privateKey, contrasena, prefijo = 'portalcosie') {
    const privateKeyInfo = forge.pki.wrapRsaPrivateKey(forge.pki.privateKeyToAsn1(privateKey));
    const encrypted = forge.pki.encryptPrivateKeyInfo(privateKeyInfo, contrasena, {
        algorithm: 'aes256'
    });
    const pem = forge.pki.encryptedPrivateKeyToPem(encrypted);
    const blob = new Blob([pem], { type: 'application/x-pem-file' });
    const enlace = document.createElement('a');
    enlace.href = URL.createObjectURL(blob);
    const slug = prefijo.toLowerCase().replace(/\s+/g, '-');
    enlace.download = `${slug}-${new Date().toISOString().slice(0, 10)}.key`;
    enlace.click();
    URL.revokeObjectURL(enlace.href);
}