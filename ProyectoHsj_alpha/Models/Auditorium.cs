using System;
using System.Collections.Generic;

namespace ProyectoHsj_alpha.Models;

public partial class Auditorium
{
    public int IdAuditoria { get; set; }

    public int? IdUsuario { get; set; }

    public DateTime FechaAuditoria { get; set; }

    public string AccionRealizada { get; set; } = null!;

    public string DescripcionDeAccion { get; set; } = null!;

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
