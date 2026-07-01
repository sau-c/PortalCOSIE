using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalCOSIE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirmaElectronica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashOriginal",
                table: "Documento");

            migrationBuilder.AddColumn<string>(
                name: "CertificadoId",
                table: "Usuario",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirmaElectronicaId",
                table: "Documento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Certificado",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Sujeto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NumeroSerie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VigenteDesde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VigenteHasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CertificadoDer = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FirmaElectronica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificadoId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    FirmaCms = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Algoritmo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaFirmaUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmaElectronica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirmaElectronica_Certificado_CertificadoId",
                        column: x => x.CertificadoId,
                        principalTable: "Certificado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CertificadoId",
                table: "Usuario",
                column: "CertificadoId",
                unique: true,
                filter: "[CertificadoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_FirmaElectronicaId",
                table: "Documento",
                column: "FirmaElectronicaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FirmaElectronica_CertificadoId",
                table: "FirmaElectronica",
                column: "CertificadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_FirmaElectronica_FirmaElectronicaId",
                table: "Documento",
                column: "FirmaElectronicaId",
                principalTable: "FirmaElectronica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Certificado_CertificadoId",
                table: "Usuario",
                column: "CertificadoId",
                principalTable: "Certificado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documento_FirmaElectronica_FirmaElectronicaId",
                table: "Documento");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Certificado_CertificadoId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "FirmaElectronica");

            migrationBuilder.DropTable(
                name: "Certificado");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_CertificadoId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Documento_FirmaElectronicaId",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "CertificadoId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "FirmaElectronicaId",
                table: "Documento");

            migrationBuilder.AddColumn<byte[]>(
                name: "HashOriginal",
                table: "Documento",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
