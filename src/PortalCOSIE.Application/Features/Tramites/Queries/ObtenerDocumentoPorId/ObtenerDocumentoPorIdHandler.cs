using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Application.Features.Tramites.Queries.ObtenerDocumentoPorId
{
    public class ObtenerDocumentoPorIdHandler : IRequestHandler<ObtenerDocumentoPorIdQuery, ArchivoDescargaDTO>
    {
        private readonly IDocumentoQueryService _documentoService;
        private readonly ITramiteRepository _tramiteRepo;
        public ObtenerDocumentoPorIdHandler(
            IDocumentoQueryService documentoService,
            ITramiteRepository tramiteRepo)
        {
            _documentoService = documentoService;
            _tramiteRepo = tramiteRepo;
        }

        public async Task<ArchivoDescargaDTO> Handle(ObtenerDocumentoPorIdQuery command)
        {
            // Primero, obtener el trámiteId del documento
            var tramiteId = await _tramiteRepo.ObtenerTramiteIdPorDocumentoId(command.DocumentoId);
            return await _documentoService.ObtenerBytesParaDescarga(tramiteId, command.DocumentoId, command.IdentityUserId);
        }
    }
}
