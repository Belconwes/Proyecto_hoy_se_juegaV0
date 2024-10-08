﻿using Microsoft.AspNetCore.Mvc;
using ProyectoHsj_alpha.Models;
using System.Net.Mail;
using FluentEmail.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ProyectoHsj_alpha.ViewsModels;
using ProyectoHsj_alpha.Utilities;
namespace ProyectoHsj_alpha.Controllers


{
    
    public class Account : Controller
    {
        private readonly HoySeJuegaContext _context;
        private readonly IFluentEmail _fluentEmail;


        public Account(HoySeJuegaContext context, IFluentEmail fluentEmail)
        {
            _context = context;
            _fluentEmail = fluentEmail;
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Usuarios.SingleOrDefaultAsync(u => u.CorreoUsuario == model.Email);
            if (user == null)
            {
                // Mostrar mensaje de que el email no está registrado
                ModelState.AddModelError(string.Empty, "Email no registrado.");
                return View(model);
            }

            // Generar token de restablecimiento
            user.PasswordResetToken = Guid.NewGuid().ToString();
            user.PasswordResetTokenExpiry = DateTime.Now.AddHours(1);

            _context.Update(user);
            await _context.SaveChangesAsync();

            // Enviar un correo de restablecimiento de contraseña
            var resetLink = Url.Action("ResetPassword", "Account", new { token = user.PasswordResetToken }, Request.Scheme);

            var email = _fluentEmail
                .To(user.CorreoUsuario)
                .Subject("Recuperación de Contraseña")
                .Body($"Por favor haz clic en el siguiente enlace para restablecer tu contraseña: {resetLink}");

            await email.SendAsync();

            // Mostrar mensaje de que el email fue enviado
            return RedirectToAction("ForgotPasswordConfirmation");
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token no válido.");

            var model = new ResetPasswordViewModel { Token = token };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Usuarios.SingleOrDefaultAsync(u => u.PasswordResetToken == model.Token);
            if (user == null || user.PasswordResetTokenExpiry < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Token no válido o expirado.");
                return View(model);
            }

            // Encriptar la nueva contraseña
            user.ContraseniaUsuario = PasswordHasher.HashPassword(model.NewPassword);
            user.PasswordResetToken = null; // Limpiar el token
            user.PasswordResetTokenExpiry = null;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("ResetPasswordConfirmation");
        }

        // GET: /Account/ResetPasswordConfirmation
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
