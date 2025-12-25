namespace PortalCOSIE.Domain.Entities.Usuarios
{
    public class Personal : Usuario
    {
        public string IdEmpleado { get; private set; } = string.Empty;
        public string Area { get; private set; } = string.Empty;

        // Constructor protegido para EF
        private Personal() { }

        // Constructor de dominio
        public Personal(
            string identityUserId,
            string idEmpleado,
            string nombre,
            string apellidoPaterno,
            string apellidoMaterno,
            string area)
            : base(identityUserId, nombre, apellidoPaterno, apellidoMaterno)
        {
            SetIdEmpleado(idEmpleado);
            SetArea(area);
        }

        public void SetIdEmpleado(string idPersonal)
        {
            idPersonal = idPersonal?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(idPersonal))
                throw new DomainException("El identificador del personal no puede estar vacío.");
            if (idPersonal.Length > 50)
                throw new DomainException("El identificador del personal no puede tener más de 50 caracteres.");
            IdEmpleado = idPersonal;
        }

        public void SetArea(string area)
        {
            area = area?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(area))
                throw new DomainException("El area del personal no puede estar vacía.");
            if (area.Length > 100)
                throw new DomainException("El area del personal no puede tener más de 100 caracteres.");
            IdEmpleado = area;
        }
    }
}
