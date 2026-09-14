using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerCertificadoFirma
{
    public class ObtenerCertificadoFirmaHandler : IRequestHandler<ObtenerCertificadoFirmaQuery, CertificadoFirmaDTO?>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ISecurityService _security;
        private readonly ICertificadoAdminService _certificadoAdmin;

        public ObtenerCertificadoFirmaHandler(
            IUsuarioRepository usuarioRepo,
            ISecurityService security,
            ICertificadoAdminService certificadoAdmin)
        {
            _usuarioRepo = usuarioRepo;
            _security = security;
            _certificadoAdmin = certificadoAdmin;
        }

        public async Task<CertificadoFirmaDTO?> Handle(ObtenerCertificadoFirmaQuery query)
        {
            if (await _security.TieneRolAsync(query.IdentityUserId, "Administrador"))
            {
                var certificadoAcuse = await _certificadoAdmin.ObtenerCertificadoAcuseParaFirmaAsync();
                return certificadoAcuse == null ? null : Mapear(certificadoAcuse);
            }

            var usuario = await _usuarioRepo.BuscarUsuarioConCertificado(query.IdentityUserId);
            if (usuario?.Certificado == null || usuario.Certificado.Tipo != TipoCertificado.Alumno)
                return null;

            return Mapear(usuario.Certificado);
        }

        private static CertificadoFirmaDTO Mapear(Certificado certificado)
        {
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