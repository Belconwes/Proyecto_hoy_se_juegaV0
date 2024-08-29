using System;
using System.Collections.Generic;

namespace ProyectoHsj_alpha.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int IdUsuario { get; set; }

    public int IdCancha { get; set; }

    public int IdHorarioDisponible { get; set; }

    public DateOnly FechaReserva { get; set; }

    public int IdEstadoReserva { get; set; }

    public virtual Cancha IdCanchaNavigation { get; set; } = null!;

    public virtual EstadoReserva IdEstadoReservaNavigation { get; set; } = null!;

    public virtual HorarioDisponible IdHorarioDisponibleNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
