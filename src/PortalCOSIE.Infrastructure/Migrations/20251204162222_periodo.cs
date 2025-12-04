using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalCOSIE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class periodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PeriodoSolicitud",
                table: "Tramite",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodoSolicitud",
                table: "Tramite");
        }
    }
}
