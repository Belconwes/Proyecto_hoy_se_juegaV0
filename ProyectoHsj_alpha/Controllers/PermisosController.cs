using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoHsj_alpha.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace ProyectoHsj_alpha.Controllers
{
    public class PermisosController : Controller
    {
        private readonly HoySeJuegaContext _hoySeJuegaContext;
        public PermisosController(HoySeJuegaContext hoySeJuegaContext)
        {
            _hoySeJuegaContext = hoySeJuegaContext;
        }
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ManageView()
        {
            var permisos = await _hoySeJuegaContext.Permisos.ToListAsync();
            if (permisos == null)
            {
                // Puedes lanzar una excepción o redirigir a una página de error
                return NotFound("No se encontraron permisos.");
            }
            return View(permisos);
            
        } 
        // GET: PermisosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PermisosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PermisosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PermisosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PermisosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PermisosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PermisosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PermisosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
