using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoHsj_alpha.ViewsModels
{
    public class UsuarioEditViewModel
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string ApellidoUsuario { get; set; } = null!;
        public string CorreoUsuario { get; set; } = null!;
        public string TelefonoUsuario { get; set; } = null!;
        public int IdRol { get; set; }
        public List<SelectListItem>? Roles { get; set; }  // Para el dropdown de roles
    }
}
