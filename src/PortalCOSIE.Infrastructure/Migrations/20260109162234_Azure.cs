using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortalCOSIE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Azure : Migration
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
                    AnioActual = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    PeriodoActual = table.Column<int>(type: "int", maxLength: 1, nullable: false),
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
                name: "TipoDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.Id);
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
                    Id = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
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
                    IdEmpleado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                    PeriodoSolicitud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaConclusion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TramiteId = table.Column<int>(type: "int", nullable: false),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    EstadoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    HashOriginal = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
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
                        name: "FK_Documento_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumento",
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
                name: "TramiteCTCE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Peticion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TieneDictamenesAnteriores = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TramiteCTCE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TramiteCTCE_Tramite_Id",
                        column: x => x.Id,
                        principalTable: "Tramite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnidadReprobada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TramiteCTCEId = table.Column<int>(type: "int", nullable: false),
                    UnidadAprendizajeId = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    PeriodoCurso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeriodoRecurse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadReprobada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadReprobada_TramiteCTCE_TramiteCTCEId",
                        column: x => x.TramiteCTCEId,
                        principalTable: "TramiteCTCE",
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
                    { 1, false, "En revisión" },
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
                    { 5, false, "Cancelado" },
                    { 6, false, "Esperando acuse" }
                });

            migrationBuilder.InsertData(
                table: "PeriodoConfig",
                columns: new[] { "Id", "AnioActual", "AnioInicio", "IsDeleted", "PeriodoActual", "PeriodoInicio" },
                values: new object[] { 1, 2027, 1997, false, 1, 1 });

            migrationBuilder.InsertData(
                table: "SesionCOSIE",
                columns: new[] { "Id", "FechaSesion", "IsDeleted", "NumeroSesion" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "PRIMERA" },
                    { 2, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "SEGUNDA" },
                    { 3, new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "TERCERA" }
                });

            migrationBuilder.InsertData(
                table: "TipoDocumento",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Identificación" },
                    { 2, false, "Boleta global" },
                    { 3, false, "Carta exposición de motivos" },
                    { 4, false, "Probatorios" },
                    { 5, false, "Dictamen CTCE" }
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
                table: "FechaRecepcion",
                columns: new[] { "Id", "Fecha", "IsDeleted", "SesionId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1 },
                    { 2, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1 },
                    { 3, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2 },
                    { 4, new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2 },
                    { 5, new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3 }
                });

            migrationBuilder.InsertData(
                table: "UnidadAprendizaje",
                columns: new[] { "Id", "CarreraId", "IsDeleted", "Nombre", "Semestre" },
                values: new object[,]
                {
                    { "A739", 5, false, "TECNOLOGIA DE MATERIALES AUTOMOTRIZ", 7 },
                    { "A740", 5, false, "TECNOLOGIA DE LA SOLDADURA", 7 },
                    { "A741", 5, false, "PROCESOS DE CONFORMADO", 7 },
                    { "A742", 5, false, "TOPICOS SELECTOS DE INGENIERIA I", 7 },
                    { "A843", 5, false, "TECNICAS DE CARACTERIZACION EN MATERIALES AUTOMOTRICES", 8 },
                    { "A844", 5, false, "TECNOLOGIA DE LA UNION EN MATERIALES AUTOMOTRICES", 8 },
                    { "A845", 5, false, "ENSAYOS SELECTOS DE MATERIALES AUTOMOTRICES", 8 },
                    { "A846", 5, false, "TOPICOS SELECTOS DE INGENIERIA II", 8 },
                    { "B101", 3, false, "BIOLOGIA CELULAR", 1 },
                    { "B102", 3, false, "QUIMICA ORGANICA", 1 },
                    { "B103", 3, false, "ALGEBRA LINEAL", 1 },
                    { "B104", 3, false, "CALCULO DIFERENCIAL E INTEGRAL", 1 },
                    { "B105", 3, false, "CALCULO VECTORIAL", 1 },
                    { "B106", 3, false, "FUNDAMENTOS DE FISICA PARA LA INGENIERIA", 1 },
                    { "B107", 3, false, "FUNDAMENTOS DE TEORIA ELECTROMAGNETICA", 1 },
                    { "B108", 3, false, "BIOESTADISTICA", 1 },
                    { "B109", 3, false, "METROLOGIA", 1 },
                    { "B110", 3, false, "PROGRAMACION ORIENTADA A OBJETOS", 1 },
                    { "B111", 3, false, "ANATOMIA", 1 },
                    { "B112", 3, false, "HERRAMIENTAS COMPUTACIONALES", 1 },
                    { "B113", 3, false, "ELECTIVA I", 1 },
                    { "B114", 3, false, "BIOETICA", 1 },
                    { "B115", 3, false, "DESARROLLO SOSTENIBLE", 1 },
                    { "B116", 3, false, "INGLES I", 1 },
                    { "B201", 3, false, "ECUACIONES DIFERENCIALES", 2 },
                    { "B202", 3, false, "FUNDAMENTOS MATEMATICOS DE INGENIERIA", 2 },
                    { "B203", 3, false, "ONDAS ELECTROMAGNETICAS Y SISTEMAS RADIANTES", 2 },
                    { "B204", 3, false, "FISICA MODERNA Y OPTICA", 2 },
                    { "B205", 3, false, "FISICOQUIMICA", 2 },
                    { "B206", 3, false, "BIOQUIMICA", 2 },
                    { "B207", 3, false, "TEORIA DE LOS CIRCUITOS", 2 },
                    { "B208", 3, false, "FISIOLOGIA", 2 },
                    { "B209", 3, false, "BIOFISICA", 2 },
                    { "B210", 3, false, "BIOLOGIA MOLECULAR", 2 },
                    { "B211", 3, false, "INVESTIGACION Y DESARROLLO DE PROYECTOS", 2 },
                    { "B212", 3, false, "SISTEMAS DE GESTION DE CALIDAD", 2 },
                    { "B213", 3, false, "BIOGNOSIS", 2 },
                    { "B214", 3, false, "ELECTIVA II", 2 },
                    { "B215", 3, false, "LIDERAZGO Y EMPRENDEDORES", 2 },
                    { "B216", 3, false, "INGLES II", 2 },
                    { "B301", 3, false, "ANALISIS NUMERICO", 3 },
                    { "B302", 3, false, "DISPOSITIVOS ELECTRONICOS", 3 },
                    { "B303", 3, false, "ELECTRONICA DIGITAL", 3 },
                    { "B304", 3, false, "TEORIA DEL CONTROL", 3 },
                    { "B305", 3, false, "BIOMATERIALES", 3 },
                    { "B306", 3, false, "CONTROL NEURODIFUSO", 3 },
                    { "B307", 3, false, "COMUNICACION ORAL Y ESCRITA", 3 },
                    { "B308", 3, false, "INGLES III", 3 },
                    { "B309", 3, false, "ELECTRONICA ANALOGICA Y DE POTENCIA", 3 },
                    { "B310", 3, false, "PROCESAMIENTO DE IMAGENES", 3 },
                    { "B311", 3, false, "BIOMAGNETISMO", 3 },
                    { "B312", 3, false, "MECANISMOS BIOMIMETICOS", 3 },
                    { "B313", 3, false, "BIOINSTRUMENTACION", 3 },
                    { "B314", 3, false, "PROCESAMIENTO DE SEÑALES BIOLOGICAS", 3 },
                    { "B315", 3, false, "SENSORES Y ACTUADORES", 3 },
                    { "B316", 3, false, "CONTROL DE SISTEMAS BIOLOGICOS", 3 },
                    { "B317", 3, false, "PROTESIS BIOMIMETICAS", 3 },
                    { "B318", 3, false, "VISION ARTIFICIAL", 3 },
                    { "B319", 3, false, "INSTRUMENTACION BIOMEDICA", 3 },
                    { "B320", 3, false, "INTELIGENCIA ARTIFICIAL", 3 },
                    { "B321", 3, false, "ROBOTICA EVOLUTIVA Y AUTONOMA", 3 },
                    { "B322", 3, false, "SIMULACION DE ANALISIS DE ESFUERZOS", 3 },
                    { "B323", 3, false, "ERGONOMIA Y BIODINAMICA", 3 },
                    { "B324", 3, false, "INSTRUMENTACION CLINICA Y LABORATORIO", 3 },
                    { "B401", 3, false, "MODELADO Y CONTROL DE SISTEMAS BIONICOS", 4 },
                    { "B402", 3, false, "DISPOSITIVOS PROGRAMABLES", 4 },
                    { "B403", 3, false, "ANALISIS DE ESFUERZOS", 4 },
                    { "B404", 3, false, "MANUFACTURA DE ELEMENTOS BIOMIMETICOS", 4 },
                    { "B405", 3, false, "RECONOCIMIENTO DE PATRONES", 4 },
                    { "B406", 3, false, "NORMATIVIDAD Y GESTION TECNOLOGICA", 4 },
                    { "B407", 3, false, "INSTRUMENTACION BIOTECNOLOGICA", 4 },
                    { "B408", 3, false, "BIOSENSORES Y BIOCHIPS", 4 },
                    { "B409", 3, false, "INGENIERIA DEL CONTROL HUMANO", 4 },
                    { "B410", 3, false, "SISTEMAS BIOMECANICOS", 4 },
                    { "B411", 3, false, "ELECTIVA III", 4 },
                    { "B412", 3, false, "SISTEMAS SENSORIALES", 4 },
                    { "B413", 3, false, "NEUROROBOTICA", 4 },
                    { "B414", 3, false, "TELEMETRIA MEDICA", 4 },
                    { "B415", 3, false, "IMAGENOLOGIA", 4 },
                    { "B501", 3, false, "BIOROBOTICA", 5 },
                    { "B502", 3, false, "BIOELECTRONICA", 5 },
                    { "B503", 3, false, "BIOMECANICA", 5 },
                    { "B504", 3, false, "METODOLOGIA DE LA INVESTIGACION", 5 },
                    { "B505", 3, false, "TRABAJO TERMINAL I", 5 },
                    { "B506", 3, false, "TRABAJO TERMINAL II", 5 },
                    { "B507", 3, false, "ELECTIVA IV", 5 },
                    { "B508", 3, false, "SERVICIO SOCIAL", 5 },
                    { "C739", 5, false, "TEORIA DE CONTROL", 7 },
                    { "C740", 5, false, "ELECTRONICA OPERACIONAL Y DE POTENCIA", 7 },
                    { "C741", 5, false, "INTERFACES Y MICRO CONTROLADORES", 7 },
                    { "C742", 5, false, "TOPICOS SELECTOS DE INGENIERIA I", 7 },
                    { "C843", 5, false, "AUTOMOVILES ELECTRICOS", 8 },
                    { "C844", 5, false, "INSTRUMENTACION AUTOMOTRIZ", 8 },
                    { "C845", 5, false, "SISTEMAS DE CONTROL DE MODELOS AUTOMOTRICES", 8 },
                    { "C846", 5, false, "TOPICOS SELECTOS DE INGENIERIA II", 8 },
                    { "D739", 5, false, "AERODINAMICA DEL AUTOMOVIL I", 7 },
                    { "D740", 5, false, "ERGONOMIA", 7 },
                    { "D741", 5, false, "DINAMICA DE FLUIDOS COMPUTACIONAL", 7 },
                    { "D742", 5, false, "TOPICOS SELECTOS DE INGENIERIA I", 7 },
                    { "D843", 5, false, "AERODINAMICA DEL AUTOMOVIL II", 8 },
                    { "D844", 5, false, "SEGURIDAD CONFORT DEL VEHICULO", 8 },
                    { "D845", 5, false, "DESARROLLO DIGITAL DE VEHICULOS", 8 },
                    { "D846", 5, false, "TOPICOS SELECTOS DE INGENIERIA II", 8 },
                    { "E101", 4, false, "QUIMICA INORGÁNICA", 1 },
                    { "E102", 4, false, "CÁCULO DIFERENCIAL E INTEGRAL", 1 },
                    { "E103", 4, false, "ÁLGEBRA LÍNEAL", 1 },
                    { "E104", 4, false, "HERRAMIENTAS COMPUTACIONALES", 1 },
                    { "E105", 4, false, "COMUNICACIÓN ORAL Y ESCRITA", 1 },
                    { "E106", 4, false, "ESTRUCTURA DE LA MATERIA", 1 },
                    { "E107", 4, false, "DISEÑO DE EXPERIMENTOS", 1 },
                    { "E201", 4, false, "MECÁNICA", 2 },
                    { "E202", 4, false, "ECUACIONES DIFERENCIALES", 2 },
                    { "E203", 4, false, "QUÍMICA ORGÁNICA", 2 },
                    { "E204", 4, false, "CÁLCULO VECTORIAL", 2 },
                    { "E205", 4, false, "ÉTICA Y RESPONSABILIDAD SOCIAL", 2 },
                    { "E206", 4, false, "PROCESOS DE MANUFACTURA", 2 },
                    { "E301", 4, false, "ELECTRICIDAD Y MAGNETISMO", 3 },
                    { "E302", 4, false, "TERMODINÁMICA", 3 },
                    { "E303", 4, false, "VARIABLE COMPLEJA", 3 },
                    { "E304", 4, false, "PROGRAMACIÓN AVANZADA", 3 },
                    { "E305", 4, false, "ENERGÍAS CONVENCIONALES Y RENOVABLES", 3 },
                    { "E306", 4, false, "PROBABILIDAD Y ESTADÍSTICA", 3 },
                    { "E307", 4, false, "SOLUCIÓN DE PROBLEMAS Y CREATIVIDAD", 3 },
                    { "E401", 4, false, "CONVERSIÓN Y ALMACENAMIENTO DE ENERGÍA", 4 },
                    { "E402", 4, false, "CIRCUITOS ELÉCTRICOS", 4 },
                    { "E403", 4, false, "MÉTODOS NUMÉRICOS", 4 },
                    { "E404", 4, false, "TEORÍA ELECTROMAGNÉTICA", 4 },
                    { "E405", 4, false, "FENÓMENOS DE TRANSPORTES", 4 },
                    { "E406", 4, false, "BALANCES DE MATERIA Y ENERGÍA", 4 },
                    { "E407", 4, false, "NORMATIVIDAD Y POLÍTICA ENERGÉTICA", 4 },
                    { "E501", 4, false, "ELECTRÓNICA DE POTENCIA", 5 },
                    { "E502", 4, false, "FISÍCA AVANZADA", 5 },
                    { "E503", 4, false, "HIGIENE, SEGURIDAD, Y RIESGOS INDUSTRIALES", 5 },
                    { "E504", 4, false, "MECÁNICA DE FLUIDOS", 5 },
                    { "E505", 4, false, "ECONOMÍA, RECURSOS Y NECESIDADES ENERGETÍCAS DE MÉXICO", 5 },
                    { "E506", 4, false, "EFICIENCIA ENERGÉTICA", 5 },
                    { "E507", 4, false, "TRANSFERENCIA DE CALOR", 5 },
                    { "E601", 4, false, "SISTEMAS DE CONTROL", 6 },
                    { "E602", 4, false, "DESARROLLO SUSTENTABLE", 6 },
                    { "E603", 4, false, "FÍSICA DEL ESTADO SÓLIDO", 6 },
                    { "E604", 4, false, "INGENIERÍA DE LA ENERGÍA NÚCLEAR", 6 },
                    { "E605", 4, false, "GENERACIÓN Y CO-GENERACIÓN DE ENERGÍA", 6 },
                    { "E606", 4, false, "INGENIERÍA DE LA ENERGÍA HIDRÁULICA", 6 },
                    { "E607", 4, false, "COMBUSTIBLES FÓSILES", 6 },
                    { "E701", 4, false, "INNOVACIÓN TECNOLÓGICA", 7 },
                    { "E702", 4, false, "INGENIERÍA DE LA ENERGÍA SOLAR", 7 },
                    { "E703", 4, false, "INGENIERÍA DE LA ENERGÍA EÓLICA", 7 },
                    { "E705", 4, false, "METODOLOGÍA DE LA INVESTIGACIÓN", 7 },
                    { "E706", 4, false, "SISTEMAS BIOENERGÉTICOS", 7 },
                    { "E707", 4, false, "EMPRENDIMIENTO Y LIDERAZGO", 7 },
                    { "E721", 4, false, "DISEÑO DE SISTEMAS ENERGÉTICOS", 7 },
                    { "E722", 4, false, "GESTIÓN ENERGÉTICA", 7 },
                    { "E723", 4, false, "MATERIALES CATALÍTICOS AVANZADOS", 7 },
                    { "E724", 4, false, "NUEVAS TECNOLOGÍAS EN COMBUSTIBLES CONVENCIONALES", 7 },
                    { "E725", 4, false, "SIMULACIÓN Y OPTIMIZACIÓN DE PROCESOS", 7 },
                    { "E726", 4, false, "PRODUCCIÓN DE BIOCOMBUSTIBLES", 7 },
                    { "E727", 4, false, "INGENIERÍA DE CELDAS SOLARES", 7 },
                    { "E728", 4, false, "GENERADORES Y TURBINAS", 7 },
                    { "E739", 5, false, "PROCESOS INDUSTRIALES AUTOMOTRICES", 7 },
                    { "E740", 5, false, "LOGISTICA DE PRODUCCION AUTOMOTRIZ", 7 },
                    { "E741", 5, false, "SISTEMAS DE GESTION DE CALIDDAD DE LA PRODUCCION AUTOMOTRIZ", 7 },
                    { "E742", 5, false, "TOPICOS SELECTOS DE INGENIERIA I", 7 },
                    { "E802", 4, false, "FORMULACIÓN Y EVALUACIÓN DE PROYECTOS", 8 },
                    { "E803", 4, false, "NANOMATERIALES EN LA ENERGÍA", 8 },
                    { "E804", 4, false, "INGENIERÍA DE LA ENERGÍA GEOTÉRMICA", 8 },
                    { "E805", 4, false, "TRABAJO TERMINAL I", 8 },
                    { "E806", 4, false, "TECNOLOGÍA DEL HIDRÓGENO", 8 },
                    { "E807", 4, false, "INTEGRACIÓN A LA RED ELÉCTRICA Y SISTEMAS AISLADOS", 8 },
                    { "E821", 4, false, "DISEÑO DE SISTEMAS ENERGÉTICOS", 8 },
                    { "E822", 4, false, "GESTIÓN ENERGÉTICA", 8 },
                    { "E823", 4, false, "MATERIALES CATALÍTICOS AVANZADOS", 8 },
                    { "E824", 4, false, "NUEVAS TECNOLOGÍAS EN COMBUSTIBLES CONVENCIONALES", 8 },
                    { "E825", 4, false, "SIMULACIÓN Y OPTIMIZACIÓN DE PROCESOS", 8 },
                    { "E826", 4, false, "PRODUCCIÓN DE BIOCOMBUSTIBLES", 8 },
                    { "E827", 4, false, "INGENIERÍA DE CELDAS SOLARES", 8 },
                    { "E828", 4, false, "GENERADORES Y TURBINAS", 8 },
                    { "E843", 5, false, "ORGANIZACION E IMPLEMENTACION DE LA EMPRESA DE PRODUC. AUTO.", 8 },
                    { "E844", 5, false, "ADMINISTRACION DE SRVICIOS DE LA AGENCIA DE VENTA Y POSVENTA", 8 },
                    { "E845", 5, false, "ADMINISTRACION GENERAL AUTOMOTIVA", 8 },
                    { "E846", 5, false, "TOPICOS SELECTOS DE INGENIERIA II", 8 },
                    { "E901", 4, false, "TRABAJO TERMINAL II", 9 },
                    { "E903", 4, false, "PRODUCCIÓN MÁS LIMPIA", 9 },
                    { "E904", 4, false, "INGENIERÍA ECONÓMICA", 9 },
                    { "E905", 4, false, "SOSTENIBILIDAD CORPORATIVA", 9 },
                    { "E921", 4, false, "DISEÑO DE SISTEMAS ENERGÉTICOS", 9 },
                    { "E922", 4, false, "GESTIÓN ENERGÉTICA", 9 },
                    { "E923", 4, false, "MATERIALES CATALÍTICOS AVANZADOS", 9 },
                    { "E924", 4, false, "NUEVAS TECNOLOGÍAS EN COMBUSTIBLES CONVENCIONALES", 9 },
                    { "E925", 4, false, "SIMULACIÓN Y OPTIMIZACIÓN DE PROCESOS", 9 },
                    { "E926", 4, false, "PRODUCCIÓN DE BIOCOMBUSTIBLES", 9 },
                    { "E927", 4, false, "INGENIERÍA DE CELDAS SOLARES", 9 },
                    { "E928", 4, false, "GENERADORES Y TURBINAS", 9 },
                    { "M101", 1, false, "ALGEBRA LINEAL Y NUMEROS COMPLEJOS", 1 },
                    { "M102", 1, false, "ANALISIS Y DISEÑO DE PROGRAMAS", 1 },
                    { "M103", 1, false, "CALCULO DIFERENCIAL E INTEGRAL", 1 },
                    { "M104", 1, false, "CALCULO VECTORIAL", 1 },
                    { "M105", 1, false, "CIRCUITOS ELECTRICOS", 1 },
                    { "M106", 1, false, "CIRCUITOS ELECTRICOS AVANZADOS", 1 },
                    { "M107", 1, false, "DIBUJO ASISTIDO POR COMPUTADORA", 1 },
                    { "M108", 1, false, "ECUACIONES DIFERENCIALES", 1 },
                    { "M109", 1, false, "ELECTRICIDAD Y MAGNETISMO", 1 },
                    { "M110", 1, false, "ESTRUCTURA Y PROPIEDADES DE LOS MATERIALES", 1 },
                    { "M111", 1, false, "COMUNICACION ORAL Y ESCRITA", 1 },
                    { "M112", 1, false, "FUNDAMENTOS DE ELECTRONICA", 1 },
                    { "M113", 1, false, "HERRAMIENTAS COMPUTACIONALES", 1 },
                    { "M114", 1, false, "INGLES I", 1 },
                    { "M115", 1, false, "INGLES II", 1 },
                    { "M116", 1, false, "INTRODUCCION A LA MECATRONICA", 1 },
                    { "M117", 1, false, "INTRODUCCION A LA PROGRAMACION", 1 },
                    { "M118", 1, false, "MECANICA DE LA PARTICULA", 1 },
                    { "M119", 1, false, "MECANICA DEL CUERPO RIGIDO", 1 },
                    { "M120", 1, false, "PROCESO DE MANUFACTURA", 1 },
                    { "M121", 1, false, "RESISTENCIA DE MATERIALES", 1 },
                    { "M201", 1, false, "ADMINISTRACION ORGANIZACIONAL", 2 },
                    { "M202", 1, false, "ANALISIS DE SEÑALES Y SISTEMAS", 2 },
                    { "M203", 1, false, "ANALISIS Y SINTESIS DE MECANISMOS", 2 },
                    { "M204", 1, false, "CIRCUITOS LOGICOS", 2 },
                    { "M205", 1, false, "DISEÑO BASICO DE ELEMENTOS DE MAQUINAS", 2 },
                    { "M206", 1, false, "DISPOSITIVOS LOGICOS PROGRAMABLES", 2 },
                    { "M207", 1, false, "ELECTRONICA ANALOGICA", 2 },
                    { "M208", 1, false, "INGLES III", 2 },
                    { "M209", 1, false, "LIDERAZGO Y EMPRENDEDORES", 2 },
                    { "M210", 1, false, "MANTENIMIENTO Y SISTEMAS DE MANUFACTURA", 2 },
                    { "M211", 1, false, "MAQUINAS ELECTRICAS", 2 },
                    { "M212", 1, false, "MECANICA DE FLUIDOS", 2 },
                    { "M213", 1, false, "MICROPROCESADORES, MICROCONTROLADORES E INTERFAZ", 2 },
                    { "M214", 1, false, "NEUMATICA E HIDRAULICA", 2 },
                    { "M215", 1, false, "OSCILACIONES Y OPTICA", 2 },
                    { "M216", 1, false, "PROBABILIDAD Y ESTADISTICA PARA INGENIERIA", 2 },
                    { "M217", 1, false, "PROGRAMACION AVANZADA", 2 },
                    { "M218", 1, false, "SENSORES Y ACONDICIONADORES DE SEÑAL", 2 },
                    { "M219", 1, false, "SIMULACION ELECTRONICA Y DISEÑO DE CIRCUITOS IMPRESOS", 2 },
                    { "M220", 1, false, "SISTEMAS NEURODIFUSOS", 2 },
                    { "M221", 1, false, "TEORIA ELECTROMAGNETICA", 2 },
                    { "M222", 1, false, "TERMODINAMICA", 2 },
                    { "M301", 1, false, "CONTROL CLASICO", 3 },
                    { "M302", 1, false, "ETICA PARA EL EJERCICIO PROFESIONAL", 3 },
                    { "M303", 1, false, "INSTRUMENTACION VIRTUAL", 3 },
                    { "M304", 1, false, "MODELADO Y SIMULACION DE SISTEMAS MECATRONICOS", 3 },
                    { "M305", 1, false, "AUTOMATIZACION INDUSTRIAL", 3 },
                    { "M306", 1, false, "DISEÑO AVANZADO DE ELEMENTOS DE MAQUINAS", 3 },
                    { "M307", 1, false, "FINANZAS E INGENIERIA ECONOMICA", 3 },
                    { "M308", 1, false, "PROCESADOR DIGITAL DE SEÑALES", 3 },
                    { "M309", 1, false, "INGENIERIA AMBIENTAL", 3 },
                    { "M310", 1, false, "ECONOMIA Y LOGISTICA", 3 },
                    { "M311", 1, false, "CONTROL DISTRIBUIDO", 3 },
                    { "M312", 1, false, "TOPICOS AVANZADOS DE ELECTRONICA", 3 },
                    { "M313", 1, false, "DISEÑO AVANZADO Y MANUFACTURA ASISTIDA POR COMPUTADORA", 3 },
                    { "M314", 1, false, "PROYECTO INTEGRADOR", 3 },
                    { "M315", 1, false, "AUTOMATAS INDUSTRIALES", 3 },
                    { "M316", 1, false, "AUTOMATIZACION DE LINEA DE PRODUCCION", 3 },
                    { "M317", 1, false, "TOPICOS AVANZADOS DE SENSORES", 3 },
                    { "M318", 1, false, "DESARROLLO EMPRESARIAL", 3 },
                    { "M319", 1, false, "SEGURIDAD INDUSTRIAL", 3 },
                    { "M320", 1, false, "SISTEMAS OPERATIVOS EN TIEMPO REAL", 3 },
                    { "M321", 1, false, "GRAFICACION EN 3D", 3 },
                    { "M322", 1, false, "PROCESOS INDUSTRIALES", 3 },
                    { "M323", 1, false, "PRODUCCION MAS LIMPIA", 3 },
                    { "M324", 1, false, "USO Y MANTENIMIENTO DE HERRAMENTAL PARA PROC. DE MANUFACTURA", 3 },
                    { "M325", 1, false, "ELECTIVA I", 3 },
                    { "M327", 1, false, "SISTEMAS DE CALIDAD PARA LA MANUFACTURA", 3 },
                    { "M328", 1, false, "PROTOCOLOS AVANZADOS DE COMUNICACIONES", 3 },
                    { "M401", 1, false, "CONTROL DE SISTEMAS MECATRONICOS", 4 },
                    { "M402", 1, false, "ELECTRONICA DE POTENCIA", 4 },
                    { "M403", 1, false, "INGENIERIA ASISTIDA POR COMPUTADORA", 4 },
                    { "M404", 1, false, "METODOLOGIA DE LA INVESTIGACION", 4 },
                    { "M405", 1, false, "PROYECTOS DE INVERSION", 4 },
                    { "M406", 1, false, "SISTEMAS DE VISION ARTIFICIAL", 4 },
                    { "M407", 1, false, "MICROCONTROLADORES AVANZADOS", 4 },
                    { "M408", 1, false, "MERCADOTECNIA", 4 },
                    { "M409", 1, false, "INTEGRACION DE UN SISTEMA ROBOTICO", 4 },
                    { "M410", 1, false, "VISION ARTIFICIAL APLICADA", 4 },
                    { "M411", 1, false, "ELECTIVA II", 4 },
                    { "M412", 1, false, "PROCESOS AVANZADOS DE MANUFACTURA", 4 },
                    { "M413", 1, false, "CONTROL DE SISTEMAS ROBOTICOS", 4 },
                    { "M414", 1, false, "IMPLEMENTACION DE SISTEMAS DIGITALES", 4 },
                    { "M415", 1, false, "INSTRUMENTACION VIRTUAL APLICADA", 4 },
                    { "M416", 1, false, "MANUFACTURA INTEGRADA POR COMPUTADORA", 4 },
                    { "M417", 1, false, "TOPICOS AVANZADOS DE AUTOMATIZACION", 4 },
                    { "M418", 1, false, "SISTEMAS AVANZADOS DE MANUFACTURA", 4 },
                    { "M419", 1, false, "CONTROL DE PROCESOS INDUSTRIALES", 4 },
                    { "M420", 1, false, "CONTROL INTELIGENTE", 4 },
                    { "M421", 1, false, "DISEÑO DE EQUIPO PARA MANEJO DE MATERIALES", 4 },
                    { "M422", 1, false, "DISEÑO ERGONOMICO", 4 },
                    { "M423", 1, false, "PROTOCOLOS DE COMUNICACION INDUSTRIAL", 4 },
                    { "M424", 1, false, "PROYECTO DE SISTEMAS EMBEBIDOS", 4 },
                    { "M425", 1, false, "REALIDAD VIRTUAL", 4 },
                    { "M426", 1, false, "SISTEMAS DE PROCESAMIENTO DIGITAL DE SEÑALES", 4 },
                    { "M427", 1, false, "TOPICOS AVANZADOS DE SOLDADURA", 4 },
                    { "M501", 1, false, "CONTROL DE MAQUINAS ELECTRICAS", 5 },
                    { "M502", 1, false, "ELECTIVA III", 5 },
                    { "M503", 1, false, "TRABAJO TERMINAL I", 5 },
                    { "M504", 1, false, "TRABAJO TERMINAL II", 5 },
                    { "M505", 1, false, "SERVICIO SOCIAL", 5 },
                    { "M739", 5, false, "TECNICAS DE MECANIZADO", 7 },
                    { "M740", 5, false, "INGENIERIA DE AUTOPARTES", 7 },
                    { "M741", 5, false, "METODOS DE FABRICACION", 7 },
                    { "M742", 5, false, "TOPICOS SELECTOS DE INGENIERIA I", 7 },
                    { "M843", 5, false, "SISTEMAS FLEXIBLES DE MANUFACTURA", 8 },
                    { "M844", 5, false, "DISEÑO DE HERRAMENTAL AUTOMOTRIZ", 8 },
                    { "M845", 5, false, "ROBOTICA AUTOMOTRIZ", 8 },
                    { "M846", 5, false, "TOPICOS SELECTOS DE INGENIERIA II", 8 },
                    { "N703", 4, false, "MATERIALES CATALÍTICOS AVANZADOS", 7 },
                    { "N704", 4, false, "NUEVAS TECNOLOGÍAS EN COMBUSTIBLES CONVENCIONALES", 7 },
                    { "N801", 4, false, "DISEÑO DE SISTEMAS ENERGÉTICOS", 8 },
                    { "N802", 4, false, "GESTIÓN ENERGÉTICA", 8 },
                    { "N803", 4, false, "MATERIALES CATALÍTICOS AVANZADOS", 8 },
                    { "N804", 4, false, "NUEVAS TECNOLOGÍAS EN COMBUSTIBLES CONVENCIONALES", 8 },
                    { "N901", 4, false, "DISEÑO DE SISTEMAS ENERGÉTICOS", 9 },
                    { "N902", 4, false, "GESTIÓN ENERGÉTICA", 9 },
                    { "N903", 4, false, "MATERIALES CATALÍTICOS", 9 },
                    { "N904", 4, false, "NUEVAS TECNOLOGÍAS EN COMBUSTIBLE CONVENCIONALES", 9 },
                    { "O739", 5, false, "SISTEMAS DIGITALES", 7 },
                    { "O740", 5, false, "ARQUITECTURAS EMBEBIDAS AUTOMOTRICES", 7 },
                    { "O741", 5, false, "APLICACIONES CON MICROCONTROLADORES PARA EL AUTOMOVIL", 7 },
                    { "O742", 5, false, "TOPICOS SELECTOS DE INGENIERIA I", 7 },
                    { "O843", 5, false, "COMUNICACIONES EMBEBIDAS AUTOMOTRICES", 8 },
                    { "O844", 5, false, "PROGRAMA DE DISPOSITIVOS MOVILES AUTOMOTRICES", 8 },
                    { "O845", 5, false, "SISTEMAS INTELIGENTES DEL AUTOMOVIL", 8 },
                    { "O846", 5, false, "TOPICOS SELECTOS DE INGENIERIA II", 8 },
                    { "R701", 4, false, "SIMULACIÓN Y OPTIMIZACIÓN DE PROCESOS", 7 },
                    { "R702", 4, false, "PRODUCCIÓN DE BIOCOMBUSTIBLES", 7 },
                    { "R703", 4, false, "INGENIERÍA DE CELDAS SOLARES", 7 },
                    { "R704", 4, false, "GENERADORES Y TURBINAS", 7 },
                    { "R801", 4, false, "SIMULACIÓN Y OPTIMIZACIÓN DE PROCESOS", 8 },
                    { "R802", 4, false, "PRODUCCIÓN DE BIOCOMBUSTIBLES", 8 },
                    { "R803", 4, false, "INGENIERÍA DE CELDAS SOLARES", 8 },
                    { "R804", 4, false, "GENERADORES Y TURBINAS", 8 },
                    { "R901", 4, false, "SIMULACIÓN Y OPTIMIZACIÓN DE PROCESOS", 9 },
                    { "R902", 4, false, "PRODUCCIÓN DE BIOCOMBUSTIBLES", 9 },
                    { "R903", 4, false, "INGENIERIA DE CELDAS SOLARES", 9 },
                    { "R904", 4, false, "GENERADORES Y TURBINAS", 9 },
                    { "S101", 5, false, "CÁLCULO DIFERENCIAL E INTEGRAL", 1 },
                    { "S102", 5, false, "FÍSICA CLÁSICA", 1 },
                    { "S103", 5, false, "FUNDAMENTOS DE ÁLGEBRA", 1 },
                    { "S104", 5, false, "FUNDAMENTOS DE PROGRAMACIÓN", 1 },
                    { "S105", 5, false, "HUMANIDADES I: ING. CIENCIA Y SOCIEDAD", 1 },
                    { "S106", 5, false, "QUÍMICA BÁSICA", 1 },
                    { "S207", 5, false, "CÁLCULO VECTORIAL", 2 },
                    { "S208", 5, false, "ECUACIONES DIFERENCIALES", 2 },
                    { "S209", 5, false, "ELECTRICIDAD Y MAGNETISMO", 2 },
                    { "S210", 5, false, "HUMANIDADES II: LA COMUNICACIÓN Y LA INGENIERÍA", 2 },
                    { "S211", 5, false, "MÉTODOS NUMÉRICOS", 2 },
                    { "S212", 5, false, "QUÍMICA APLICADA", 2 },
                    { "S313", 5, false, "ANÁLISIS DE CIRCUITOS DE C.D. Y C.A", 3 },
                    { "S314", 5, false, "DINÁMICA DE FLUIDOS", 3 },
                    { "S315", 5, false, "ESTÁTICA", 3 },
                    { "S316", 5, false, "HUMANIDADES III: DESARROLLO HUMANO", 3 },
                    { "S317", 5, false, "TERMODINÁMICA I", 3 },
                    { "S318", 5, false, "INTRO. A LA CIENCIA DE LOS MATERIALES", 3 },
                    { "S419", 5, false, "DINÁMICA", 4 },
                    { "S420", 5, false, "ELECTRÓNICA I", 4 },
                    { "S421", 5, false, "TERMODINÁMICA II", 4 },
                    { "S422", 5, false, "OLEONEUMATICA", 4 },
                    { "S423", 5, false, "PROBABILIDAD Y ESTADÍSTICA", 4 },
                    { "S424", 5, false, "RESISTENCIA DE LOS MATERIALES I", 4 },
                    { "S525", 5, false, "ELEMENTOS MECÁNICOS AUT.", 5 },
                    { "S526", 5, false, "SISTEMAS AUTOMOTRICES", 5 },
                    { "S527", 5, false, "TRANSFERENCIA DE CALOR", 5 },
                    { "S528", 5, false, "ELECTRICIDAD Y ELECTRÓNICA AUT.", 5 },
                    { "S529", 5, false, "MOD. Y SIM. ASIT. POR COMP.", 5 },
                    { "S530", 5, false, "METROLOGÍA Y NORMALIZACIÓN", 5 },
                    { "S631", 5, false, "DISEÑO AUTOMOTRIZ", 6 },
                    { "S632", 5, false, "DINÁMICA DEL VEHICULO", 6 },
                    { "S633", 5, false, "MOTORES DE COMBUSTIÓN INTERNA", 6 },
                    { "S634", 5, false, "SISTEMAS DE DIRECCIÓN, SUSPENSIÓN Y FRENOS", 6 },
                    { "S635", 5, false, "TREN MOTRIZ", 6 },
                    { "S636", 5, false, "PROCESOS DE MANUFACTURA AUTOMOTRIZ", 6 },
                    { "S737", 5, false, "INGENIERÍA AMBIENTAL AUTOMOTRIZ", 7 },
                    { "S738", 5, false, "HUMANIDADES IV: DESARROLLO PERSONAL Y PROFESIONAL", 7 },
                    { "S739", 5, false, "SENSORES AUTOMOTRICES Y ACONDICIONADORES DE SEÑAL", 7 },
                    { "S740", 5, false, "PROGRAMACION DE SISTEMAS INMERSOS", 7 },
                    { "S741", 5, false, "MICROCOMPUTADORAS AUTOMOTRICES I", 7 },
                    { "S742", 5, false, "TOPICOS SELECTOS DE INGENIERIA I", 7 },
                    { "S843", 5, false, "SENSORES AUTOMOTRICES Y ACONDICIONADORES DE SEÑAL II", 8 },
                    { "S844", 5, false, "CONTROL DE ACTUADORES AUTOMOTRICES", 8 },
                    { "S845", 5, false, "MICROCOMPUTADORAS AUTOMOTRICES II", 8 },
                    { "S846", 5, false, "TOPICOS SELECTOS DE INGENIERIA II", 8 },
                    { "S848", 5, false, "EVALUACION ECONOMICA DE PROYECTOS", 8 },
                    { "S849", 5, false, "HUMANIDADES V: EL HUMANISMO FRENTE A LA GLOBALIZACION", 8 },
                    { "S950", 5, false, "PROYECTO INTEGRADOR", 9 },
                    { "T101", 2, false, "PROGRAMACION", 1 },
                    { "T102", 2, false, "ANALISIS Y DISEÑO DE SISTEMAS", 1 },
                    { "T103", 2, false, "ESTRUCTURA DE DATOS", 1 },
                    { "T104", 2, false, "ADMINISTRACION DE SISTEMAS OPERATIVOS", 1 },
                    { "T105", 2, false, "DISEÑO DIGITAL", 1 },
                    { "T106", 2, false, "ARQUITECTURA DE COMPUTADORAS", 1 },
                    { "T107", 2, false, "FUNDAMENTOS DE FISICA", 1 },
                    { "T108", 2, false, "ECUACIONES DIFERENCIALES", 1 },
                    { "T109", 2, false, "PROBABILIDAD", 1 },
                    { "T110", 2, false, "CALCULO DIFERENCIAL E INTEGRAL", 1 },
                    { "T111", 2, false, "VARIABLE COMPLEJA", 1 },
                    { "T112", 2, false, "ALGEBRA LINEAL", 1 },
                    { "T113", 2, false, "ELECTROMAGNETISMO", 1 },
                    { "T114", 2, false, "CALCULO MULTIVARIABLE", 1 },
                    { "T115", 2, false, "ADMINISTRACION ORGANIZACIONAL", 1 },
                    { "T116", 2, false, "ETICA, PROFESION Y SOCIEDAD", 1 },
                    { "T117", 2, false, "COMUNICACION ORAL Y ESCRITA", 1 },
                    { "T118", 2, false, "INGLES I", 1 },
                    { "T119", 2, false, "INGLES II", 1 },
                    { "T120", 2, false, "PROGRAMACION ESTRUCTURADA", 1 },
                    { "T121", 2, false, "SOCIEDAD, CIENCIA Y TECNOLOGIA", 1 },
                    { "T201", 2, false, "SEÑALES Y SISTEMAS", 2 },
                    { "T202", 2, false, "PROPAGACION DE ONDAS ELECTROMAGNETICAS", 2 },
                    { "T203", 2, false, "ELECTRONICA", 2 },
                    { "T204", 2, false, "TEORIA DE LOS CIRCUITOS", 2 },
                    { "T205", 2, false, "TEORIA DE LA INFORMACION", 2 },
                    { "T206", 2, false, "TEORIA DE LAS COMUNICACIONES", 2 },
                    { "T207", 2, false, "COMUNICACIONES DIGITALES", 2 },
                    { "T208", 2, false, "PROCESAMIENTO DIGITAL DE SEÑALES", 2 },
                    { "T209", 2, false, "TELEFONIA", 2 },
                    { "T210", 2, false, "SISTEMAS CELULARES", 2 },
                    { "T211", 2, false, "PROTOCOLOS DE INTERNET", 2 },
                    { "T212", 2, false, "SISTEMAS DISTRIBUIDOS", 2 },
                    { "T213", 2, false, "INGENIERIA WEB", 2 },
                    { "T214", 2, false, "PROGRAMACION AVANZADA", 2 },
                    { "T215", 2, false, "BASES DE DATOS", 2 },
                    { "T216", 2, false, "TRANSMISION DE DATOS", 2 },
                    { "T217", 2, false, "INFORMACION FINANCIERA E INGENIERIA ECONOMICA", 2 },
                    { "T218", 2, false, "OPTATIVA I", 2 },
                    { "T219", 2, false, "INGLES III", 2 },
                    { "T220", 2, false, "METODOS NUMERICOS", 2 },
                    { "T221", 2, false, "ELECTRONICA PARA COMUNICACIONES", 2 },
                    { "T222", 2, false, "OPTICA", 2 },
                    { "T223", 2, false, "DESARROLLO SUSTENTABLE", 2 },
                    { "T224", 2, false, "ECONOMIA PARA INGENIEROS", 2 },
                    { "T225", 2, false, "INGLES IV", 2 },
                    { "T226", 2, false, "REDES INALAMBRICAS", 2 },
                    { "T227", 2, false, "REDES NEURONALES", 2 },
                    { "T228", 2, false, "LOGICA DIFUSA", 2 },
                    { "T229", 2, false, "SISTEMAS DE INFORMACION GEOGRAFICA", 2 },
                    { "T230", 2, false, "PROGRAMACION DE DISPOSITIVOS MOVILES", 2 },
                    { "T231", 2, false, "NORMATIVIDAD EN TELECOMUNICACIONES E INFORMATICA", 2 },
                    { "T301", 2, false, "REDES INTELIGENTES", 3 },
                    { "T302", 2, false, "LINEAS DE TRANSMISION Y ANTENAS", 3 },
                    { "T303", 2, false, "SEGURIDAD EN REDES", 3 },
                    { "T304", 2, false, "MULTIMEDIA", 3 },
                    { "T305", 2, false, "BASES DE DATOS DISTRIBUIDAS", 3 },
                    { "T306", 2, false, "METODOLOGIA DE LA INVESTIGACION", 3 },
                    { "T307", 2, false, "ADMINISTRACION DE PROYECTOS", 3 },
                    { "T308", 2, false, "LIDERAZGO Y EMPRENDEDORES", 3 },
                    { "T310", 2, false, "CRIPTOGRAFIA", 3 },
                    { "T311", 2, false, "MICROONDAS", 3 },
                    { "T312", 2, false, "PROCESAMIENTO DE IMAGENES", 3 },
                    { "T313", 2, false, "TELEVISION DIGITAL", 3 },
                    { "T314", 2, false, "SISTEMAS DE CALIDAD", 3 },
                    { "T315", 2, false, "PROCESAMIENTO DE VOZ", 3 },
                    { "T316", 2, false, "FILTRADO AVANZADO", 3 },
                    { "T401", 2, false, "PROYECTO TERMINAL I", 4 },
                    { "T402", 2, false, "PROYECTO TERMINAL II", 4 },
                    { "T403", 2, false, "REDES DE TELECOMUNICACIONES", 4 },
                    { "T404", 2, false, "APLICACIONES DISTRIBUIDAS", 4 },
                    { "T405", 2, false, "DISPOSITIVOS PROGRAMABLES", 4 },
                    { "T406", 2, false, "SERVICIO SOCIAL", 4 },
                    { "T407", 2, false, "ELECTIVA I", 4 },
                    { "T408", 2, false, "ELECTIVA II", 4 },
                    { "T409", 2, false, "ELECTIVA III", 4 },
                    { "T410", 2, false, "ELECTIVA IV", 4 },
                    { "T739", 5, false, "AIRE ACONDICIONADO", 7 },
                    { "T740", 5, false, "ANALISIS VIBRATORIO DE VEHICULOS", 7 },
                    { "T741", 5, false, "ELEMENTO FINITO", 7 },
                    { "T742", 5, false, "TOPICOS SELECTOS DE INGENIERIA I", 7 },
                    { "T843", 5, false, "TRIBOLOGIA", 8 },
                    { "T844", 5, false, "CONTROL DE RUIDO Y VIBRACIONES", 8 },
                    { "T845", 5, false, "INGENIERIA DE PROYECTOS", 8 },
                    { "T846", 5, false, "TOPICOS SELECTOS DE INGENIERIA II", 8 },
                    { "V739", 5, false, "ENERGOTECNIA", 7 },
                    { "V740", 5, false, "ADMINISTRACION DEL TALLER AUTOMOTRIZ", 7 },
                    { "V741", 5, false, "INGENIERIA DE MANTENIMIENTO", 7 },
                    { "V742", 5, false, "TOPICOS SELECTOS DE INGENIERIA I", 7 },
                    { "V843", 5, false, "ANALISIS DE FALLAS AUTOMOTRICES", 8 },
                    { "V844", 5, false, "EQUIPO PESADO AUTOMOTRIZ", 8 },
                    { "V845", 5, false, "OPTIMATIZACION DE SISTEMAS INTEGRALES MECANICOS DE VEHICULOS", 8 },
                    { "V846", 5, false, "TOPICOS SELECTOS DE INGENIERIA II", 8 }
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
                name: "IX_Documento_TipoDocumentoId",
                table: "Documento",
                column: "TipoDocumentoId");

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
                name: "IX_SesionCOSIE_NumeroSesion",
                table: "SesionCOSIE",
                column: "NumeroSesion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoDocumento_Nombre",
                table: "TipoDocumento",
                column: "Nombre",
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
                name: "IX_UnidadReprobada_TramiteCTCEId",
                table: "UnidadReprobada",
                column: "TramiteCTCEId");

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
                name: "TipoDocumento");

            migrationBuilder.DropTable(
                name: "SesionCOSIE");

            migrationBuilder.DropTable(
                name: "TramiteCTCE");

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
