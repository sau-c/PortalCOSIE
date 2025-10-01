using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortalCOSIE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NuevaBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carrera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnidadAprendizaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CarreraId = table.Column<int>(type: "int", nullable: false),
                    Semestre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadAprendizaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadAprendizaje_Carrera_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carrera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alumno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NumeroBoleta = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PeriodoIngreso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarreraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alumno_Carrera_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carrera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alumno_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdPersonal = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personal_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tramite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    PersonalId = table.Column<int>(type: "int", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaConclusion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tramite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tramite_Alumno_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tramite_Personal_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Personal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TramiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documento_Tramite_TramiteId",
                        column: x => x.TramiteId,
                        principalTable: "Tramite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Carrera",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Mecatrónica" },
                    { 2, "Telemática" },
                    { 3, "Biónica" },
                    { 4, "Energía" },
                    { 5, "ISISA" }
                });

            migrationBuilder.InsertData(
                table: "UnidadAprendizaje",
                columns: new[] { "Id", "CarreraId", "Nombre", "Semestre" },
                values: new object[,]
                {
                    { 1, 2, "Programación", 1 },
                    { 2, 2, "Análisis y diseño de sistemas", 1 },
                    { 3, 2, "Estructura de datos", 1 },
                    { 4, 2, "Administración de sistemas operativos", 1 },
                    { 5, 2, "Diseño digital", 1 },
                    { 6, 2, "Arquitectura de computadoras", 1 },
                    { 7, 2, "Fundamentos de física", 1 },
                    { 8, 2, "Ecuaciones diferenciales", 1 },
                    { 9, 2, "Probabilidad", 1 },
                    { 10, 2, "Cálculo diferencial e integral", 1 },
                    { 11, 2, "Variable compleja", 1 },
                    { 12, 2, "Álgebra lineal", 1 },
                    { 13, 2, "Electromagnetismo", 1 },
                    { 14, 2, "Cálculo multivariable", 1 },
                    { 15, 2, "Administración organizacional", 1 },
                    { 16, 2, "Ética, profesión y sociedad", 1 },
                    { 17, 2, "Comunicación oral y escrita", 1 },
                    { 18, 2, "Inglés I", 1 },
                    { 19, 2, "Inglés II", 1 },
                    { 20, 2, "Programación estructurada", 1 },
                    { 21, 2, "Sociedad, ciencia y tecnología", 1 },
                    { 22, 2, "Señales y sistemas", 2 },
                    { 23, 2, "Propagación de ondas electromagnéticas", 2 },
                    { 24, 2, "Electrónica", 2 },
                    { 25, 2, "Teoría de los circuitos", 2 },
                    { 26, 2, "Teoría de la información", 2 },
                    { 27, 2, "Teoría de las comunicaciones", 2 },
                    { 28, 2, "Comunicaciones digitales", 2 },
                    { 29, 2, "Procesamiento digital de señales", 2 },
                    { 30, 2, "Telefonía", 2 },
                    { 31, 2, "Sistemas celulares", 2 },
                    { 32, 2, "Protocolos de Internet", 2 },
                    { 33, 2, "Sistemas distribuidos", 2 },
                    { 34, 2, "Ingeniería web", 2 },
                    { 35, 2, "Programación avanzada", 2 },
                    { 36, 2, "Bases de datos", 2 },
                    { 37, 2, "Transmisión de datos", 2 },
                    { 38, 2, "Información financiera e ingeniería económica", 2 },
                    { 39, 2, "Optativa I", 2 },
                    { 40, 2, "Inglés III", 2 },
                    { 41, 2, "Métodos numéricos", 2 },
                    { 42, 2, "Electrónica para comunicaciones", 2 },
                    { 43, 2, "Óptica", 2 },
                    { 44, 2, "Desarrollo sustentable", 2 },
                    { 45, 2, "Economía para ingenieros", 2 },
                    { 46, 2, "Inglés IV", 2 },
                    { 47, 2, "Redes inalámbricas", 2 },
                    { 48, 2, "Redes neuronales", 2 },
                    { 49, 2, "Lógica difusa", 2 },
                    { 50, 2, "Sistemas de información geográfica", 2 },
                    { 51, 2, "Programación de dispositivos móviles", 2 },
                    { 52, 2, "Normatividad en telecomunicaciones e informática", 2 },
                    { 53, 2, "Redes inteligentes", 3 },
                    { 54, 2, "Líneas de transmisión y antenas", 3 },
                    { 55, 2, "Seguridad en redes", 3 },
                    { 56, 2, "Multimedia", 3 },
                    { 57, 2, "Bases de datos distribuidas", 3 },
                    { 58, 2, "Metodología de la investigación", 3 },
                    { 59, 2, "Administración de proyectos", 3 },
                    { 60, 2, "Liderazgo y emprendedores", 3 },
                    { 61, 2, "Criptografía", 3 },
                    { 62, 2, "Microondas", 3 },
                    { 63, 2, "Procesamiento de imágenes", 3 },
                    { 64, 2, "Televisión digital", 3 },
                    { 65, 2, "Sistemas de calidad", 3 },
                    { 66, 2, "Procesamiento de voz", 3 },
                    { 67, 2, "Filtrado avanzado", 3 },
                    { 68, 2, "Proyecto terminal I", 4 },
                    { 69, 2, "Proyecto terminal II", 4 },
                    { 70, 2, "Redes de telecomunicaciones", 4 },
                    { 71, 2, "Aplicaciones distribuidas", 4 },
                    { 72, 2, "Dispositivos programables", 4 },
                    { 73, 2, "Servicio social", 4 },
                    { 74, 2, "Electiva I", 4 },
                    { 75, 2, "Electiva II", 4 },
                    { 76, 2, "Electiva III", 4 },
                    { 77, 2, "Electiva IV", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumno_CarreraId",
                table: "Alumno",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_Alumno_NumeroBoleta",
                table: "Alumno",
                column: "NumeroBoleta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_TramiteId",
                table: "Documento",
                column: "TramiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_AlumnoId",
                table: "Tramite",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_PersonalId",
                table: "Tramite",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadAprendizaje_CarreraId",
                table: "UnidadAprendizaje",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdentityUserId",
                table: "Usuario",
                column: "IdentityUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "UnidadAprendizaje");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tramite");

            migrationBuilder.DropTable(
                name: "Alumno");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "Carrera");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
