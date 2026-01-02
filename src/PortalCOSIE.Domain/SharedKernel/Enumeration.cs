using PortalCOSIE.Domain.SharedKernel;
using System.Reflection;

/// <summary>
/// Clase base abstracta para implementar el patrón Smart Enumeration (Coherente con BD).
/// Proporciona un tipo seguro con comportamiento de objeto completo que puede ser extendido.
/// </summary>
/// <remarks>
/// Ventajas sobre enums nativos:
/// - Mejor para persistencia en base de datos
/// - Puede tener propiedades adicionales
/// - Puede contener lógica de negocio
/// - Es extensible mediante herencia
/// - Soporta métodos de instancia
/// </remarks>
public abstract class Enumeration : BaseEntity<int>, IComparable
{
    /// <summary>
    /// Nombre descriptivo de la enumeración
    /// </summary>
    public string Nombre { get; protected set; }

    /// <summary>
    /// Constructor protegido para inicializar una enumeración
    /// </summary>
    /// <param name="id">Identificador único de la enumeración</param>
    /// <param name="nombre">Nombre descriptivo de la enumeración</param>
    protected Enumeration(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }

    /// <summary>
    /// Representación en string de la enumeración (devuelve el nombre)
    /// </summary>
    /// <returns>Nombre de la enumeración</returns>
    public override string ToString() => Nombre;

    /// <summary>
    /// Compara dos enumeraciones para determinar si son iguales
    /// </summary>
    /// <param name="obj">Objeto a comparar</param>
    /// <returns>True si tienen el mismo Id y tipo</returns>
    public override bool Equals(object obj)
    {
        if (obj is not Enumeration other)
            return false;

        return Id == other.Id && GetType() == other.GetType();
    }

    /// <summary>
    /// Obtiene el código hash basado en el Id
    /// </summary>
    /// <returns>Código hash del Id</returns>
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// Compara esta enumeración con otra para ordenamiento
    /// </summary>
    /// <param name="other">Otra enumeración a comparar</param>
    /// <returns>Valor que indica el orden relativo</returns>
    public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

    /// <summary>
    /// Obtiene todas las instancias de una enumeración específica
    /// </summary>
    /// <typeparam name="T">Tipo de enumeración</typeparam>
    /// <returns>Colección de todas las instancias de la enumeración</returns>
    /// <example>
    /// var estados = EstadoTramite.GetAll&lt;EstadoTramite&gt;();
    /// </example>
    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        return typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(f => f.FieldType == typeof(T))
            .Select(f => f.GetValue(null))
            .Cast<T>();
    }

    /// <summary>
    /// Obtiene una instancia específica de enumeración por su Id
    /// </summary>
    /// <typeparam name="T">Tipo de enumeración</typeparam>
    /// <param name="id">Id de la enumeración a buscar</param>
    /// <returns>Instancia de la enumeración con el Id especificado</returns>
    /// <exception cref="InvalidOperationException">Cuando no se encuentra la enumeración con el Id especificado</exception>
    /// <example>
    /// var estado = EstadoTramite.FromValue&lt;EstadoTramite&gt;(1);
    /// </example>
    public static T FromValue<T>(int id) where T : Enumeration
    {
        return GetAll<T>().First(x => x.Id == id);
    }
}