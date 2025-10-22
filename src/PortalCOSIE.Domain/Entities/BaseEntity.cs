using System.Security.Cryptography;

namespace PortalCOSIE.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool Eliminado { get; set; }
    }
}
