using Microsoft.AspNetCore.Mvc;
using ProyectoHsj_alpha.Models;
using ProyectoHsj_alpha.Services;
namespace ProyectoHsj_alpha.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ReservaService _reservaService;
         public ReservaController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ViewData["Canchas"] = _reservaService.ObtenerCanchas();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                // Verificar si ya existe una reserva para la fecha y el horario seleccionado
                if (_reservaService.ExisteReserva(reserva.IdCancha, reserva.FechaReserva.ToDateTime(TimeOnly.MinValue), reserva.IdHorarioDisponible))
                {
                    ModelState.AddModelError("", "Ya existe una reserva para el horario seleccionado.");
                    ViewData["Canchas"] = _reservaService.ObtenerCanchas();
                    return View(reserva);
                }

                // Intentar crear la reserva
                if (_reservaService.CrearReserva(reserva))
                {
                    TempData["Mensaje"] = "Reserva creada exitosamente.";
                    return RedirectToAction("Index");
                }
            }

            // Si el modelo no es válido o hubo un error, mostramos la vista de nuevo
            ViewData["Canchas"] = _reservaService.ObtenerCanchas();
            return View(reserva);
        }
    


      public IActionResult ReservaView()
        {
            return View();
        }
    }
}
