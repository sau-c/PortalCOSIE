using PortalCOSIE.Domain.SharedKernel;

namespace PortalCOSIE.Domain.Entities.SesionesCOSIE
{
    /// <summary>
    /// Representa una fecha específica para recepción de trámites dentro de una sesión COSIE.
    /// </summary>
    /// <remarks>
    /// Cada sesión puede tener múltiples fechas de recepción que definen los días
    /// en que los alumnos pueden presentar trámites para ser incluidos en dicha sesión.
    /// Las fechas deben ser anteriores a la fecha de la sesión.
    /// </remarks>
    public class FechaRecepcion : BaseEntity<int>
    {
        /// <summary>Identificador de la sesión COSIE asociada</summary>
        public int SesionId { get; private set; }

        /// <summary>Fecha concreta para recepción de trámites</summary>
        public DateTime Fecha { get; private set; }

        /// <summary>Sesión COSIE a la que pertenece esta fecha</summary>
        public SesionCOSIE Sesion { get; private set; }

        /// <summary>Constructor protegido para migraciones</summary>
        protected FechaRecepcion() { }

        public FechaRecepcion(DateTime fecha)
        {
            Fecha = fecha;
        }
    }
}