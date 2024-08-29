
using System.ComponentModel.DataAnnotations;

namespace ProyectoHsj_alpha.ViewsModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
