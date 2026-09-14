using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Features.Tramites.Services;

public static class AcuseMetadatosFactory
{
    public static AcusePdfMetadatosDTO Crear(
        Tramite tramite,
        Certificado certificadoAcuse,
        string emisorCaSujeto,
        string baseUrl,
        string tokenVerificacion,
        DateTime fechaFirmaUtc)
    {
        var alumno = tramite.Alumno
            ?? throw new InvalidOperationException("No se encontraron los datos del alumno.");

        var url = $"{baseUrl.TrimEnd('/')}/verificar/acuse/{tokenVerificacion.Trim()}";

        return new AcusePdfMetadatosDTO(
            url,
            certificadoAcuse.Sujeto,
            emisorCaSujeto,
            certificadoAcuse.NumeroSerie,
            certificadoAcuse.VigenteDesde,
            certificadoAcuse.VigenteHasta,
            tramite.Id,
            $"{alumno.Nombre} {alumno.ApellidoPaterno} {alumno.ApellidoMaterno}".Trim(),
            alumno.NumeroBoleta,
            ConvertirFechaLocalMexico(fechaFirmaUtc));
    }

    private static DateTime ConvertirFechaLocalMexico(DateTime fechaFirmaUtc)
    {
        var utc = fechaFirmaUtc.Kind == DateTimeKind.Utc
            ? fechaFirmaUtc
            : DateTime.SpecifyKind(fechaFirmaUtc, DateTimeKind.Utc);

        try
        {
            var zona = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return TimeZoneInfo.ConvertTimeFromUtc(utc, zona);
        }
        catch
        {
            return utc.ToLocalTime();
        }
    }
}