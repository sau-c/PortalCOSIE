using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Application.DTO.Dashboard;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Infrastructure.Data;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TarjetasDTO> ObtenerTarjetasContables()
        {
            var rolAlumnoId = await _context.Roles
                .Where(r => r.Name == "Alumno")
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            var totalAlumnos = await _context.Set<Alumno>().CountAsync();
            var alumnosActivos = await _context.UserRoles
                .CountAsync(ur => ur.RoleId == rolAlumnoId);

            var tramitesDict = await _context.Set<Tramite>()
                .GroupBy(t => t.EstadoTramiteId)
                .Select(g => new { EstadoId = g.Key, Cantidad = g.Count() })
                .ToDictionaryAsync(k => k.EstadoId, v => v.Cantidad);

            var docsDict = await _context.Set<Documento>()
                .GroupBy(d => d.EstadoDocumentoId)
                .Select(g => new { EstadoId = g.Key, Cantidad = g.Count() })
                .ToDictionaryAsync(k => k.EstadoId, v => v.Cantidad);

            // Helper local para leer del diccionario
            int GetCount(Dictionary<int, int> dict, int id) => dict.TryGetValue(id, out int val) ? val : 0;

            return new TarjetasDTO(
                totalAlumnos,
                alumnosActivos,

                GetCount(tramitesDict, EstadoTramite.Solicitado.Id),
                GetCount(tramitesDict, EstadoTramite.EnRevision.Id),
                GetCount(tramitesDict, EstadoTramite.Concluido.Id),

                GetCount(docsDict, EstadoDocumento.ConErrores.Id),
                GetCount(docsDict, EstadoDocumento.Validado.Id),
                GetCount(docsDict, EstadoDocumento.EnRevision.Id)
            );
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
        public async Task<ChartDTO> ObtenerUnidadesMasReprobadasPorCarrera(int? carreraId, string periodo)
        {
            // Dame todas las UnidadesReprobadas.
            var query = _context.Set<DetalleCTCE>()
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
                    Materia = g.Key.Materia,
                    Carrera = g.Key.Carrera,
                    Total = g.Count()
                })
                .OrderByDescending(x => x.Total)
                .Take(10)
                .ToListAsync();

            return new ChartDTO
            {
                Labels = ranking.Select(r => r.Materia).ToList(),
                Values = ranking.Select(r => r.Total).ToList()
            };
        }
    }
}
