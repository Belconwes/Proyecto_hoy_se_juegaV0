using System;
using System.Collections.Generic;

namespace ProyectoHsj_alpha.Models;

public partial class EstadoReserva
{
    public int IdEstadoReserva { get; set; }

    public string NombreEstadoReserva { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
