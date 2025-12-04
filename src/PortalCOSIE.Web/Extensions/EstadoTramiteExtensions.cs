using PortalCOSIE.Domain.Entities.Tramites;

public static class EstadoTramiteExtensions
{
    public static string ToBadgeClass(this EstadoTramite estado)
    {
        if (estado is null) return "bg-light text-dark";

        return estado switch
        {
            // Usamos .Equals() porque compara por ID, no por referencia de memoria
            _ when estado.Equals(EstadoTramite.Solicitado) => "bg-light text-black",
            _ when estado.Equals(EstadoTramite.EnRevision) => "bg-secondary",
            _ when estado.Equals(EstadoTramite.DocumentosPendientes) => "bg-warning text-black",
            _ when estado.Equals(EstadoTramite.Concluido) => "bg-success",
            _ when estado.Equals(EstadoTramite.Cancelado) => "bg-danger",
            _ => "bg-light text-dark"
        };
    }

    public static string ToIconClass(this EstadoTramite estado)
    {
        if (estado is null) return "fas fa-circle-question";

        return estado switch
        {
            _ when estado.Equals(EstadoTramite.Solicitado) => "fas fa-inbox",
            _ when estado.Equals(EstadoTramite.EnRevision) => "fas fa-eye",
            _ when estado.Equals(EstadoTramite.DocumentosPendientes) => "fas fa-file-circle-exclamation",
            _ when estado.Equals(EstadoTramite.Cancelado) => "fas fa-circle-xmark",
            _ when estado.Equals(EstadoTramite.Concluido) => "fas fa-flag",
            _ => "fas fa-circle-question"
        };
    }
}