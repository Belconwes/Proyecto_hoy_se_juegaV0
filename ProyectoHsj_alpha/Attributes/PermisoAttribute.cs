using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoHsj_alpha.Attributes
{
    public class PermisoAttribute : TypeFilterAttribute
    {
        public PermisoAttribute(string permiso) : base(typeof(PermisoFilter))
        {
            Arguments = new object[] { permiso };
        }
    }

    // Clase PermisoFilter que verifica si el usuario tiene el permiso requerido
    public class PermisoFilter : IAsyncActionFilter
    {
        private readonly string _permiso;

        public PermisoFilter(string permiso)
        {
            _permiso = permiso;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Verificar si el usuario tiene el permiso como claim
            if (!context.HttpContext.User.HasClaim("Permiso", _permiso))
            {
                // Si no tiene el permiso, redirigir o devolver 403 Forbidden
                context.Result = new ForbidResult(); // O redirigir a otra página si prefieres
                return;
            }

            await next(); // Permitir la ejecución de la acción
        }
    }
}
