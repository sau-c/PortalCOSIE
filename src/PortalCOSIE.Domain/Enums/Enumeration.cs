using PortalCOSIE.Domain.Entities;
using System.Reflection;

namespace PortalCOSIE.Domain.Common
{
    public abstract class Enumeration : BaseEntity, IComparable
    {
        public string Nombre { get; protected set; }

        protected Enumeration(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public override string ToString() => Nombre;

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration other)
                return false;

            return Id == other.Id && GetType() == other.GetType();
        }

        public override int GetHashCode() => Id.GetHashCode();

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            return typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(f => f.FieldType == typeof(T))
                .Select(f => f.GetValue(null))
                .Cast<T>();
        }

        public static T FromValue<T>(int id) where T : Enumeration
        {
            return GetAll<T>().First(x => x.Id == id);
        }
    }
}
