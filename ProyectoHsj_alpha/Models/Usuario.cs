using System;
using System.Collections.Generic;

namespace ProyectoHsj_alpha.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ApellidoUsuario { get; set; } = null!;

    public string CorreoUsuario { get; set; } = null!;

    public string ContraseniaUsuario { get; set; } = null!;

    public string TelefonoUsuario { get; set; } = null!;

    public int IdRol { get; set; }

    public string? PasswordResetToken { get; set; } 
    public DateTime? PasswordResetTokenExpiry { get; set; }
    public string? EmailConfirmationToken { get; set; } //Propiedad Integrada manualmente
    public bool? EmailConfirmed { get; set; } = false; //Propiedad Integrada manualmente
    public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
