using PortalCOSIE.Domain.Entities.Carreras;
using System.Text.RegularExpressions;

namespace PortalCOSIE.Domain.Entities.Usuarios
{
    public class Alumno : BaseEntity
    {
        // Propiedades
        public string NumeroBoleta { get; private set; }
        public string PeriodoIngreso { get; private set; }
        public int CarreraId { get; private set; }

        // Navegaciones
        public Carrera Carrera { get; private set; }
        public Usuario Usuario { get; private set; }

        // Constantes de validación
        private static readonly Regex SoloNumeros10 = new(@"^\d{10}$", RegexOptions.Compiled);
        private const string FormatoPeriodo = @"^\d{4}/[1-3]$"; // YYYY-P (ej: 2024/1)

        // Constructor privado para EF
        private Alumno() { }

        // Constructor de dominio
        public Alumno(string numeroBoleta, string periodoIngreso, int carreraId)
        {
            SetNumeroBoleta(numeroBoleta);
            SetPeriodoIngreso(periodoIngreso);
            SetCarrera(carreraId);
        }

        public void SetNumeroBoleta(string numeroBoleta)
        {
            if (string.IsNullOrWhiteSpace(numeroBoleta))
                throw new DomainException("El número de boleta no puede estar vacío.");
            if (!SoloNumeros10.IsMatch(numeroBoleta))
                throw new DomainException("La boleta debe tener exactamente 10 dígitos numéricos.");
            var año = int.Parse(numeroBoleta[..4]);
            if (año < 1995 || año > 2100)
                throw new DomainException("El año en la boleta debe estar entre 1995 y 2100.");
            NumeroBoleta = numeroBoleta;
        }

        public void SetPeriodoIngreso(string periodoIngreso)
        {
            periodoIngreso = periodoIngreso.Trim();
            if (string.IsNullOrWhiteSpace(periodoIngreso))
                throw new DomainException("El periodo de ingreso es requerido");
            if (!Regex.IsMatch(periodoIngreso, FormatoPeriodo))
                throw new DomainException("El formato del periodo de ingreso no es válido. Use: YYYY/P (ej: 2020/1)");
            PeriodoIngreso = periodoIngreso;
        }

        public void SetCarrera(int carreraId)
        {
            if (carreraId <= 0)
                throw new DomainException("El ID de carrera no es válido");
            CarreraId = carreraId;
        }
    }
}