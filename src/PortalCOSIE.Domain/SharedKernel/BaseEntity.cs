namespace PortalCOSIE.Domain.SharedKernel
{
    /// <summary>
    /// Clase base abstracta para todas las entidades del dominio.
    /// Define un identificador genérico y soporte para eliminación lógica (soft delete).
    /// </summary>
    public abstract class BaseEntity<TId>
    {
        /// <summary>
        /// Identificador único de la entidad.
        /// </summary>
        public TId Id { get; protected set; }

        /// <summary>
        /// Indica si la entidad está marcada como eliminada lógicamente.
        /// </summary>
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// Marca la entidad como eliminada lógicamente.
        /// </summary>
        public void SoftDelete()
        {
            if (IsDeleted)
                throw new DomainException("La entidad ya ha sido desactivada.");
            IsDeleted = true;
        }

        /// <summary>
        /// Restaura una entidad eliminada lógicamente.
        /// </summary>
        public void Restore()
        {
            if (!IsDeleted)
                throw new DomainException("La entidad ya está activa.");
            IsDeleted = false;
        }
    }
}
