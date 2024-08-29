using Microsoft.AspNetCore.Mvc;
using ProyectoHsj_alpha.Models;
using System.Net.Mail;
namespace ProyectoHsj_alpha.Controllers


{
    public class Account : Controller
    {
        private readonly HoySeJuegaContext _context;

        public Account(HoySeJuegaContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
