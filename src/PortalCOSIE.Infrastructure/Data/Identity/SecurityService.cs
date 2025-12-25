using PortalCOSIE.Application;
using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.DTO.Usuario;
using PortalCOSIE.Application.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Identity;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Infrastructure.Data.Identity
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public SecurityService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUsuarioRepository usuarioRepo,
            IUnitOfWork unitOfWork,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _usuarioRepo = usuarioRepo;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        public async Task<Result<string>> IngresarUsuarioAsync(IngresarDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Correo);

            if (user == null)
            {
                throw new ApplicationException("Usuario no encontrado.");
            }

            if (!user.EmailConfirmed)
            {
                //En caso de que el ususario borre el email de confirmacion que?
                return Result<string>.Failure("Revisa tu correo electrónico para confirmar tu cuenta.");
            }

            //RememberMe false para mayor seguridad
            var login = await _signInManager.PasswordSignInAsync(user, dto.Contrasena, false, lockoutOnFailure: true);

            if (login.IsLockedOut)
            {
                var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
                var restante = lockoutEnd.Value.UtcDateTime - DateTime.UtcNow;
                int minutos = (int)restante.TotalMinutes;
                int segundos = restante.Seconds;

                string tiempo;
                if (minutos > 0)
                    tiempo = $"{minutos} minuto{(minutos > 1 ? "s" : "")} y {segundos} segundo{(segundos != 1 ? "s" : "")}";
                else
                    tiempo = $"{segundos} segundo{(segundos != 1 ? "s" : "")}";

                return Result<string>.Failure($"Cuenta bloqueada por múltiples intentos fallidos. Intenta de nuevo en {tiempo}.");
            }
            if (login.IsNotAllowed)
            {
                return Result<string>.Failure("Ingreso no permitido");
            }

            return login.Succeeded
                ? Result<string>.Success("Inicio de sesión exitoso.")
                : Result<string>.Failure("Contraseña incorrecta.");
        }
        public async Task CerrarSesionAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<Result<string>> CrearUsuarioAsync(CrearCuentaDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Correo) || string.IsNullOrEmpty(dto.Contrasena) || string.IsNullOrEmpty(dto.ConfirmarContrasena))
                throw new ApplicationException("Datos incompletos para restablecer la contraseña.");
            if (await _userManager.FindByEmailAsync(dto.Correo) != null)
                throw new ApplicationException("Ya existe este usuario.");
            if (dto.ConfirmarContrasena != dto.ConfirmarContrasena)
                return Result<string>.Failure("Las contraseñas no coinciden.");

            var user = new IdentityUser
            {
                UserName = dto.Correo,
                Email = dto.Correo,
                PhoneNumber = dto.Celular
            };

            var crear = await _userManager.CreateAsync(user, dto.Contrasena);
            if (!crear.Succeeded)
            {
                var errors = crear.Errors.Select(e => e.Description);
                throw new ApplicationException("No se pudo crear el usuario: " + string.Join(", ", errors));
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);

            var correo = await _emailSender.SendEmailAsync(
                dto.Correo,
                "Confirma tu cuenta",
                HtmlTemplates.ConfirmarCorreoHtml(dto.Correo, encodedToken));

            if (!correo.Succeeded)
            {
                var errors = crear.Errors.Select(e => e.Description);
                return Result<string>.Failure(errors);
            }
            return Result<string>.Success("Se ha enviado un enlace a tu correo para confirmar tu cuenta. No olvides revisar tu carpeta de spam.");
        }
        public async Task<Result<string>> CrearPersonalAsync(CrearPersonalDTO dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var user = await _userManager.FindByEmailAsync(dto.Correo);
                if (user != null)
                    return Result<string>.Failure("Este usuario ya existe.");

                var cuentaDto = new CrearCuentaDTO
                {
                    Correo = dto.Correo,
                    Celular = dto.Celular,
                    Contrasena = dto.Contrasena,
                    ConfirmarContrasena = dto.ConfirmarContrasena
                };

                var resultado = await CrearUsuarioAsync(cuentaDto);
                var userCreado = await _userManager.FindByEmailAsync(dto.Correo);

                var rolResult = await _userManager.AddToRoleAsync(userCreado, "Personal");
                if (!rolResult.Succeeded)
                {
                    var errors = rolResult.Errors.Select(e => e.Description);
                    return Result<string>.Failure(errors);
                }

                var usuario = new Personal(
                    userCreado.Id,
                    "EMP",
                    dto.Nombre,
                    dto.ApellidoPaterno,
                    dto.ApellidoMaterno,
                    "Gestion"
                    );

                await _usuarioRepo.AddAsync(usuario);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return Result<string>.Success("Usuario de personal creado exitosamente.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        public async Task<Result<string>> ConfirmarCorreoAsync(string correo, string token)
        {
            var user = await _userManager.FindByEmailAsync(correo);
            if (user == null)
                return Result<string>.Failure("Usuario no encontrado");
            if (user.EmailConfirmed)
                return Result<string>.Failure("Ya se ha confirmado tu correo");
            if (token == null)
                return Result<string>.Failure("Token vacio");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Result<string>.Failure(errors);
            }
            return Result<string>.Success("Correo confirmado");
        }
        public async Task<Result<string>> RecuperarContrasenaAsync(string correo)
        {
            if (string.IsNullOrEmpty(correo))
                throw new ApplicationException("El correo no puede ser nulo");
            var user = await _userManager.FindByEmailAsync(correo);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                return Result<string>.Failure("No se puede recuperar la contraseña. Verifica que el correo sea correcto y esté confirmado.");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);
            var envio = await _emailSender.SendEmailAsync(
                correo,
                "Recupera tu contraseña",
                HtmlTemplates.RecuperarContrasenaHtml(correo, encodedToken)
            );

            if (!envio.Succeeded)
                return Result<string>.Failure("No se pudo enviar el correo de recuperación.");

            return Result<string>.Success("Se ha enviado un enlace para restablecer tu contraseña.");
        }
        public async Task<Result<string>> RestablecerContrasenaAsync(RestablecerDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Correo) || string.IsNullOrEmpty(dto.Token) || string.IsNullOrEmpty(dto.NuevaContrasena))
                throw new ApplicationException("Datos incompletos para restablecer la contraseña.");
            var user = await _userManager.FindByEmailAsync(dto.Correo);
            if (user == null)
                throw new ApplicationException("No se encontró el usuario.");

            if (dto.NuevaContrasena != dto.ConfirmarContrasena)
                return Result<string>.Failure("Las contraseñas no coinciden.");

            var resultado = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NuevaContrasena);

            if (!resultado.Succeeded)
            {
                var errors = resultado.Errors.Select(e => e.Description);
                return Result<string>.Failure(errors);
            }

            var envio = await _emailSender.SendEmailAsync(
                dto.Correo,
                "Contraseña restablecida con éxito",
                HtmlTemplates.ContrasenaRestablecidaHtml()
            );

            if (!envio.Succeeded)
                return Result<string>.Failure("No se pudo enviar el correo de aviso.");

            return Result<string>.Success("Contraseña restablecida con éxito.");
        }
        public async Task<Result<string>> CambiarContrasena(CambiarContrasenaDTO dto)
        {
            if (dto.NuevaContrasena != dto.ConfirmarContrasena)
                return Result<string>.Failure("Las contraseñas no coinciden.");
            var user = await _userManager.FindByIdAsync(dto.IdentityUserId);
            if (user == null)
                return Result<string>.Failure("Usuario no encontrado");
            var resultado = await _userManager.ChangePasswordAsync(user, dto.Contrasena, dto.NuevaContrasena);
            if (!resultado.Succeeded)
            {
                var errors = resultado.Errors.Select(e => e.Description);
                return Result<string>.Failure(errors);
            }
            var envio = await _emailSender.SendEmailAsync(
                user.Email,
                "Contraseña cambiada con éxito",
                HtmlTemplates.ContrasenaCambiadaHtml()
            );
            if (!envio.Succeeded)
                return Result<string>.Failure("No se pudo enviar el correo de aviso.");
            return Result<string>.Success("Contraseña restablecida con éxito.");
        }
        public async Task<Result<string>> VerificarCorreoAsync(string userId, string correo)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(userId))
                return Result<string>.Failure("El id no puede ser nulo o vacío");
            if (string.IsNullOrWhiteSpace(correo))
                return Result<string>.Failure("El correo no puede ser nulo o vacío");

            // Buscar usuario
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result<string>.Failure("No se encontró el usuario a editar");

            // Si es el mismo correo -> no hacer nada
            if (user.NormalizedEmail == _userManager.NormalizeEmail(correo))
                return Result<string>.Failure("No se detectaron cambios");

            // Validar si el correo ya está en uso
            var correoExistente = await _userManager.FindByEmailAsync(correo);
            if (correoExistente != null)
                return Result<string>.Failure("Ese correo ya está en uso");

            // 2. Envio correo
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, correo);
            var encodedToken = WebUtility.UrlEncode(token);
            var envio = await _emailSender.SendEmailAsync(correo, "Cambio de correo", HtmlTemplates.VerificarCorreoHtml(userId, correo, encodedToken));
            if (!envio.Succeeded)
                return Result<string>.Failure("No se pudo enviar el correo.");

            return Result<string>.Success($"Se envió una verificación a {correo}");
        }
        public async Task<Result<string>> ActualizarCorreoAsync(string id, string correo, string token)
        {
            // 1. Validaciones
            if (string.IsNullOrEmpty(correo))
                return Result<string>.Failure("El nuevo correo no puede ser nulo o vacio");
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return Result<string>.Failure("Usuario no encontrado");
            string correoViejo = user.Email;

            var emailResult = await _userManager.ChangeEmailAsync(user, correo, token);
            if (!emailResult.Succeeded)
                return Result<string>.Failure(string.Join(", ", emailResult.Errors.Select(e => e.Description)));

            var nameResult = await _userManager.SetUserNameAsync(user, correo);
            if (!nameResult.Succeeded)
                return Result<string>.Failure($"Error al actualizar nombre de usuario: {string.Join(", ", nameResult.Errors.Select(e => e.Description))}");

            var envio = await _emailSender.SendEmailAsync(correoViejo, "Correo actualizado", HtmlTemplates.CorreoActualizadoHtml(correo));
            if (!envio.Succeeded)
                return Result<string>.Failure("No se pudo enviar el correo.");

            return Result<string>.Success("Se actualizó el correo con éxito");
        }
        public async Task<Result<string>> ActualizarCelularAsync(string userId, string celular)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ApplicationException("El id no puede ser nulo o vacio");
            if (string.IsNullOrEmpty(celular))
                throw new ApplicationException("El nuevo celular no puede ser nulo o vacio");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result<string>.Failure("Usuario no encontrado");
            if (user.PhoneNumber == celular)
                return Result<string>.Failure("No se detectaron cambios");

            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, celular);
            var result = await _userManager.ChangePhoneNumberAsync(user, celular, token);
            if (!result.Succeeded)
                return Result<string>.Failure($"Error al actualizar celular: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            var envio = await _emailSender.SendEmailAsync(user.Email, "Celular actualizado", HtmlTemplates.CelularActualizadoHtml(celular));
            if (!envio.Succeeded)
                return Result<string>.Failure("No se pudo enviar el correo.");
            return Result<string>.Success("Celular actualizado con éxito");
        }
        public async Task<Result<string>> ToggleRol(string userId, string rol)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Result<string>.Failure("ID de usuario no válido");
            if (string.IsNullOrWhiteSpace(rol))
                return Result<string>.Failure("Nombre de rol no válido");

            // Roles válidos permitidos
            var validRoles = new[] { "Alumno", "Personal" };
            if (!validRoles.Contains(rol))
                return Result<string>.Failure($"Rol '{rol}' no es válido");

            // Obtener usuario
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result<string>.Failure("Usuario no encontrado");

            // Verificar si el usuario tiene el rol actualmente
            var hasRole = await _userManager.IsInRoleAsync(user, rol);
            IdentityResult result;

            if (hasRole)
            {
                // Si tiene el rol, lo removemos (pasa a "Inactivo")
                result = await _userManager.RemoveFromRoleAsync(user, rol);

                if (!result.Succeeded)
                {
                    return Result<string>.Failure($"Error al desactivar usuario: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                await _emailSender.SendEmailAsync(user.Email, "Acceso restringido", HtmlTemplates.RestringirAccesoHtml(rol));
                return Result<string>.Success("Usuario marcado como Inactivo correctamente");
            }
            else
            {
                // Si no tiene el rol, primero removemos cualquier rol conflictivo del mismo tipo
                var currentRoles = await _userManager.GetRolesAsync(user);
                var conflictingRoles = currentRoles.Where(r => validRoles.Contains(r)).ToList();

                // Remover roles conflictivos si existen
                if (conflictingRoles.Any())
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, conflictingRoles);
                    if (!removeResult.Succeeded)
                    {
                        return Result<string>.Failure($"Error al remover roles existentes: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");
                    }
                }

                // Asignar el nuevo rol
                result = await _userManager.AddToRoleAsync(user, rol);

                if (!result.Succeeded)
                {
                    return Result<string>.Failure($"Error al asignar rol: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                await _emailSender.SendEmailAsync(user.Email, "Acceso activado", HtmlTemplates.ActivarAccesoHtml(rol));
                return Result<string>.Success($"Rol {rol} asignado correctamente");
            }
        }
        public async Task<IEnumerable<AlumnoCompletoDTO>> ListarAlumnos()
        {
            var alumnos = await _usuarioRepo.ListarAlumnoConCarrera();
            var alumnosDTO = new List<AlumnoCompletoDTO>();

            foreach (var alumno in alumnos)
            {
                var identityUser = await _userManager.FindByIdAsync(alumno.IdentityUserId);
                if (identityUser == null) continue;

                var roles = await _userManager.GetRolesAsync(identityUser);

                alumnosDTO.Add(new AlumnoCompletoDTO
                {
                    IdentityUserId = identityUser.Id,
                    NumeroBoleta = alumno?.NumeroBoleta ?? "N/A",
                    Nombre = alumno.Nombre,
                    ApellidoPaterno = alumno.ApellidoPaterno,
                    ApellidoMaterno = alumno.ApellidoMaterno,
                    Carrera = alumno.Carrera,
                    PeriodoIngreso = alumno.PeriodoIngreso,
                    Correo = identityUser.Email,
                    Celular = identityUser.PhoneNumber,
                    Rol = roles.FirstOrDefault()
                });
            }
            return alumnosDTO;
        }
        public async Task<IEnumerable<PersonalCompletoDTO>> ListarPersonal()
        {
            var usuarios = await _usuarioRepo.ListarPersonal();

            var personalDTO = new List<PersonalCompletoDTO>();

            foreach (var usuario in usuarios)
            {
                var identityUser = await _userManager.FindByIdAsync(usuario.IdentityUserId);
                if (identityUser == null) continue;

                var roles = await _userManager.GetRolesAsync(identityUser);

                personalDTO.Add(new PersonalCompletoDTO
                {
                    IdentityUserId = identityUser.Id,
                    Nombre = usuario.Nombre,
                    ApellidoPaterno = usuario.ApellidoPaterno,
                    ApellidoMaterno = usuario.ApellidoMaterno,
                    Correo = identityUser.Email,
                    Rol = roles.FirstOrDefault()
                });
            }
            return personalDTO;
        }
        public async Task<AlumnoCompletoDTO?> BuscarAlumnoCompleto(string identityUserId)
        {
            if (string.IsNullOrWhiteSpace(identityUserId))
                throw new ArgumentException("El ID del usuario es obligatorio", nameof(identityUserId));

            var alumno = await _usuarioRepo.BuscarAlumnoConCarrera(identityUserId)
                           ?? throw new ApplicationException("No se encontró el usuario");

            // Obtener IdentityUser para el celular
            var identityUser = await _userManager.FindByIdAsync(identityUserId)
                               ?? throw new ApplicationException("No se encontró el IdentityUser");

            return new AlumnoCompletoDTO
            {
                IdentityUserId = identityUserId,
                Nombre = alumno.Nombre,
                ApellidoPaterno = alumno.ApellidoPaterno,
                ApellidoMaterno = alumno.ApellidoMaterno,
                NumeroBoleta = alumno.NumeroBoleta,
                Correo = identityUser.Email,
                CorreoConfirmado = identityUser.EmailConfirmed,
                PeriodoIngreso = alumno.PeriodoIngreso,
                Carrera = alumno.Carrera,
                Celular = identityUser.PhoneNumber
            };
        }

    }
}
