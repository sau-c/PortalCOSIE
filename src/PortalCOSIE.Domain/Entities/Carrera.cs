namespace PortalCOSIE.Domain.Entities
{
    public class Carrera : BaseEntity
    {
        public string Nombre { get; private set; }
        private readonly List<UnidadAprendizaje> _unidadesAprendizaje = new();

        //Navegacion de EFCore
        public virtual IReadOnlyCollection<UnidadAprendizaje> UnidadesAprendizaje => _unidadesAprendizaje.AsReadOnly();

        // Constructor requerido por EF Core (debe ser protegido o privado)
        protected Carrera() { }

        public Carrera(string nombre)
        {
            SetNombre(nombre);
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre de la carrera no puede estar vacío.");

            Nombre = nombre;
        }

        public void AgregarUnidadAprendizaje(UnidadAprendizaje unidad)
        {
            if (unidad == null)
                throw new DomainException("La unidad de aprendizaje no puede ser nula");

            _unidadesAprendizaje.Add(unidad);
        }

        public void RemoverUnidadAprendizaje(UnidadAprendizaje unidad)
        {
            if (unidad == null)
                throw new DomainException("La unidad de aprendizaje no puede ser nula");

            _unidadesAprendizaje.Remove(unidad);
        }
    }
}
