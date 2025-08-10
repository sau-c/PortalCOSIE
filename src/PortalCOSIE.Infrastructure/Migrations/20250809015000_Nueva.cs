using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortalCOSIE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Nueva : Migration
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
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanEstudio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanEstudio", x => x.Id);
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
                name: "Alumno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NumeroBoleta = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarreraId = table.Column<int>(type: "int", nullable: false),
                    PlanEstudioId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_Alumno_PlanEstudio_PlanEstudioId",
                        column: x => x.PlanEstudioId,
                        principalTable: "PlanEstudio",
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
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000001", 0, "00000000-0000-0000-0000-000000000001", "correo0@gmail.com", true, false, null, "CORREO0@GMAIL.COM", "CORREO0@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-000000000001", false, "correo0@gmail.com" },
                    { "00000000-0000-0000-0000-0000000000010", 0, "00000000-0000-0000-0000-0000000000010", "correo9@gmail.com", true, false, null, "CORREO9@GMAIL.COM", "CORREO9@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-0000000000010", false, "correo9@gmail.com" },
                    { "00000000-0000-0000-0000-0000000000011", 0, "00000000-0000-0000-0000-0000000000011", "correo10@gmail.com", true, false, null, "CORREO10@GMAIL.COM", "CORREO10@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-0000000000011", false, "correo10@gmail.com" },
                    { "00000000-0000-0000-0000-0000000000012", 0, "00000000-0000-0000-0000-0000000000012", "correo11@gmail.com", true, false, null, "CORREO11@GMAIL.COM", "CORREO11@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-0000000000012", false, "correo11@gmail.com" },
                    { "00000000-0000-0000-0000-0000000000013", 0, "00000000-0000-0000-0000-0000000000013", "correo12@gmail.com", true, false, null, "CORREO12@GMAIL.COM", "CORREO12@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-0000000000013", false, "correo12@gmail.com" },
                    { "00000000-0000-0000-0000-0000000000014", 0, "00000000-0000-0000-0000-0000000000014", "correo13@gmail.com", true, false, null, "CORREO13@GMAIL.COM", "CORREO13@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-0000000000014", false, "correo13@gmail.com" },
                    { "00000000-0000-0000-0000-0000000000015", 0, "00000000-0000-0000-0000-0000000000015", "correo14@gmail.com", true, false, null, "CORREO14@GMAIL.COM", "CORREO14@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-0000000000015", false, "correo14@gmail.com" },
                    { "00000000-0000-0000-0000-0000000000016", 0, "00000000-0000-0000-0000-0000000000016", "correo15@gmail.com", true, false, null, "CORREO15@GMAIL.COM", "CORREO15@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-0000000000016", false, "correo15@gmail.com" },
                    { "00000000-0000-0000-0000-0000000000017", 0, "00000000-0000-0000-0000-0000000000017", "correo16@gmail.com", true, false, null, "CORREO16@GMAIL.COM", "CORREO16@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-0000000000017", false, "correo16@gmail.com" },
                    { "00000000-0000-0000-0000-0000000000018", 0, "00000000-0000-0000-0000-0000000000018", "correo17@gmail.com", true, false, null, "CORREO17@GMAIL.COM", "CORREO17@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-0000000000018", false, "correo17@gmail.com" },
                    { "00000000-0000-0000-0000-0000000000019", 0, "00000000-0000-0000-0000-0000000000019", "correo18@gmail.com", true, false, null, "CORREO18@GMAIL.COM", "CORREO18@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-0000000000019", false, "correo18@gmail.com" },
                    { "00000000-0000-0000-0000-000000000002", 0, "00000000-0000-0000-0000-000000000002", "correo1@gmail.com", true, false, null, "CORREO1@GMAIL.COM", "CORREO1@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-000000000002", false, "correo1@gmail.com" },
                    { "00000000-0000-0000-0000-0000000000020", 0, "00000000-0000-0000-0000-0000000000020", "correo19@gmail.com", true, false, null, "CORREO19@GMAIL.COM", "CORREO19@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-0000000000020", false, "correo19@gmail.com" },
                    { "00000000-0000-0000-0000-000000000003", 0, "00000000-0000-0000-0000-000000000003", "correo2@gmail.com", true, false, null, "CORREO2@GMAIL.COM", "CORREO2@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-000000000003", false, "correo2@gmail.com" },
                    { "00000000-0000-0000-0000-000000000004", 0, "00000000-0000-0000-0000-000000000004", "correo3@gmail.com", true, false, null, "CORREO3@GMAIL.COM", "CORREO3@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-000000000004", false, "correo3@gmail.com" },
                    { "00000000-0000-0000-0000-000000000005", 0, "00000000-0000-0000-0000-000000000005", "correo4@gmail.com", true, false, null, "CORREO4@GMAIL.COM", "CORREO4@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-000000000005", false, "correo4@gmail.com" },
                    { "00000000-0000-0000-0000-000000000006", 0, "00000000-0000-0000-0000-000000000006", "correo5@gmail.com", true, false, null, "CORREO5@GMAIL.COM", "CORREO5@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-000000000006", false, "correo5@gmail.com" },
                    { "00000000-0000-0000-0000-000000000007", 0, "00000000-0000-0000-0000-000000000007", "correo6@gmail.com", true, false, null, "CORREO6@GMAIL.COM", "CORREO6@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-000000000007", false, "correo6@gmail.com" },
                    { "00000000-0000-0000-0000-000000000008", 0, "00000000-0000-0000-0000-000000000008", "correo7@gmail.com", true, false, null, "CORREO7@GMAIL.COM", "CORREO7@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-000000000008", false, "correo7@gmail.com" },
                    { "00000000-0000-0000-0000-000000000009", 0, "00000000-0000-0000-0000-000000000009", "correo8@gmail.com", true, false, null, "CORREO8@GMAIL.COM", "CORREO8@GMAIL.COM", "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", null, false, "00000000-0000-0000-0000-000000000009", false, "correo8@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Carrera",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Carrera centrada en la optimización de procesos tecnologicos e industriales.", "Mecatrónica" },
                    { 2, "Carrera enfocada en el desarrollo de telecomunicaciones y sistemas computacionales.", "Telemática" },
                    { 3, "Carrera centrada en la optimización de procesos.", "Biónica" },
                    { 4, "Carrera centrada en la optimización de procesos.", "Energía" },
                    { 5, "Carrera centrada en la optimización de procesos.", "ISISA" }
                });

            migrationBuilder.InsertData(
                table: "PlanEstudio",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Primer plan", "1998" },
                    { 2, "Segundo plan", "2009" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "ApellidoMaterno", "ApellidoPaterno", "IdentityUserId", "Nombre" },
                values: new object[,]
                {
                    { 1, "ApellidoMaterno0", "ApellidoPaterno0", "00000000-0000-0000-0000-000000000001", "Nombre0" },
                    { 2, "ApellidoMaterno1", "ApellidoPaterno1", "00000000-0000-0000-0000-000000000002", "Nombre1" },
                    { 3, "ApellidoMaterno2", "ApellidoPaterno2", "00000000-0000-0000-0000-000000000003", "Nombre2" },
                    { 4, "ApellidoMaterno3", "ApellidoPaterno3", "00000000-0000-0000-0000-000000000004", "Nombre3" },
                    { 5, "ApellidoMaterno4", "ApellidoPaterno4", "00000000-0000-0000-0000-000000000005", "Nombre4" },
                    { 6, "ApellidoMaterno5", "ApellidoPaterno5", "00000000-0000-0000-0000-000000000006", "Nombre5" },
                    { 7, "ApellidoMaterno6", "ApellidoPaterno6", "00000000-0000-0000-0000-000000000007", "Nombre6" },
                    { 8, "ApellidoMaterno7", "ApellidoPaterno7", "00000000-0000-0000-0000-000000000008", "Nombre7" },
                    { 9, "ApellidoMaterno8", "ApellidoPaterno8", "00000000-0000-0000-0000-000000000009", "Nombre8" },
                    { 10, "ApellidoMaterno9", "ApellidoPaterno9", "00000000-0000-0000-0000-0000000000010", "Nombre9" },
                    { 11, "ApellidoMaterno10", "ApellidoPaterno10", "00000000-0000-0000-0000-0000000000011", "Nombre10" },
                    { 12, "ApellidoMaterno11", "ApellidoPaterno11", "00000000-0000-0000-0000-0000000000012", "Nombre11" },
                    { 13, "ApellidoMaterno12", "ApellidoPaterno12", "00000000-0000-0000-0000-0000000000013", "Nombre12" },
                    { 14, "ApellidoMaterno13", "ApellidoPaterno13", "00000000-0000-0000-0000-0000000000014", "Nombre13" },
                    { 15, "ApellidoMaterno14", "ApellidoPaterno14", "00000000-0000-0000-0000-0000000000015", "Nombre14" },
                    { 16, "ApellidoMaterno15", "ApellidoPaterno15", "00000000-0000-0000-0000-0000000000016", "Nombre15" },
                    { 17, "ApellidoMaterno16", "ApellidoPaterno16", "00000000-0000-0000-0000-0000000000017", "Nombre16" },
                    { 18, "ApellidoMaterno17", "ApellidoPaterno17", "00000000-0000-0000-0000-0000000000018", "Nombre17" },
                    { 19, "ApellidoMaterno18", "ApellidoPaterno18", "00000000-0000-0000-0000-0000000000019", "Nombre18" },
                    { 20, "ApellidoMaterno19", "ApellidoPaterno19", "00000000-0000-0000-0000-0000000000020", "Nombre19" }
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
                name: "IX_Alumno_PlanEstudioId",
                table: "Alumno",
                column: "PlanEstudioId");

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
                name: "PlanEstudio");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
