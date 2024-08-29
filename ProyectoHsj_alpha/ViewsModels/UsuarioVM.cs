namespace ProyectoHsj_alpha.ViewsModels
{
    public class UsuarioVM
    {
        public string NombreUsuario { get; set; } = null!;

        public string ApellidoUsuario { get; set; } = null!;

        public string CorreoUsuario { get; set; } = null!;

        public string ContraseniaUsuario { get; set; } = null!;
        public string ConfirmarContraseña { get; set; } = null!;

        public string TelefonoUsuario { get; set; } = null!;

        public int IdRol { get; set; }
    }
}
