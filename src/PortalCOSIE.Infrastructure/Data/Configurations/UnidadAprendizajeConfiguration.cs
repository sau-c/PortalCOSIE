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
                new UnidadAprendizaje(1, "Programación", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(2, "Análisis y diseño de sistemas", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(3, "Estructura de datos", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(4, "Administración de sistemas operativos", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(5, "Diseño digital", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(6, "Arquitectura de computadoras", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(7, "Fundamentos de física", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(8, "Ecuaciones diferenciales", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(9, "Probabilidad", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(10, "Cálculo diferencial e integral", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(11, "Variable compleja", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(12, "Álgebra lineal", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(13, "Electromagnetismo", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(14, "Cálculo multivariable", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(15, "Administración organizacional", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(16, "Ética, profesión y sociedad", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(17, "Comunicación oral y escrita", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(18, "Inglés I", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(19, "Inglés II", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(20, "Programación estructurada", 2, Semestre.PRIMERO),
                new UnidadAprendizaje(21, "Sociedad, ciencia y tecnología", 2, Semestre.PRIMERO),

                new UnidadAprendizaje(22, "Señales y sistemas", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(23, "Propagación de ondas electromagnéticas", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(24, "Electrónica", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(25, "Teoría de los circuitos", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(26, "Teoría de la información", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(27, "Teoría de las comunicaciones", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(28, "Comunicaciones digitales", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(29, "Procesamiento digital de señales", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(30, "Telefonía", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(31, "Sistemas celulares", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(32, "Protocolos de Internet", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(33, "Sistemas distribuidos", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(34, "Ingeniería web", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(35, "Programación avanzada", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(36, "Bases de datos", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(37, "Transmisión de datos", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(38, "Información financiera e ingeniería económica", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(39, "Optativa I", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(40, "Inglés III", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(41, "Métodos numéricos", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(42, "Electrónica para comunicaciones", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(43, "Óptica", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(44, "Desarrollo sustentable", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(45, "Economía para ingenieros", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(46, "Inglés IV", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(47, "Redes inalámbricas", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(48, "Redes neuronales", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(49, "Lógica difusa", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(50, "Sistemas de información geográfica", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(51, "Programación de dispositivos móviles", 2, Semestre.SEGUNDO),
                new UnidadAprendizaje(52, "Normatividad en telecomunicaciones e informática", 2, Semestre.SEGUNDO),

                new UnidadAprendizaje(53, "Redes inteligentes", 2, Semestre.TERCERO),
                new UnidadAprendizaje(54, "Líneas de transmisión y antenas", 2, Semestre.TERCERO),
                new UnidadAprendizaje(55, "Seguridad en redes", 2, Semestre.TERCERO),
                new UnidadAprendizaje(56, "Multimedia", 2, Semestre.TERCERO),
                new UnidadAprendizaje(57, "Bases de datos distribuidas", 2, Semestre.TERCERO),
                new UnidadAprendizaje(58, "Metodología de la investigación", 2, Semestre.TERCERO),
                new UnidadAprendizaje(59, "Administración de proyectos", 2, Semestre.TERCERO),
                new UnidadAprendizaje(60, "Liderazgo y emprendedores", 2, Semestre.TERCERO),
                new UnidadAprendizaje(61, "Criptografía", 2, Semestre.TERCERO),
                new UnidadAprendizaje(62, "Microondas", 2, Semestre.TERCERO),
                new UnidadAprendizaje(63, "Procesamiento de imágenes", 2, Semestre.TERCERO),
                new UnidadAprendizaje(64, "Televisión digital", 2, Semestre.TERCERO),
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