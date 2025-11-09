/**
 * Función genérica para hacer fetch
 * @param {string} url URL del recurso
 * @param {string} metodo Método HTTP ('GET', 'POST', etc.)
 * @param {object|null} datos Datos a enviar (opcional)
 * @param {string} tipoRespuesta 'json', 'text', 'html' (por defecto 'json')
 * @returns {Promise<{success: boolean, data?: any, message?: string}>} Respuesta procesada
 */

async function executeFetch(url, metodo = 'GET', datos = null, tipoRespuesta = 'json') {
    const opciones = {
        method: metodo,
        headers: {}
    };

    // Añadir token anti-forgery si existe
    const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
    if (tokenInput) {
        opciones.headers['RequestVerificationToken'] = tokenInput.value;
    }

    // Enviar datos si los hay
    if (datos) {
        // Si los datos son FormData, no se pone Content-Type manualmente
        if (datos instanceof FormData) {
            opciones.body = datos;
        } else {
            // Tipo JSON
            opciones.headers['Content-Type'] = 'application/json';
            opciones.body = JSON.stringify(datos);
        }
    }

    const response = await fetch(url, opciones);

    // Verificar si hay redirección (sesión expirada)
    if (response.redirected) {
        window.location.href = response.url;
        throw new Error('Sesión expirada. Redirigiendo...');
    }

    // Parsear respuesta
    const resultado = tipoRespuesta.toLowerCase() === 'json'
        ? await response.json()
        : await response.text();

    // Si el status no es exitoso, lanzar error
    if (!response.ok) {
        const errorMessage = resultado?.message || `Error ${response.status}`;
        throw new Error(errorMessage);
    }

    // Lanzar error si el backend indica failure
    if (resultado.success === false) {
        throw new Error(resultado.message || 'Operación falló');
    }

    return {
        success: true,
        data: resultado,
        message: resultado.message
    };
}
