using Microsoft.AspNetCore.Mvc;
using ProyectoHsj_alpha.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ProyectoHsj_alpha.Controllers
{
    public class PerfilUController : Controller
    {
        private readonly HoySeJuegaContext _context;

        public PerfilUController(HoySeJuegaContext Context)
        {
            _context = Context;
        }
        
        public async Task <IActionResult> Perfil()
        {
            var usuarioclaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (usuarioclaim == null)
            {
                return RedirectToAction("Login", "Acces");

            }
            var userId = int.Parse(usuarioclaim.Value);
            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(u => u.IdUsuario == userId);
            Console.WriteLine(" usuario :" + usuario.NombreUsuario);

            if(usuario == null)
            {
                return RedirectToAction("Signup","Acces");
            }

            return View(usuario);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
