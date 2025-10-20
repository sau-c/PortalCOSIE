using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortalCOSIE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CrearDBNet : Migration
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
                name: "DocumentoEstado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentoEstado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoTramite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTramite", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TramiteEstado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TramiteEstado", x => x.Id);
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
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    PersonalId = table.Column<int>(type: "int", nullable: false),
                    TipoTramiteId = table.Column<int>(type: "int", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaConclusion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TramiteEstadoId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Tramite_TramiteEstado_TramiteEstadoId",
                        column: x => x.TramiteEstadoId,
                        principalTable: "TramiteEstado",
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
                    Observaciones = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TramiteId = table.Column<int>(type: "int", nullable: false),
                    DocumentoEstadoId = table.Column<int>(type: "int", nullable: false),
                    Blob = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documento_DocumentoEstado_DocumentoEstadoId",
                        column: x => x.DocumentoEstadoId,
                        principalTable: "DocumentoEstado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "DocumentoEstado",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Sin cargar" },
                    { 2, "Validado" },
                    { 3, "Con errores" },
                    { 4, "Mala calidad" },
                    { 5, "Documento equivocado" },
                    { 6, "Corrupto" }
                });

            migrationBuilder.InsertData(
                table: "TipoTramite",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Dictamen interno (CTE)" },
                    { 2, "Dictamen externo (CGC)" }
                });

            migrationBuilder.InsertData(
                table: "TramiteEstado",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Solicitado" },
                    { 2, "En revision" },
                    { 3, "Documentos pendientes" },
                    { 4, "Concluido" },
                    { 5, "Cacelado" }
                });

            migrationBuilder.InsertData(
                table: "UnidadAprendizaje",
                columns: new[] { "Id", "CarreraId", "Nombre", "Semestre" },
                values: new object[,]
                {
                    { 1, 1, "ALGEBRA LINEAL Y NUMEROS COMPLEJOS", 1 },
                    { 2, 1, "ANALISIS Y DISEÑO DE PROGRAMAS", 1 },
                    { 3, 1, "CALCULO DIFERENCIAL E INTEGRAL", 1 },
                    { 4, 1, "CALCULO VECTORIAL", 1 },
                    { 5, 1, "CIRCUITOS ELECTRICOS", 1 },
                    { 6, 1, "CIRCUITOS ELECTRICOS AVANZADOS", 1 },
                    { 7, 1, "DIBUJO ASISTIDO POR COMPUTADORA", 1 },
                    { 8, 1, "ECUACIONES DIFERENCIALES", 1 },
                    { 9, 1, "ELECTRICIDAD Y MAGNETISMO", 1 },
                    { 10, 1, "ESTRUCTURA Y PROPIEDADES DE LOS MATERIALES", 1 },
                    { 11, 1, "COMUNICACION ORAL Y ESCRITA", 1 },
                    { 12, 1, "FUNDAMENTOS DE ELECTRONICA", 1 },
                    { 13, 1, "HERRAMIENTAS COMPUTACIONALES", 1 },
                    { 14, 1, "INGLES I", 1 },
                    { 15, 1, "INGLES II", 1 },
                    { 16, 1, "INTRODUCCION A LA MECATRONICA", 1 },
                    { 17, 1, "INTRODUCCION A LA PROGRAMACION", 1 },
                    { 18, 1, "MECANICA DE LA PARTICULA", 1 },
                    { 19, 1, "MECANICA DEL CUERPO RIGIDO", 1 },
                    { 20, 1, "PROCESO DE MANUFACTURA", 1 },
                    { 21, 1, "RESISTENCIA DE MATERIALES", 1 },
                    { 22, 1, "ADMINISTRACION ORGANIZACIONAL", 2 },
                    { 23, 1, "ANALISIS DE SEÑALES Y SISTEMAS", 2 },
                    { 24, 1, "ANALISIS Y SINTESIS DE MECANISMOS", 2 },
                    { 25, 1, "CIRCUITOS LOGICOS", 2 },
                    { 26, 1, "DISEÑO BASICO DE ELEMENTOS DE MAQUINAS", 2 },
                    { 27, 1, "DISPOSITIVOS LOGICOS PROGRAMABLES", 2 },
                    { 28, 1, "ELECTRONICA ANALOGICA", 2 },
                    { 29, 1, "INGLES III", 2 },
                    { 30, 1, "LIDERAZGO Y EMPRENDEDORES", 2 },
                    { 31, 1, "MANTENIMIENTO Y SISTEMAS DE MANUFACTURA", 2 },
                    { 32, 1, "MAQUINAS ELECTRICAS", 2 },
                    { 33, 1, "MECANICA DE FLUIDOS", 2 },
                    { 34, 1, "MICROPROCESADORES, MICROCONTROLADORES E INTERFAZ", 2 },
                    { 35, 1, "NEUMATICA E HIDRAULICA", 2 },
                    { 36, 1, "OSCILACIONES Y OPTICA", 2 },
                    { 37, 1, "PROBABILIDAD Y ESTADISTICA PARA INGENIERIA", 2 },
                    { 38, 1, "PROGRAMACION AVANZADA", 2 },
                    { 39, 1, "SENSORES Y ACONDICIONADORES DE SEÑAL", 2 },
                    { 40, 1, "SIMULACION ELECTRONICA Y DISEÑO DE CIRCUITOS IMPRESOS", 2 },
                    { 41, 1, "SISTEMAS NEURODIFUSOS", 2 },
                    { 42, 1, "TEORIA ELECTROMAGNETICA", 2 },
                    { 43, 1, "TERMODINAMICA", 2 },
                    { 44, 1, "CONTROL CLASICO", 3 },
                    { 45, 1, "ETICA PARA EL EJERCICIO PROFESIONAL", 3 },
                    { 46, 1, "INSTRUMENTACION VIRTUAL", 3 },
                    { 47, 1, "MODELADO Y SIMULACION DE SISTEMAS MECATRONICOS", 3 },
                    { 48, 1, "AUTOMATIZACION INDUSTRIAL", 3 },
                    { 49, 1, "DISEÑO AVANZADO DE ELEMENTOS DE MAQUINAS", 3 },
                    { 50, 1, "FINANZAS E INGENIERIA ECONOMICA", 3 },
                    { 51, 1, "PROCESADOR DIGITAL DE SEÑALES", 3 },
                    { 52, 1, "INGENIERIA AMBIENTAL", 3 },
                    { 53, 1, "ECONOMIA Y LOGISTICA", 3 },
                    { 54, 1, "CONTROL DISTRIBUIDO", 3 },
                    { 55, 1, "TOPICOS AVANZADOS DE ELECTRONICA", 3 },
                    { 56, 1, "DISEÑO AVANZADO Y MANUFACTURA ASISTIDA POR COMPUTADORA", 3 },
                    { 57, 1, "PROYECTO INTEGRADOR", 3 },
                    { 58, 1, "AUTOMATAS INDUSTRIALES", 3 },
                    { 59, 1, "AUTOMATIZACION DE LINEA DE PRODUCCION", 3 },
                    { 60, 1, "TOPICOS AVANZADOS DE SENSORES", 3 },
                    { 61, 1, "DESARROLLO EMPRESARIAL", 3 },
                    { 62, 1, "SEGURIDAD INDUSTRIAL", 3 },
                    { 63, 1, "SISTEMAS OPERATIVOS EN TIEMPO REAL", 3 },
                    { 64, 1, "GRAFICACION EN 3D", 3 },
                    { 65, 1, "PROCESOS INDUSTRIALES", 3 },
                    { 66, 1, "PRODUCCION MAS LIMPIA", 3 },
                    { 67, 1, "USO Y MANTENIMIENTO DE HERRAMENTAL PARA PROC. DE MANUFACTURA", 3 },
                    { 68, 1, "ELECTIVA I", 3 },
                    { 69, 1, "SISTEMAS DE CALIDAD PARA LA MANUFACTURA", 3 },
                    { 70, 1, "PROTOCOLOS AVANZADOS DE COMUNICACIONES", 3 },
                    { 71, 1, "CONTROL DE SISTEMAS MECATRONICOS", 4 },
                    { 72, 1, "ELECTRONICA DE POTENCIA", 4 },
                    { 73, 1, "INGENIERIA ASISTIDA POR COMPUTADORA", 4 },
                    { 74, 1, "METODOLOGIA DE LA INVESTIGACION", 4 },
                    { 75, 1, "PROYECTOS DE INVERSION", 4 },
                    { 76, 1, "SISTEMAS DE VISION ARTIFICIAL", 4 },
                    { 77, 1, "MICROCONTROLADORES AVANZADOS", 4 },
                    { 78, 1, "MERCADOTECNIA", 4 },
                    { 79, 1, "INTEGRACION DE UN SISTEMA ROBOTICO", 4 },
                    { 80, 1, "VISION ARTIFICIAL APLICADA", 4 },
                    { 81, 1, "ELECTIVA II", 4 },
                    { 82, 1, "PROCESOS AVANZADOS DE MANUFACTURA", 4 },
                    { 83, 1, "CONTROL DE SISTEMAS ROBOTICOS", 4 },
                    { 84, 1, "IMPLEMENTACION DE SISTEMAS DIGITALES", 4 },
                    { 85, 1, "INSTRUMENTACION VIRTUAL APLICADA", 4 },
                    { 86, 1, "MANUFACTURA INTEGRADA POR COMPUTADORA", 4 },
                    { 87, 1, "TOPICOS AVANZADOS DE AUTOMATIZACION", 4 },
                    { 88, 1, "SISTEMAS AVANZADOS DE MANUFACTURA", 4 },
                    { 89, 1, "CONTROL DE PROCESOS INDUSTRIALES", 4 },
                    { 90, 1, "CONTROL INTELIGENTE", 4 },
                    { 91, 1, "DISEÑO DE EQUIPO PARA MANEJO DE MATERIALES", 4 },
                    { 92, 1, "DISEÑO ERGONOMICO", 4 },
                    { 93, 1, "PROTOCOLOS DE COMUNICACION INDUSTRIAL", 4 },
                    { 94, 1, "PROYECTO DE SISTEMAS EMBEBIDOS", 4 },
                    { 95, 1, "REALIDAD VIRTUAL", 4 },
                    { 96, 1, "SISTEMAS DE PROCESAMIENTO DIGITAL DE SEÑALES", 4 },
                    { 97, 1, "TOPICOS AVANZADOS DE SOLDADURA", 4 },
                    { 98, 1, "CONTROL DE MAQUINAS ELECTRICAS", 5 },
                    { 99, 1, "ELECTIVA III", 5 },
                    { 100, 1, "TRABAJO TERMINAL I", 5 },
                    { 101, 1, "TRABAJO TERMINAL II", 5 },
                    { 102, 1, "SERVICIO SOCIAL", 5 },
                    { 103, 2, "PROGRAMACION", 1 },
                    { 104, 2, "ANALISIS Y DISEÑO DE SISTEMAS", 1 },
                    { 105, 2, "ESTRUCTURA DE DATOS", 1 },
                    { 106, 2, "ADMINISTRACION DE SISTEMAS OPERATIVOS", 1 },
                    { 107, 2, "DISEÑO DIGITAL", 1 },
                    { 108, 2, "ARQUITECTURA DE COMPUTADORAS", 1 },
                    { 109, 2, "FUNDAMENTOS DE FISICA", 1 },
                    { 110, 2, "ECUACIONES DIFERENCIALES", 1 },
                    { 111, 2, "PROBABILIDAD", 1 },
                    { 112, 2, "CALCULO DIFERENCIAL E INTEGRAL", 1 },
                    { 113, 2, "VARIABLE COMPLEJA", 1 },
                    { 114, 2, "ALGEBRA LINEAL", 1 },
                    { 115, 2, "ELECTROMAGNETISMO", 1 },
                    { 116, 2, "CALCULO MULTIVARIABLE", 1 },
                    { 117, 2, "ADMINISTRACION ORGANIZACIONAL", 1 },
                    { 118, 2, "ETICA, PROFESION Y SOCIEDAD", 1 },
                    { 119, 2, "COMUNICACION ORAL Y ESCRITA", 1 },
                    { 120, 2, "INGLES I", 1 },
                    { 121, 2, "INGLES II", 1 },
                    { 122, 2, "PROGRAMACION ESTRUCTURADA", 1 },
                    { 123, 2, "SOCIEDAD, CIENCIA Y TECNOLOGIA", 1 },
                    { 124, 2, "SEÑALES Y SISTEMAS", 2 },
                    { 125, 2, "PROPAGACION DE ONDAS ELECTROMAGNETICAS", 2 },
                    { 126, 2, "ELECTRONICA", 2 },
                    { 127, 2, "TEORIA DE LOS CIRCUITOS", 2 },
                    { 128, 2, "TEORIA DE LA INFORMACION", 2 },
                    { 129, 2, "TEORIA DE LAS COMUNICACIONES", 2 },
                    { 130, 2, "COMUNICACIONES DIGITALES", 2 },
                    { 131, 2, "PROCESAMIENTO DIGITAL DE SEÑALES", 2 },
                    { 132, 2, "TELEFONIA", 2 },
                    { 133, 2, "SISTEMAS CELULARES", 2 },
                    { 134, 2, "PROTOCOLOS DE INTERNET", 2 },
                    { 135, 2, "SISTEMAS DISTRIBUIDOS", 2 },
                    { 136, 2, "INGENIERIA WEB", 2 },
                    { 137, 2, "PROGRAMACION AVANZADA", 2 },
                    { 138, 2, "BASES DE DATOS", 2 },
                    { 139, 2, "TRANSMISION DE DATOS", 2 },
                    { 140, 2, "INFORMACION FINANCIERA E INGENIERIA ECONOMICA", 2 },
                    { 141, 2, "OPTATIVA I", 2 },
                    { 142, 2, "INGLES III", 2 },
                    { 143, 2, "METODOS NUMERICOS", 2 },
                    { 144, 2, "ELECTRONICA PARA COMUNICACIONES", 2 },
                    { 145, 2, "OPTICA", 2 },
                    { 146, 2, "DESARROLLO SUSTENTABLE", 2 },
                    { 147, 2, "ECONOMIA PARA INGENIEROS", 2 },
                    { 148, 2, "INGLES IV", 2 },
                    { 149, 2, "REDES INALAMBRICAS", 2 },
                    { 150, 2, "REDES NEURONALES", 2 },
                    { 151, 2, "LOGICA DIFUSA", 2 },
                    { 152, 2, "SISTEMAS DE INFORMACION GEOGRAFICA", 2 },
                    { 153, 2, "PROGRAMACION DE DISPOSITIVOS MOVILES", 2 },
                    { 154, 2, "NORMATIVIDAD EN TELECOMUNICACIONES E INFORMATICA", 2 },
                    { 155, 2, "REDES INTELIGENTES", 3 },
                    { 156, 2, "LINEAS DE TRANSMISION Y ANTENAS", 3 },
                    { 157, 2, "SEGURIDAD EN REDES", 3 },
                    { 158, 2, "MULTIMEDIA", 3 },
                    { 159, 2, "BASES DE DATOS DISTRIBUIDAS", 3 },
                    { 160, 2, "METODOLOGIA DE LA INVESTIGACION", 3 },
                    { 161, 2, "ADMINISTRACION DE PROYECTOS", 3 },
                    { 162, 2, "LIDERAZGO Y EMPRENDEDORES", 3 },
                    { 163, 2, "CRIPTOGRAFIA", 3 },
                    { 164, 2, "MICROONDAS", 3 },
                    { 165, 2, "PROCESAMIENTO DE IMAGENES", 3 },
                    { 166, 2, "TELEVISION DIGITAL", 3 },
                    { 167, 2, "SISTEMAS DE CALIDAD", 3 },
                    { 168, 2, "PROCESAMIENTO DE VOZ", 3 },
                    { 169, 2, "FILTRADO AVANZADO", 3 },
                    { 171, 2, "PROYECTO TERMINAL I", 4 },
                    { 172, 2, "PROYECTO TERMINAL II", 4 },
                    { 173, 2, "REDES DE TELECOMUNICACIONES", 4 },
                    { 174, 2, "APLICACIONES DISTRIBUIDAS", 4 },
                    { 175, 2, "DISPOSITIVOS PROGRAMABLES", 4 },
                    { 176, 2, "SERVICIO SOCIAL", 4 },
                    { 177, 2, "ELECTIVA I", 4 },
                    { 178, 2, "ELECTIVA II", 4 },
                    { 179, 2, "ELECTIVA III", 4 },
                    { 180, 2, "ELECTIVA IV", 4 }
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
                name: "IX_Documento_DocumentoEstadoId",
                table: "Documento",
                column: "DocumentoEstadoId");

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
                name: "IX_Tramite_TramiteEstadoId",
                table: "Tramite",
                column: "TramiteEstadoId");

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
                name: "TipoTramite");

            migrationBuilder.DropTable(
                name: "UnidadAprendizaje");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DocumentoEstado");

            migrationBuilder.DropTable(
                name: "Tramite");

            migrationBuilder.DropTable(
                name: "Alumno");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "TramiteEstado");

            migrationBuilder.DropTable(
                name: "Carrera");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
