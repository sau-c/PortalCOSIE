using Microsoft.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Infrastructure.Data;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class DocumentoRepository : IDocumentoRepository
    {
        private readonly AppDbContext _context;

        public DocumentoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task SubirArchivoAsync(Documento documento, Stream contenido)
        {
            // Inicializamos el contenido en 0 bytes para reservar el espacio en SQL.
            documento.SetContenido(Array.Empty<byte>());

            _context.Set<Documento>().Add(documento);
            await _context.SaveChangesAsync(); // Esto genera el ID y el RowGuid en BD si es necesario

            // 2. Preparar el streaming (FILESTREAM requiere transacción activa)
            // Usamos la transacción actual del UoW.
            var dbTransaction = _context.Database.CurrentTransaction?.GetDbTransaction();

            if (dbTransaction == null)
            {
                throw new InvalidOperationException("FILESTREAM requiere una transacción activa.");
            }

            // 3. Obtener Path y Contexto del archivo insertado
            string sql = "SELECT Contenido.PathName(), GET_FILESTREAM_TRANSACTION_CONTEXT() FROM Documento WHERE Id = @Id";

            var cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.Transaction = dbTransaction;
            cmd.CommandText = sql;

            var param = cmd.CreateParameter();
            param.ParameterName = "@Id";
            param.Value = documento.Id;
            cmd.Parameters.Add(param);

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    var filePath = reader.GetString(0); // Path lógico de SQL
                    var transactionContext = (byte[])reader.GetValue(1); // Contexto transaccional

                    reader.Close(); // Importante cerrar reader antes de abrir el stream de escritura

                    // 4. Escribir directamente al FileSystem de SQL Server
                    using (var sqlFileStream = new SqlFileStream(filePath, transactionContext, FileAccess.Write))
                    {
                        await contenido.CopyToAsync(sqlFileStream);
                    }
                }
            }
        }
    }
}