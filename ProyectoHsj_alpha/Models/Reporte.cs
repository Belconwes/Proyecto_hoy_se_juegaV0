using System;
using System.Collections.Generic;

namespace ProyectoHsj_alpha.Models;

public partial class Reporte
{
    public int IdReporte { get; set; }

    public byte MesReporte { get; set; }

    public int AnioReporte { get; set; }

    public int? UsuariosRegistradosReporte { get; set; }

    public int? ReservasRealizadasReporte { get; set; }

    public DateTime FechaDeReporte { get; set; }
}
