using PortalCOSIE.Domain.Entities.Tramites;

public static class EstadoTramiteExtensions
{
    public static string ToBadgeClass(this EstadoTramite estado)
    {
        return estado.Nombre switch
        {
            "Solicitado" => "bg-secondary",
            "EnRevision" => "bg-warning text-dark",
            "DocumentosPendientes" => "bg-secondary",
            "Concluido" => "bg-primary",
            "Cancelado" => "bg-danger",
            _ => "bg-light text-dark" // Default
        };
    }
    public static string ToIconClass(this EstadoTramite estado)
    {
        return estado.Nombre switch
        {
            "Solicitado" => "fa-solid fa-inbox",
            "EnRevision" => "fa-solid fa-eye",
            "DocumentosPendientes" => "fa-solid fa-file-circle-exclamation",
            "Cancelado" => "fa-solid fa-circle-xmark",
            "Concluido" => "fa-solid fa-flag",
            _ => "fa-solid fa-circle-question"
        };
    }

}