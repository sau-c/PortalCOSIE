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
        //throw new Error('Sesión expirada. Redirigiendo...');
    }

    // Parsear respuesta
    const resultado = tipoRespuesta.toLowerCase() === 'json'
        ? await response.json()
        : await response.text();

    // Si el status no es exitoso, lanzar error
    if (!response.ok) {
        const errorMessage = resultado.message || `Error ${response.status}`;
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

/**
 * Controlador de botón para bloquear y restaurar durante procesos async.
 * @param {HTMLButtonElement} button - El botón que quieres controlar.
 * @returns {object} - Funciones bloquear() y restaurar().
 */
function bloqueadorBoton(button) {
    const icon = button.querySelector('i');
    const textSpan = button.querySelector('span');

    const originalState = {
        iconClass: icon ? icon.className : '',
        text: textSpan ? textSpan.textContent : ''
    };

    const bloquear = () => {
        if (icon) icon.className = 'fas fa-spinner fa-spin me-2';
        if (textSpan) textSpan.textContent = 'Cargando...';
        button.disabled = true;
    };

    const restaurar = () => {
        if (icon) icon.className = originalState.iconClass;
        if (textSpan) textSpan.textContent = originalState.text;
        button.disabled = false;
    };

    return { bloquear, restaurar };
}

function limitarArchivos({ maxMB = 3, tiposPermitidos = ["application/pdf", "application/x-x509-ca-cert", "application/pkix-cert", "application/octet-stream"], extensionesPermitidas = ["pdf", "cer", "key"] } = {}) {

    const MAX_BYTES = maxMB * 1024 * 1024;

    document.querySelectorAll('input[type="file"]').forEach(input => {
        input.addEventListener("change", () => {
            const file = input.files[0];
            if (!file) return;

            // Verifica tamaño
            if (file.size > MAX_BYTES) {
                showGlobalModal("error", `Tamaño máximo permitido: ${maxMB} MB`);
                input.value = "";
                return;
            }

            // Verifica tipo MIME o extensión
            const extension = file.name.split('.').pop().toLowerCase();
            if (!tiposPermitidos.includes(file.type) && !extensionesPermitidas.includes(extension)) {
                showGlobalModal("error", `Tipo de archivo no permitido. Solo: ${extensionesPermitidas.join(", ")}`);
                input.value = "";
            }
        });
    });
}



/**
* Componente Genérico para Chart Cards
*/
class ChartWidget {
    constructor(element) {
        this.container = element;
        this.apiUrl = element.dataset.apiUrl;
        this.chartType = element.dataset.chartType || 'doughnut';
        this.chartId = element.dataset.chartId;

        // Referencias internas (buscamos DENTRO del contenedor para evitar colisiones de ID)
        this.canvas = this.container.querySelector('canvas');
        this.loader = this.container.querySelector('.chart-loader');
        this.filters = this.container.querySelectorAll('.chart-filter');

        this.chartInstance = null;

        // Configuración base de colores y estilos
        this.colors = ['#5A1236', '#FF6B35', '#FFC107', '#85CB33', '#1391FF', '#6C757D'];

        this.init();
    }

    init() {
        // 1. Escuchar cambios en cualquier filtro dentro de la tarjeta
        this.filters.forEach(filter => {
            filter.addEventListener('change', () => this.loadData());
        });

        // 2. Carga inicial
        this.loadData();
    }

    async loadData() {
        if (!this.apiUrl) return;

        this.toggleLoader(true);

        try {
            // Construir Query String dinámico basado en los filtros existentes
            const params = new URLSearchParams();
            this.filters.forEach(f => params.append(f.name, f.value));

            const fullUrl = `${this.apiUrl}?${params.toString()}`;

            // Usamos tu función executeFetch existente
            const response = await executeFetch(fullUrl, 'GET');

            // Asumimos que el backend devuelve { data: { labels: [], values: [] } }
            if (response && response.data) {
                this.updateChart(response.data.labels, response.data.values);
            }
        } catch (error) {
            console.error("Error loading chart:", error);
            // Opcional: Mostrar error visual en el card
        } finally {
            this.toggleLoader(false);
        }
    }

    updateChart(labels, values) {
        if (!this.chartInstance) {
            // Crear nueva instancia
            this.chartInstance = new Chart(this.canvas, {
                type: this.chartType,
                data: {
                    labels: labels,
                    datasets: [{
                        data: values,
                        backgroundColor: this.colors,
                        borderWidth: 0
                    }]
                },
                options: this.getChartOptions(),
                plugins: [ChartDataLabels] // Asegúrate de tener el plugin importado
            });
        } else {
            // Actualizar existente
            this.chartInstance.data.labels = labels;
            this.chartInstance.data.datasets[0].data = values;
            this.chartInstance.update();
        }
    }

    getChartOptions() {
        return {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { position: 'bottom' },
                datalabels: {
                    color: '#fff',
                    font: { weight: 'bold', size: 14 },
                    formatter: (value, ctx) => {
                        const total = ctx.chart.data.datasets[0].data.reduce((a, b) => a + b, 0);
                        return total > 0 ? ((value / total) * 100).toFixed(1) + '%' : '0%';
                    }
                }
            }
        };
    }

    toggleLoader(visible) {
        if (!this.loader) return;
        if (visible) {
            this.loader.classList.remove('d-none');
            this.canvas.style.opacity = 0.3;
        } else {
            this.loader.classList.add('d-none');
            this.canvas.style.opacity = 1;
        }
    }
}

// Inicialización Automática
document.addEventListener('DOMContentLoaded', () => {
    // Busca todos los elementos generados por el TagHelper
    const widgets = document.querySelectorAll('[data-component="chart-widget"]');
    widgets.forEach(el => new ChartWidget(el));

    limitarArchivos();
});