/**
 * Firma los PDFs en el navegador y arma el FormData para enviar al servidor.
 * El certificado público se obtiene por GET desde el servidor (registrado en BD).
 */
let certificadoDerBase64Cache = null;

async function obtenerCertificadoDerBase64() {
    if (certificadoDerBase64Cache) {
        return certificadoDerBase64Cache;
    }

    const url = window.efirmaCertificadoUrl;
    if (!url) {
        throw new Error('No se configuró la obtención del certificado de firma.');
    }

    const response = await executeFetch(url, 'GET');
    const base64 = response.data?.certificadoDerBase64;
    if (!base64) {
        throw new Error('No se pudo obtener el certificado registrado.');
    }

    certificadoDerBase64Cache = base64;
    return certificadoDerBase64Cache;
}

async function prepararEnvioConFirmas(form, documentos) {
    const llaveKey = form.querySelector('[name="LlaveKey"]')?.files?.[0];
    const password = form.querySelector('[name="PasswordKey"]')?.value ?? '';
    const certificadoDerBase64 = await obtenerCertificadoDerBase64();

    const material = await cargarMaterialFirma(llaveKey, password, certificadoDerBase64);
    const formData = new FormData(form);
    formData.delete('LlaveKey');
    formData.delete('PasswordKey');

    for (const documento of documentos) {
        const input = form.querySelector(`[name="${documento.campoArchivo}"]`);
        const archivo = input?.files?.[0];

        if (!archivo) {
            if (documento.opcional) {
                formData.delete(documento.campoArchivo);
                formData.delete(documento.campoFirma);
                continue;
            }
            throw new Error(`El archivo ${documento.etiqueta} es obligatorio.`);
        }

        const firmaBase64 = await firmarArchivoCmsConMaterial(archivo, material);
        formData.set(documento.campoArchivo, archivo, archivo.name);
        formData.set(documento.campoFirma, firmaBase64);
    }

    return formData;
}

async function enviarFormularioFirmado(form, documentos, opciones = {}) {
    const {
        boton,
        url = form.getAttribute('action'),
        redirectUrl,
        onSuccess,
        onError
    } = opciones;

    if (boton) {
        boton.bloquear();
    }

    try {
        const formData = await prepararEnvioConFirmas(form, documentos);
        const data = await executeFetch(url, 'POST', formData);

        if (onSuccess) {
            onSuccess(data);
            return;
        }

        showGlobalModal('success', data.message, { redirectUrl });
    } catch (error) {
        if (onError) {
            onError(error);
            return;
        }

        showGlobalModal('error', error.message, {
            onClose: () => boton?.restaurar()
        });
    }
}

const documentosSolicitudCtce = [
    { campoArchivo: 'Identificacion', campoFirma: 'FirmaIdentificacion', etiqueta: 'Identificación' },
    { campoArchivo: 'BoletaGlobal', campoFirma: 'FirmaBoletaGlobal', etiqueta: 'Boleta global' },
    { campoArchivo: 'CartaExposicionMotivos', campoFirma: 'FirmaCartaExposicionMotivos', etiqueta: 'Carta de motivos' },
    { campoArchivo: 'Probatorios', campoFirma: 'FirmaProbatorios', etiqueta: 'Probatorios' }
];

const documentosCorreccionCtce = documentosSolicitudCtce.map((documento) => ({
    ...documento,
    opcional: true
}));

const documentosConclusionCtce = [
    { campoArchivo: 'Acuse', campoFirma: 'FirmaAcuse', etiqueta: 'Dictamen CTCE' }
];