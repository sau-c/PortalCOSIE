using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Application.Features.Dashboard.DTO;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Tramites.CTCE;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Infrastructure.Persistence;

namespace PortalCOSIE.Infrastructure.QueryHandlers
{
    public class DashboardQueryService : IDashboardQueryService
    {
        private readonly AppDbContext _context;

        public DashboardQueryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ChartDTO> ObtenerSolicitudesPorCarrera(string periodo)
        {
            var data = await _context.Set<Tramite>()
                .Where(t => t.PeriodoSolicitud == periodo)
                .GroupBy(t => t.Alumno.Carrera.Nombre)
                .Select(g => new
                {
                    Carrera = g.Key,
                    Cantidad = g.Count()
                })
                .ToListAsync();

            return new ChartDTO
            {
                Labels = data.Select(x => x.Carrera).ToList(),
                Values = data.Select(x => x.Cantidad).ToList()
            };
        }
        public async Task<ChartDTO> ObtenerEstadoTramitesCTCE(string periodo)
        {
            var data = await _context.Set<Tramite>()
                .Where(t => t.PeriodoSolicitud == periodo)
                .Where(t => t.TipoTramiteId == TipoTramite.DictamenInterno.Id)
                .GroupBy(t => t.EstadoTramite.Nombre)
                .Select(g => new
                {
                    Nombre = g.Key,
                    Cantidad = g.Count()
                })
                .ToListAsync();

            return new ChartDTO
            {
                Labels = data.Select(x => x.Nombre).ToList(),
                Values = data.Select(x => x.Cantidad).ToList()
            };
        }
        public async Task<ChartDTO> ObtenerEstadoDocumentosCTCE(string periodo)
        {
            var data = await _context.Set<Documento>()
                .Where(d => d.Tramite.TipoTramiteId == TipoTramite.DictamenInterno.Id)
                .Where(d => d.Tramite.PeriodoSolicitud == periodo)
                .GroupBy(d => d.EstadoDocumento.Nombre)
                .Select(g => new
                {
                    Estado = g.Key,
                    Cantidad = g.Count()
                })
                .ToListAsync();

            return new ChartDTO
            {
                Labels = data.Select(x => x.Estado).ToList(),
                Values = data.Select(x => x.Cantidad).ToList()
            };
        }
        public async Task<ChartDTO> ObtenerRolesAlumnos()
        {
            var rolAlumnoId = await _context.Roles
                .Where(r => r.Name == "Alumno")
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            if (rolAlumnoId == null)
                return new ChartDTO();

            var totalAlumnos = await _context.Set<Alumno>().CountAsync();
            var alumnosActivos = await _context.Set<Alumno>()
                .Join(_context.UserRoles,
                      alumno => alumno.IdentityUserId,
                      userRole => userRole.UserId,
                      (alumno, userRole) => userRole)
                .CountAsync(ur => ur.RoleId == rolAlumnoId);

            var alumnosInactivos = totalAlumnos - alumnosActivos;

            return new ChartDTO
            {
                Labels = new List<string> { "Activos", "Inactivos" },
                Values = new List<int> { alumnosActivos, alumnosInactivos }
            };
        }
        public async Task<ChartDTO> ObtenerUnidadesMasReprobadasPorCarrera(int? carreraId, string periodo)
        {
            // Dame todas las UnidadesReprobadas.
            var query = _context.Set<TramiteCTCE>()
                .Where(d => d.PeriodoSolicitud == periodo)
                .SelectMany(d => d.UnidadesReprobadas);

            // Paso 2: Filtramos la carrera de la UNIDAD DE APRENDIZAJE.
            if (carreraId.HasValue && carreraId.Value > 0)
                query = query.Where(ur => ur.UnidadAprendizaje.CarreraId == carreraId.Value);

            // Paso 3: Agrupamos y seleccionamos
            var ranking = await query
                .GroupBy(ur => new
                {
                    Materia = ur.UnidadAprendizaje.Nombre,
                    Carrera = ur.UnidadAprendizaje.Carrera.Nombre
                })
                .Select(g => new
                {
                    g.Key.Materia,
                    g.Key.Carrera,
                    Total = g.Count()
                })
                .OrderByDescending(x => x.Total)
                .Take(20)
                .ToListAsync();

            return new ChartDTO
            {
                Labels = ranking.Select(r => r.Materia).ToList(),
                Values = ranking.Select(r => r.Total).ToList()
            };
        }
    }
}
