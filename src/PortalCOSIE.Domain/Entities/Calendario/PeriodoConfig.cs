namespace PortalCOSIE.Domain.Entities.Calendario
{
    /// <summary>
    /// Configuración de los períodos escolares que se pueden usar en el sistema.
    /// </summary>
    /// <remarks>
    /// Configuracion para llenar los select que registran periodos escolares (2020/1, 2021,2, etc)
    /// </remarks>
    public class PeriodoConfig : BaseEntity<int>
    {
        /// <summary>Año de inicio del periodo escolar de referencia</summary>
        public int AnioInicio { get; private set; }

        /// <summary>Semestre de inicio del período académico de referencia</summary>
        public int PeriodoInicio { get; private set; }

        /// <summary>Año actual de termino del periodo</summary>
        public int AnioActual { get; private set; }

        /// <summary>Semestre actual de termino del periodo</summary>
        public int PeriodoActual { get; private set; }

        /// <summary>Constructor protegido para migraciones</summary>
        protected PeriodoConfig() { }

        public PeriodoConfig(int anioInicio, int periodoInicio, int anioActual, int periodoActual)
        {
            SetAnioInicio(anioInicio);
            SetPeriodoInicio(periodoInicio);
            SetAnioInicio(anioInicio);
            SetPeriodoActual(periodoActual);
        }
        
        public void SetAnioInicio(int anioInicio)
        {
            if (anioInicio < 1995 || anioInicio > 2100)
                throw new DomainException("El año de inicio debe estar entre 1995 y 2100.");
            AnioInicio = anioInicio;
        }

        public void SetPeriodoInicio(int periodoInicio)
        {
            if (periodoInicio < 0 || periodoInicio > 2)
                throw new DomainException("Periodo de inicio debe estar entre 1 y 2.");
            PeriodoInicio = periodoInicio;
        }
        
        public void SetAnioActual(int anioActual)
        {
            if (anioActual < 1995 || anioActual > 2100)
                throw new DomainException("El año de fin debe estar entre 1995 y 2100.");
            AnioActual = anioActual;
        }

        public void SetPeriodoActual(int periodoActual)
        {
            if (periodoActual < 0 || periodoActual > 2)
                throw new DomainException("Periodo de inicio debe estar entre 1 y 2.");
            PeriodoActual = periodoActual;
        }
    }
}