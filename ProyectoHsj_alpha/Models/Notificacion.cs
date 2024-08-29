using System;
using System.Collections.Generic;

namespace ProyectoHsj_alpha.Models;

public partial class Notificacion
{
    public int IdNotificacion { get; set; }

    public int IdUsuario { get; set; }

    public int IdReserva { get; set; }

    public string MensajeNotificacion { get; set; } = null!;

    public DateTime FechaEnvioNotificacion { get; set; }

    public string TituloNotificacion { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
