namespace PortalCOSIE.Domain.Entities.Calendario
{
    public class PeriodoConfig : BaseEntity
    {
        public int AnioInicio { get; private set; }
        public int PeriodoInicio { get; private set; }
        public int AnioFin { get; private set; }
        public int PeriodoFin { get; private set; }

        // Constructor protegido para EF
        protected PeriodoConfig() { }

        // Constructor principal
        public PeriodoConfig(int anioInicio, int periodoInicio, int anioFin, int periodoFin)
        {
            SetAnioInicio(anioInicio);
            SetPeriodoInicio(periodoInicio);
            SetAnioInicio(anioInicio);
            SetPeriodoFin(periodoFin);
        }

        public void SetAnioInicio(int anioInicio)
        {
            if (anioInicio < 1995 || anioInicio > 2100)
                throw new DomainException("El año de inicio debe estar entre 1995 y 2100.");
            AnioInicio = anioInicio;
        }
        
        public void SetPeriodoInicio(int inicio)
        {
            if (inicio < 0 || inicio > 2)
                throw new DomainException("Periodo de inicio debe estar entre 1 y 2.");
            PeriodoInicio = inicio;
        }

        public void SetAnioFin(int anioFin)
        {
            if (anioFin < 1995 || anioFin > 2100)
                throw new DomainException("El año de fin debe estar entre 1995 y 2100.");
            AnioFin = anioFin;
        }

        public void SetPeriodoFin(int fin)
        {
            if (fin < 0 || fin > 2)
                throw new DomainException("Periodo de inicio debe estar entre 1 y 2.");
            PeriodoFin = fin;
        }
    }
}
