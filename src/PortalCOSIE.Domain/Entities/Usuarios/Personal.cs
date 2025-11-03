namespace PortalCOSIE.Domain.Entities.Usuarios
{
    public class Personal : BaseEntity
    {
        public string IdPersonal { get; private set; } = string.Empty;
        public string Area { get; private set; } = string.Empty;

        // Navegación
        public Usuario Usuario { get; private set; } = null!;

        // Constructor protegido para EF
        protected Personal() { }

        // Constructor de dominio
        public Personal(string idPersonal, string area)
        {
            SetIdPersonal(idPersonal);
            SetArea(area);
        }

        public void SetIdPersonal(string idPersonal)
        {
            idPersonal = idPersonal?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(idPersonal))
                throw new DomainException("El identificador del personal no puede estar vacío.");
            if (idPersonal.Length > 50)
                throw new DomainException("El identificador del personal no puede tener más de 50 caracteres.");
            IdPersonal = idPersonal;
        }

        public void SetArea(string area)
        {
            area = area?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(area))
                throw new DomainException("El area del personal no puede estar vacía.");
            if (area.Length > 100)
                throw new DomainException("El area del personal no puede tener más de 100 caracteres.");
            IdPersonal = area;
        }
    }
}
