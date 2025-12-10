using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Application.DTO.Tramites;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Infrastructure.Data;

public class DocumentoQueryService : IDocumentoQueryService
{
    private readonly AppDbContext _context;

    public DocumentoQueryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ArchivoDescargaDTO?> ObtenerBytesParaDescarga(int tramiteId, int documentoId, string identityUserId)
    {
        // Usamos EF Core con AsNoTracking y Select (Proyección)
        // Esto NO crea entidades de dominio, crea un DTO directo desde SQL.
        
        return await _context.Set<Tramite>()
            .Where(t => t.Id == tramiteId)
            .Where(t =>
                t.Alumno.IdentityUserId == identityUserId || // ¿Es el alumno?
                (t.Personal != null && t.Personal.IdentityUserId == identityUserId) // ¿Es el personal asignado?
            )
            .SelectMany(t => t.Documentos)
            .Where(d => d.Id == documentoId)
            .Select(d => new ArchivoDescargaDTO
            {
                Nombre = d.Nombre,
                TipoContenido = "application/pdf",
                Contenido = d.Contenido // SQL Server manda solo los bytes de este registro
            })
            .AsNoTracking() // No ensuciamos el ChangeTracker
            .FirstOrDefaultAsync();
    }
}