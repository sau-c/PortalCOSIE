using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities.Bitacoras;
using PortalCOSIE.Infrastructure.Persistence;

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
                join u in _context.Users
                    on b.IdentityUserId equals u.Id into usersJoin
                from u in usersJoin.DefaultIfEmpty()   // LEFT JOIN
                orderby b.FechaRegistro descending
                select new EntradaBitacora
                (
                    b.Id,
                    b.Accion,
                    b.Entidad,
                    b.EntidadId,
                    b.ValorNuevo,
                    u != null ? u.Email : "Sistema",  // Maneja casos sin usuario
                    b.IpAddress,
                    b.UserAgent,
                    b.FechaRegistro
                );

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
