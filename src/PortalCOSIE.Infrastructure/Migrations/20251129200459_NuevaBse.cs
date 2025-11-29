using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortalCOSIE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NuevaBse : Migration
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
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoDocumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoTramite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoTramite", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeriodoConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnioInicio = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    PeriodoInicio = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    AnioFin = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    PeriodoFin = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodoConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SesionCOSIE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroSesion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaSesion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionCOSIE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoTramite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTramite", x => x.Id);
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
                name: "Bitacora",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntidadId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorNuevo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bitacora", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bitacora_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Semestre = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                name: "FechaRecepcion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SesionId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FechaRecepcion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FechaRecepcion_SesionCOSIE_SesionId",
                        column: x => x.SesionId,
                        principalTable: "SesionCOSIE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alumno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NumeroBoleta = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdPersonal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personal_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personal_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
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
                    EstadoTramiteId = table.Column<int>(type: "int", nullable: false),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    PersonalId = table.Column<int>(type: "int", nullable: true),
                    TipoTramiteId = table.Column<int>(type: "int", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaConclusion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_Tramite_EstadoTramite_EstadoTramiteId",
                        column: x => x.EstadoTramiteId,
                        principalTable: "EstadoTramite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tramite_Personal_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Personal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tramite_TipoTramite_TipoTramiteId",
                        column: x => x.TipoTramiteId,
                        principalTable: "TipoTramite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleCTCE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Situacion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TieneDictamenesAnteriores = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCTCE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleCTCE_Tramite_Id",
                        column: x => x.Id,
                        principalTable: "Tramite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TramiteId = table.Column<int>(type: "int", nullable: false),
                    EstadoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    Contenido = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documento_EstadoDocumento_EstadoDocumentoId",
                        column: x => x.EstadoDocumentoId,
                        principalTable: "EstadoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documento_Tramite_TramiteId",
                        column: x => x.TramiteId,
                        principalTable: "Tramite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnidadReprobada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetalleCTCEId = table.Column<int>(type: "int", nullable: false),
                    UnidadAprendizajeId = table.Column<int>(type: "int", nullable: false),
                    PeriodoCurso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeriodoRecurse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadReprobada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadReprobada_DetalleCTCE_DetalleCTCEId",
                        column: x => x.DetalleCTCEId,
                        principalTable: "DetalleCTCE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnidadReprobada_UnidadAprendizaje_UnidadAprendizajeId",
                        column: x => x.UnidadAprendizajeId,
                        principalTable: "UnidadAprendizaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", null, "Administrador", "ADMINISTRADOR" },
                    { "22222222-2222-2222-2222-222222222222", null, "Personal", "PERSONAL" },
                    { "33333333-3333-3333-3333-333333333333", null, "Alumno", "ALUMNO" }
                });

            migrationBuilder.InsertData(
                table: "Carrera",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Mecatrónica" },
                    { 2, false, "Telemática" },
                    { 3, false, "Biónica" },
                    { 4, false, "Energía" },
                    { 5, false, "ISISA" }
                });

            migrationBuilder.InsertData(
                table: "EstadoDocumento",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "En Revisión" },
                    { 2, false, "Validado" },
                    { 3, false, "Con errores" },
                    { 4, false, "Documento incorrecto" }
                });

            migrationBuilder.InsertData(
                table: "EstadoTramite",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Solicitado" },
                    { 2, false, "En revision" },
                    { 3, false, "Documentos pendientes" },
                    { 4, false, "Concluido" },
                    { 5, false, "Cancelado" }
                });

            migrationBuilder.InsertData(
                table: "PeriodoConfig",
                columns: new[] { "Id", "AnioFin", "AnioInicio", "IsDeleted", "PeriodoFin", "PeriodoInicio" },
                values: new object[] { 1, 2026, 1997, false, 2, 1 });

            migrationBuilder.InsertData(
                table: "SesionCOSIE",
                columns: new[] { "Id", "FechaSesion", "IsDeleted", "NumeroSesion" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "PRIMERA" },
                    { 2, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "SEGUNDA" },
                    { 3, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "TERCERA" }
                });

            migrationBuilder.InsertData(
                table: "TipoTramite",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Dictamen interno (CTCE)" },
                    { 2, false, "Dictamen externo (CGC)" }
                });

            migrationBuilder.InsertData(
                table: "UnidadAprendizaje",
                columns: new[] { "Id", "CarreraId", "IsDeleted", "Nombre", "Semestre" },
                values: new object[,]
                {
                    { 1, 1, false, "ALGEBRA LINEAL Y NUMEROS COMPLEJOS", 1 },
                    { 2, 1, false, "ANALISIS Y DISEÑO DE PROGRAMAS", 1 },
                    { 3, 1, false, "CALCULO DIFERENCIAL E INTEGRAL", 1 },
                    { 4, 1, false, "CALCULO VECTORIAL", 1 },
                    { 5, 1, false, "CIRCUITOS ELECTRICOS", 1 },
                    { 6, 1, false, "CIRCUITOS ELECTRICOS AVANZADOS", 1 },
                    { 7, 1, false, "DIBUJO ASISTIDO POR COMPUTADORA", 1 },
                    { 8, 1, false, "ECUACIONES DIFERENCIALES", 1 },
                    { 9, 1, false, "ELECTRICIDAD Y MAGNETISMO", 1 },
                    { 10, 1, false, "ESTRUCTURA Y PROPIEDADES DE LOS MATERIALES", 1 },
                    { 11, 1, false, "COMUNICACION ORAL Y ESCRITA", 1 },
                    { 12, 1, false, "FUNDAMENTOS DE ELECTRONICA", 1 },
                    { 13, 1, false, "HERRAMIENTAS COMPUTACIONALES", 1 },
                    { 14, 1, false, "INGLES I", 1 },
                    { 15, 1, false, "INGLES II", 1 },
                    { 16, 1, false, "INTRODUCCION A LA MECATRONICA", 1 },
                    { 17, 1, false, "INTRODUCCION A LA PROGRAMACION", 1 },
                    { 18, 1, false, "MECANICA DE LA PARTICULA", 1 },
                    { 19, 1, false, "MECANICA DEL CUERPO RIGIDO", 1 },
                    { 20, 1, false, "PROCESO DE MANUFACTURA", 1 },
                    { 21, 1, false, "RESISTENCIA DE MATERIALES", 1 },
                    { 22, 1, false, "ADMINISTRACION ORGANIZACIONAL", 2 },
                    { 23, 1, false, "ANALISIS DE SEÑALES Y SISTEMAS", 2 },
                    { 24, 1, false, "ANALISIS Y SINTESIS DE MECANISMOS", 2 },
                    { 25, 1, false, "CIRCUITOS LOGICOS", 2 },
                    { 26, 1, false, "DISEÑO BASICO DE ELEMENTOS DE MAQUINAS", 2 },
                    { 27, 1, false, "DISPOSITIVOS LOGICOS PROGRAMABLES", 2 },
                    { 28, 1, false, "ELECTRONICA ANALOGICA", 2 },
                    { 29, 1, false, "INGLES III", 2 },
                    { 30, 1, false, "LIDERAZGO Y EMPRENDEDORES", 2 },
                    { 31, 1, false, "MANTENIMIENTO Y SISTEMAS DE MANUFACTURA", 2 },
                    { 32, 1, false, "MAQUINAS ELECTRICAS", 2 },
                    { 33, 1, false, "MECANICA DE FLUIDOS", 2 },
                    { 34, 1, false, "MICROPROCESADORES, MICROCONTROLADORES E INTERFAZ", 2 },
                    { 35, 1, false, "NEUMATICA E HIDRAULICA", 2 },
                    { 36, 1, false, "OSCILACIONES Y OPTICA", 2 },
                    { 37, 1, false, "PROBABILIDAD Y ESTADISTICA PARA INGENIERIA", 2 },
                    { 38, 1, false, "PROGRAMACION AVANZADA", 2 },
                    { 39, 1, false, "SENSORES Y ACONDICIONADORES DE SEÑAL", 2 },
                    { 40, 1, false, "SIMULACION ELECTRONICA Y DISEÑO DE CIRCUITOS IMPRESOS", 2 },
                    { 41, 1, false, "SISTEMAS NEURODIFUSOS", 2 },
                    { 42, 1, false, "TEORIA ELECTROMAGNETICA", 2 },
                    { 43, 1, false, "TERMODINAMICA", 2 },
                    { 44, 1, false, "CONTROL CLASICO", 3 },
                    { 45, 1, false, "ETICA PARA EL EJERCICIO PROFESIONAL", 3 },
                    { 46, 1, false, "INSTRUMENTACION VIRTUAL", 3 },
                    { 47, 1, false, "MODELADO Y SIMULACION DE SISTEMAS MECATRONICOS", 3 },
                    { 48, 1, false, "AUTOMATIZACION INDUSTRIAL", 3 },
                    { 49, 1, false, "DISEÑO AVANZADO DE ELEMENTOS DE MAQUINAS", 3 },
                    { 50, 1, false, "FINANZAS E INGENIERIA ECONOMICA", 3 },
                    { 51, 1, false, "PROCESADOR DIGITAL DE SEÑALES", 3 },
                    { 52, 1, false, "INGENIERIA AMBIENTAL", 3 },
                    { 53, 1, false, "ECONOMIA Y LOGISTICA", 3 },
                    { 54, 1, false, "CONTROL DISTRIBUIDO", 3 },
                    { 55, 1, false, "TOPICOS AVANZADOS DE ELECTRONICA", 3 },
                    { 56, 1, false, "DISEÑO AVANZADO Y MANUFACTURA ASISTIDA POR COMPUTADORA", 3 },
                    { 57, 1, false, "PROYECTO INTEGRADOR", 3 },
                    { 58, 1, false, "AUTOMATAS INDUSTRIALES", 3 },
                    { 59, 1, false, "AUTOMATIZACION DE LINEA DE PRODUCCION", 3 },
                    { 60, 1, false, "TOPICOS AVANZADOS DE SENSORES", 3 },
                    { 61, 1, false, "DESARROLLO EMPRESARIAL", 3 },
                    { 62, 1, false, "SEGURIDAD INDUSTRIAL", 3 },
                    { 63, 1, false, "SISTEMAS OPERATIVOS EN TIEMPO REAL", 3 },
                    { 64, 1, false, "GRAFICACION EN 3D", 3 },
                    { 65, 1, false, "PROCESOS INDUSTRIALES", 3 },
                    { 66, 1, false, "PRODUCCION MAS LIMPIA", 3 },
                    { 67, 1, false, "USO Y MANTENIMIENTO DE HERRAMENTAL PARA PROC. DE MANUFACTURA", 3 },
                    { 68, 1, false, "ELECTIVA I", 3 },
                    { 69, 1, false, "SISTEMAS DE CALIDAD PARA LA MANUFACTURA", 3 },
                    { 70, 1, false, "PROTOCOLOS AVANZADOS DE COMUNICACIONES", 3 },
                    { 71, 1, false, "CONTROL DE SISTEMAS MECATRONICOS", 4 },
                    { 72, 1, false, "ELECTRONICA DE POTENCIA", 4 },
                    { 73, 1, false, "INGENIERIA ASISTIDA POR COMPUTADORA", 4 },
                    { 74, 1, false, "METODOLOGIA DE LA INVESTIGACION", 4 },
                    { 75, 1, false, "PROYECTOS DE INVERSION", 4 },
                    { 76, 1, false, "SISTEMAS DE VISION ARTIFICIAL", 4 },
                    { 77, 1, false, "MICROCONTROLADORES AVANZADOS", 4 },
                    { 78, 1, false, "MERCADOTECNIA", 4 },
                    { 79, 1, false, "INTEGRACION DE UN SISTEMA ROBOTICO", 4 },
                    { 80, 1, false, "VISION ARTIFICIAL APLICADA", 4 },
                    { 81, 1, false, "ELECTIVA II", 4 },
                    { 82, 1, false, "PROCESOS AVANZADOS DE MANUFACTURA", 4 },
                    { 83, 1, false, "CONTROL DE SISTEMAS ROBOTICOS", 4 },
                    { 84, 1, false, "IMPLEMENTACION DE SISTEMAS DIGITALES", 4 },
                    { 85, 1, false, "INSTRUMENTACION VIRTUAL APLICADA", 4 },
                    { 86, 1, false, "MANUFACTURA INTEGRADA POR COMPUTADORA", 4 },
                    { 87, 1, false, "TOPICOS AVANZADOS DE AUTOMATIZACION", 4 },
                    { 88, 1, false, "SISTEMAS AVANZADOS DE MANUFACTURA", 4 },
                    { 89, 1, false, "CONTROL DE PROCESOS INDUSTRIALES", 4 },
                    { 90, 1, false, "CONTROL INTELIGENTE", 4 },
                    { 91, 1, false, "DISEÑO DE EQUIPO PARA MANEJO DE MATERIALES", 4 },
                    { 92, 1, false, "DISEÑO ERGONOMICO", 4 },
                    { 93, 1, false, "PROTOCOLOS DE COMUNICACION INDUSTRIAL", 4 },
                    { 94, 1, false, "PROYECTO DE SISTEMAS EMBEBIDOS", 4 },
                    { 95, 1, false, "REALIDAD VIRTUAL", 4 },
                    { 96, 1, false, "SISTEMAS DE PROCESAMIENTO DIGITAL DE SEÑALES", 4 },
                    { 97, 1, false, "TOPICOS AVANZADOS DE SOLDADURA", 4 },
                    { 98, 1, false, "CONTROL DE MAQUINAS ELECTRICAS", 5 },
                    { 99, 1, false, "ELECTIVA III", 5 },
                    { 100, 1, false, "TRABAJO TERMINAL I", 5 },
                    { 101, 1, false, "TRABAJO TERMINAL II", 5 },
                    { 102, 1, false, "SERVICIO SOCIAL", 5 },
                    { 103, 2, false, "PROGRAMACION", 1 },
                    { 104, 2, false, "ANALISIS Y DISEÑO DE SISTEMAS", 1 },
                    { 105, 2, false, "ESTRUCTURA DE DATOS", 1 },
                    { 106, 2, false, "ADMINISTRACION DE SISTEMAS OPERATIVOS", 1 },
                    { 107, 2, false, "DISEÑO DIGITAL", 1 },
                    { 108, 2, false, "ARQUITECTURA DE COMPUTADORAS", 1 },
                    { 109, 2, false, "FUNDAMENTOS DE FISICA", 1 },
                    { 110, 2, false, "ECUACIONES DIFERENCIALES", 1 },
                    { 111, 2, false, "PROBABILIDAD", 1 },
                    { 112, 2, false, "CALCULO DIFERENCIAL E INTEGRAL", 1 },
                    { 113, 2, false, "VARIABLE COMPLEJA", 1 },
                    { 114, 2, false, "ALGEBRA LINEAL", 1 },
                    { 115, 2, false, "ELECTROMAGNETISMO", 1 },
                    { 116, 2, false, "CALCULO MULTIVARIABLE", 1 },
                    { 117, 2, false, "ADMINISTRACION ORGANIZACIONAL", 1 },
                    { 118, 2, false, "ETICA, PROFESION Y SOCIEDAD", 1 },
                    { 119, 2, false, "COMUNICACION ORAL Y ESCRITA", 1 },
                    { 120, 2, false, "INGLES I", 1 },
                    { 121, 2, false, "INGLES II", 1 },
                    { 122, 2, false, "PROGRAMACION ESTRUCTURADA", 1 },
                    { 123, 2, false, "SOCIEDAD, CIENCIA Y TECNOLOGIA", 1 },
                    { 124, 2, false, "SEÑALES Y SISTEMAS", 2 },
                    { 125, 2, false, "PROPAGACION DE ONDAS ELECTROMAGNETICAS", 2 },
                    { 126, 2, false, "ELECTRONICA", 2 },
                    { 127, 2, false, "TEORIA DE LOS CIRCUITOS", 2 },
                    { 128, 2, false, "TEORIA DE LA INFORMACION", 2 },
                    { 129, 2, false, "TEORIA DE LAS COMUNICACIONES", 2 },
                    { 130, 2, false, "COMUNICACIONES DIGITALES", 2 },
                    { 131, 2, false, "PROCESAMIENTO DIGITAL DE SEÑALES", 2 },
                    { 132, 2, false, "TELEFONIA", 2 },
                    { 133, 2, false, "SISTEMAS CELULARES", 2 },
                    { 134, 2, false, "PROTOCOLOS DE INTERNET", 2 },
                    { 135, 2, false, "SISTEMAS DISTRIBUIDOS", 2 },
                    { 136, 2, false, "INGENIERIA WEB", 2 },
                    { 137, 2, false, "PROGRAMACION AVANZADA", 2 },
                    { 138, 2, false, "BASES DE DATOS", 2 },
                    { 139, 2, false, "TRANSMISION DE DATOS", 2 },
                    { 140, 2, false, "INFORMACION FINANCIERA E INGENIERIA ECONOMICA", 2 },
                    { 141, 2, false, "OPTATIVA I", 2 },
                    { 142, 2, false, "INGLES III", 2 },
                    { 143, 2, false, "METODOS NUMERICOS", 2 },
                    { 144, 2, false, "ELECTRONICA PARA COMUNICACIONES", 2 },
                    { 145, 2, false, "OPTICA", 2 },
                    { 146, 2, false, "DESARROLLO SUSTENTABLE", 2 },
                    { 147, 2, false, "ECONOMIA PARA INGENIEROS", 2 },
                    { 148, 2, false, "INGLES IV", 2 },
                    { 149, 2, false, "REDES INALAMBRICAS", 2 },
                    { 150, 2, false, "REDES NEURONALES", 2 },
                    { 151, 2, false, "LOGICA DIFUSA", 2 },
                    { 152, 2, false, "SISTEMAS DE INFORMACION GEOGRAFICA", 2 },
                    { 153, 2, false, "PROGRAMACION DE DISPOSITIVOS MOVILES", 2 },
                    { 154, 2, false, "NORMATIVIDAD EN TELECOMUNICACIONES E INFORMATICA", 2 },
                    { 155, 2, false, "REDES INTELIGENTES", 3 },
                    { 156, 2, false, "LINEAS DE TRANSMISION Y ANTENAS", 3 },
                    { 157, 2, false, "SEGURIDAD EN REDES", 3 },
                    { 158, 2, false, "MULTIMEDIA", 3 },
                    { 159, 2, false, "BASES DE DATOS DISTRIBUIDAS", 3 },
                    { 160, 2, false, "METODOLOGIA DE LA INVESTIGACION", 3 },
                    { 161, 2, false, "ADMINISTRACION DE PROYECTOS", 3 },
                    { 162, 2, false, "LIDERAZGO Y EMPRENDEDORES", 3 },
                    { 163, 2, false, "CRIPTOGRAFIA", 3 },
                    { 164, 2, false, "MICROONDAS", 3 },
                    { 165, 2, false, "PROCESAMIENTO DE IMAGENES", 3 },
                    { 166, 2, false, "TELEVISION DIGITAL", 3 },
                    { 167, 2, false, "SISTEMAS DE CALIDAD", 3 },
                    { 168, 2, false, "PROCESAMIENTO DE VOZ", 3 },
                    { 169, 2, false, "FILTRADO AVANZADO", 3 },
                    { 170, 2, false, "PROYECTO TERMINAL I", 4 },
                    { 171, 2, false, "PROYECTO TERMINAL II", 4 },
                    { 172, 2, false, "REDES DE TELECOMUNICACIONES", 4 },
                    { 173, 2, false, "APLICACIONES DISTRIBUIDAS", 4 },
                    { 174, 2, false, "DISPOSITIVOS PROGRAMABLES", 4 },
                    { 175, 2, false, "SERVICIO SOCIAL", 4 },
                    { 176, 2, false, "ELECTIVA I", 4 },
                    { 177, 2, false, "ELECTIVA II", 4 },
                    { 178, 2, false, "ELECTIVA III", 4 },
                    { 179, 2, false, "ELECTIVA IV", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumno_CarreraId",
                table: "Alumno",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_Alumno_NumeroBoleta",
                table: "Alumno",
                column: "NumeroBoleta",
                unique: true,
                filter: "[NumeroBoleta] IS NOT NULL");

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
                name: "IX_Bitacora_IdentityUserId",
                table: "Bitacora",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrera_Nombre",
                table: "Carrera",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documento_EstadoDocumentoId",
                table: "Documento",
                column: "EstadoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_TramiteId",
                table: "Documento",
                column: "TramiteId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadoDocumento_Nombre",
                table: "EstadoDocumento",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstadoTramite_Nombre",
                table: "EstadoTramite",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FechaRecepcion_SesionId",
                table: "FechaRecepcion",
                column: "SesionId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_UsuarioId",
                table: "Personal",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SesionCOSIE_NumeroSesion",
                table: "SesionCOSIE",
                column: "NumeroSesion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoTramite_Nombre",
                table: "TipoTramite",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_AlumnoId",
                table: "Tramite",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_EstadoTramiteId",
                table: "Tramite",
                column: "EstadoTramiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_PersonalId",
                table: "Tramite",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_TipoTramiteId",
                table: "Tramite",
                column: "TipoTramiteId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadAprendizaje_CarreraId",
                table: "UnidadAprendizaje",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadReprobada_DetalleCTCEId",
                table: "UnidadReprobada",
                column: "DetalleCTCEId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadReprobada_UnidadAprendizajeId",
                table: "UnidadReprobada",
                column: "UnidadAprendizajeId");

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
                name: "Bitacora");

            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "FechaRecepcion");

            migrationBuilder.DropTable(
                name: "PeriodoConfig");

            migrationBuilder.DropTable(
                name: "UnidadReprobada");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EstadoDocumento");

            migrationBuilder.DropTable(
                name: "SesionCOSIE");

            migrationBuilder.DropTable(
                name: "DetalleCTCE");

            migrationBuilder.DropTable(
                name: "UnidadAprendizaje");

            migrationBuilder.DropTable(
                name: "Tramite");

            migrationBuilder.DropTable(
                name: "Alumno");

            migrationBuilder.DropTable(
                name: "EstadoTramite");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "TipoTramite");

            migrationBuilder.DropTable(
                name: "Carrera");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
