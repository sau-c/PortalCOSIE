using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalCOSIE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRutaDocumentoFuente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RutaDocumentoFuente",
                table: "FirmaElectronica");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RutaDocumentoFuente",
                table: "FirmaElectronica",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}