using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Tramites.Commands.PrepararAcuse;

public class PrepararAcuseHandler : IRequestHandler<PrepararAcuseCommand, Result<PrepararAcuseResultDTO>>
{
    private readonly ITramiteRepository _tramiteRepo;
    private readonly ISecurityService _security;
    private readonly ICertificadoAdminService _certificadoAdmin;
    private readonly ICertificadoCaService _certificadoCa;
    private readonly IUnitOfWork _unitOfWork;

    public PrepararAcuseHandler(
        ITramiteRepository tramiteRepo,
        ISecurityService security,
        ICertificadoAdminService certificadoAdmin,
        ICertificadoCaService certificadoCa,
        IUnitOfWork unitOfWork)
    {
        _tramiteRepo = tramiteRepo;
        _security = security;
        _certificadoAdmin = certificadoAdmin;
        _certificadoCa = certificadoCa;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PrepararAcuseResultDTO>> Handle(PrepararAcuseCommand command)
    {
        if (!await _security.TieneRolAsync(command.IdentityUserId, "Administrador"))
            return Result<PrepararAcuseResultDTO>.Failure("Solo un administrador puede preparar el acuse.");

        if (command.Acuse?.Contenido == null || command.Acuse.Contenido.Length == 0)
            return Result<PrepararAcuseResultDTO>.Failure("El dictamen es obligatorio.");

        var tramite = await _tramiteRepo.ObtenerTramiteCTCEPorIdParaRevision(command.TramiteId);
        if (tramite is null)
            return Result<PrepararAcuseResultDTO>.Failure("Trámite no encontrado.");
        if (tramite.EstadoTramiteId != EstadoTramite.EsperandoAcuse.Id)
            return Result<PrepararAcuseResultDTO>.Failure("El trámite no está en estado de espera de acuse.");

        if (await _certificadoAdmin.ObtenerCertificadoAcuseParaFirmaAsync() is null)
            return Result<PrepararAcuseResultDTO>.Failure("No hay certificado de acuse registrado. Genera uno en Mi cuenta.");

        if (await _certificadoCa.ObtenerCaAsync() is null)
            return Result<PrepararAcuseResultDTO>.Failure("No hay certificado de la CA PortalCOSIE registrado.");

        byte[] pdfOriginal;
        using (var ms = new MemoryStream())
        {
            await command.Acuse.Contenido.CopyToAsync(ms);
            pdfOriginal = ms.ToArray();
        }

        var token = Guid.NewGuid().ToString("N");
        tramite.EstablecerTokenAcusePendiente(token);
        await _unitOfWork.SaveChangesAsync();

        return Result<PrepararAcuseResultDTO>.Success(new PrepararAcuseResultDTO(
            token,
            command.Acuse.Nombre,
            Convert.ToBase64String(pdfOriginal)));
    }
}