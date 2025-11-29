using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities.Bitacoras;
using PortalCOSIE.Infrastructure.Data;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly AppDbContext _context;

        public BitacoraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EntradaBitacora>> ListarConCorreo()
        {
            var query = from b in _context.Set<EntradaBitacora>()
                        join u in _context.Users on b.IdentityUserId equals u.Id // Asumiendo que b.UserId es el FK
                        orderby b.FechaRegistro descending
                        select new EntradaBitacora
                        (
                            b.Id,
                            b.Accion,
                            b.Entidad,
                            b.EntidadId,
                            b.ValorNuevo,
                            u.Email,
                            b.IpAddress,
                            b.UserAgent,
                            b.FechaRegistro
                        );

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
