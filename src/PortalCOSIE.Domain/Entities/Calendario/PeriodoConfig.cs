namespace PortalCOSIE.Domain.Entities.Calendario
{
    public class PeriodoConfig : BaseEntity
    {
        public int AnioInicio { get; private set; }
        public int PeriodoInicio { get; private set; }
        public int AnioActual { get; private set; }
        public int PeriodoActual { get; private set; }

        // Constructor protegido para EF
        protected PeriodoConfig() { }

        // Constructor principal
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
