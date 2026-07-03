/**
 * Firma CMS/PKCS#7 detached (SHA-256 + RSA) con node-forge.
 */
function firmarContenidoCmsBase64(contenidoBytes, material) {
    const p7 = forge.pkcs7.createSignedData();
    p7.content = forge.util.createBuffer(new Uint8Array(contenidoBytes));
    p7.addCertificate(material.certificado);
    p7.addSigner({
        key: material.llavePrivada,
        certificate: material.certificado,
        digestAlgorithm: forge.pki.oids.sha256
    });
    p7.sign({ detached: true });
    return forge.util.encode64(forge.asn1.toDer(p7.toAsn1()).getBytes());
}

async function firmarArchivoCmsConMaterial(archivo, material) {
    const contenido = await leerArchivoComoArrayBuffer(archivo);
    return firmarContenidoCmsBase64(contenido, material);
}