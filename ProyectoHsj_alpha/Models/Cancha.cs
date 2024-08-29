using System;
using System.Collections.Generic;

namespace ProyectoHsj_alpha.Models;

public partial class Cancha
{
    public int IdCancha { get; set; }

    public string NombreCancha { get; set; } = null!;

    public string UbicacionCancha { get; set; } = null!;

    public virtual ICollection<HorarioDisponible> HorarioDisponibles { get; set; } = new List<HorarioDisponible>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
