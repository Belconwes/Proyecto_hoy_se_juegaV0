using Microsoft.AspNetCore.Mvc;
using ProyectoHsj_alpha.Models;
using ProyectoHsj_alpha.ViewsModels;
using ProyectoHsj_alpha.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace ProyectoHsj_alpha.Controllers
{
    public class AccesController : Controller
    {
        private readonly HoySeJuegaContext _hoysejuegacontext;

        public AccesController(HoySeJuegaContext seJuegaContext)
        {
            _hoysejuegacontext = seJuegaContext;
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public async Task <IActionResult> Signup(UsuarioVM modelo)
        {
            if(modelo.ContraseniaUsuario != modelo.ConfirmarContraseña)
            {
                ViewData["Message"] = "Las contraseñas no coinciden";
                return View();
            }
            string hashedPassword = PasswordHasher.HashPassword(modelo.ContraseniaUsuario);
            // Obtener el siguiente ID disponible manualmente
            int nuevoIdUsuario = _hoysejuegacontext.Usuarios.Any()
                ? _hoysejuegacontext.Usuarios.Max(u => u.IdUsuario) + 1
                : 1;


            Usuario usuario = new Usuario()
            {
                IdUsuario = nuevoIdUsuario,
                NombreUsuario = modelo.NombreUsuario,
                ApellidoUsuario = modelo.ApellidoUsuario,
                CorreoUsuario = modelo.CorreoUsuario,
                TelefonoUsuario = modelo.TelefonoUsuario,
                ContraseniaUsuario = hashedPassword,
                IdRol = (1),
            };
            await _hoysejuegacontext.Usuarios.AddAsync(usuario);
            await _hoysejuegacontext.SaveChangesAsync();
            if(usuario.IdUsuario != 0)
            {
                return RedirectToAction("Login", "Acces");
            }
            ViewData["Message"] = "No se pudo registrar al usuario";
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo)
        {
            Usuario? usuario_found = await _hoysejuegacontext.Usuarios
                                    .Where(u =>
                                     u.CorreoUsuario == modelo.CorreoUsuario
                                     ).FirstOrDefaultAsync();
            if (usuario_found == null || !PasswordHasher.VerifyPassword(modelo.ContraseniaUsuario, usuario_found.ContraseniaUsuario))
            {
                ViewData["Message"] = "No se encontro el usuario solicitado, por favor revise los campos a rellenar";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, usuario_found.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario_found.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario_found.IdRol.ToString())
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties

                );
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Cerrar()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
