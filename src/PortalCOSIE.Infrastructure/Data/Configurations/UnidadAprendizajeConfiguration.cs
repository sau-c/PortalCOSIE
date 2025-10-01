using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class UnidadAprendizajeConfiguration : IEntityTypeConfiguration<UnidadAprendizaje>
    {
        public void Configure(EntityTypeBuilder<UnidadAprendizaje> builder)
        {
            builder.ToTable("UnidadAprendizaje");

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Semestre)
                .IsRequired();

            builder.HasData(
                new UnidadAprendizaje(1, "Programaci�n", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(2, "An�lisis y dise�o de sistemas", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(3, "Estructura de datos", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(4, "Administraci�n de sistemas operativos", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(5, "Dise�o digital", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(6, "Arquitectura de computadoras", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(7, "Fundamentos de f�sica", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(8, "Ecuaciones diferenciales", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(9, "Probabilidad", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(10, "C�lculo diferencial e integral", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(11, "Variable compleja", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(12, "�lgebra lineal", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(13, "Electromagnetismo", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(14, "C�lculo multivariable", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(15, "Administraci�n organizacional", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(16, "�tica, profesi�n y sociedad", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(17, "Comunicaci�n oral y escrita", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(18, "Ingl�s I", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(19, "Ingl�s II", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(20, "Programaci�n estructurada", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(21, "Sociedad, ciencia y tecnolog�a", 2, Semestre.PRIMERO),

                new UnidadAprendizaje(22, "Se�ales y sistemas", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(23, "Propagaci�n de ondas electromagn�ticas", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(24, "Electr�nica", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(25, "Teor�a de los circuitos", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(26, "Teor�a de la informaci�n", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(27, "Teor�a de las comunicaciones", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(28, "Comunicaciones digitales", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(29, "Procesamiento digital de se�ales", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(30, "Telefon�a", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(31, "Sistemas celulares", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(32, "Protocolos de Internet", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(33, "Sistemas distribuidos", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(34, "Ingenier�a web", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(35, "Programaci�n avanzada", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(36, "Bases de datos", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(37, "Transmisi�n de datos", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(38, "Informaci�n financiera e ingenier�a econ�mica", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(39, "Optativa I", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(40, "Ingl�s III", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(41, "M�todos num�ricos", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(42, "Electr�nica para comunicaciones", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(43, "�ptica", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(44, "Desarrollo sustentable", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(45, "Econom�a para ingenieros", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(46, "Ingl�s IV", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(47, "Redes inal�mbricas", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(48, "Redes neuronales", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(49, "L�gica difusa", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(50, "Sistemas de informaci�n geogr�fica", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(51, "Programaci�n de dispositivos m�viles", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(52, "Normatividad en telecomunicaciones e inform�tica", 2, Semestre.SEGUNDO),

                new UnidadAprendizaje(53, "Redes inteligentes", 2, Semestre.TERCERO),
                new UnidadAprendizaje(54, "L�neas de transmisi�n y antenas", 2, Semestre.TERCERO),
                new UnidadAprendizaje(55, "Seguridad en redes", 2, Semestre.TERCERO),
                new UnidadAprendizaje(56, "Multimedia", 2, Semestre.TERCERO),
                new UnidadAprendizaje(57, "Bases de datos distribuidas", 2, Semestre.TERCERO),
                new UnidadAprendizaje(58, "Metodolog�a de la investigaci�n", 2, Semestre.TERCERO),
                new UnidadAprendizaje(59, "Administraci�n de proyectos", 2, Semestre.TERCERO),
                new UnidadAprendizaje(60, "Liderazgo y emprendedores", 2, Semestre.TERCERO),
                new UnidadAprendizaje(61, "Criptograf�a", 2, Semestre.TERCERO),
                new UnidadAprendizaje(62, "Microondas", 2, Semestre.TERCERO),
                new UnidadAprendizaje(63, "Procesamiento de im�genes", 2, Semestre.TERCERO),
                new UnidadAprendizaje(64, "Televisi�n digital", 2, Semestre.TERCERO),
                new UnidadAprendizaje(65, "Sistemas de calidad", 2, Semestre.TERCERO),
                new UnidadAprendizaje(66, "Procesamiento de voz", 2, Semestre.TERCERO),
                new UnidadAprendizaje(67, "Filtrado avanzado", 2, Semestre.TERCERO),

                new UnidadAprendizaje(68, "Proyecto terminal I", 2, Semestre.CUARTO),
                new UnidadAprendizaje(69, "Proyecto terminal II", 2, Semestre.CUARTO),
                new UnidadAprendizaje(70, "Redes de telecomunicaciones", 2, Semestre.CUARTO),
                new UnidadAprendizaje(71, "Aplicaciones distribuidas", 2, Semestre.CUARTO),
                new UnidadAprendizaje(72, "Dispositivos programables", 2, Semestre.CUARTO),
                new UnidadAprendizaje(73, "Servicio social", 2, Semestre.CUARTO),
                new UnidadAprendizaje(74, "Electiva I", 2, Semestre.CUARTO),
                new UnidadAprendizaje(75, "Electiva II", 2, Semestre.CUARTO),
                new UnidadAprendizaje(76, "Electiva III", 2, Semestre.CUARTO),
                new UnidadAprendizaje(77, "Electiva IV", 2, Semestre.CUARTO)
            );
        }
    }
}