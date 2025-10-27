using System.Security.Cryptography;

namespace PortalCOSIE.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }
        public bool IsDeleted { get; private set; }

        public void SoftDelete()
        {
            if (IsDeleted)
                throw new DomainException("La entidad ya ha sido desactivada.");
            IsDeleted = true;
        }

        public void Restore()
        {
            if (!IsDeleted)
                throw new DomainException("La entidad ya está activa.");
            IsDeleted = false;
        }
    }
}
