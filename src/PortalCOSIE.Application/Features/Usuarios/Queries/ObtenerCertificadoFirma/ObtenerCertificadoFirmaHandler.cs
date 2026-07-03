using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerCertificadoFirma
{
    public class ObtenerCertificadoFirmaHandler : IRequestHandler<ObtenerCertificadoFirmaQuery, CertificadoFirmaDTO?>
    {
        private readonly IUsuarioRepository _usuarioRepo;

        public ObtenerCertificadoFirmaHandler(IUsuarioRepository usuarioRepo)
            => _usuarioRepo = usuarioRepo;

        public async Task<CertificadoFirmaDTO?> Handle(ObtenerCertificadoFirmaQuery query)
        {
            var usuario = await _usuarioRepo.BuscarUsuarioConCertificado(query.IdentityUserId);
            if (usuario?.Certificado == null)
                return null;

            var certificado = usuario.Certificado;
            var certificadoDer = certificado.CertificadoDer;
            return new CertificadoFirmaDTO(
                certificado.Id,
                certificado.Sujeto,
                certificado.VigenteDesde,
                certificado.VigenteHasta,
                Convert.ToBase64String(certificadoDer),
                certificadoDer.Length >= 300);
        }
    }
}