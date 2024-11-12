using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Gestion.Utilities
{
    // Filtro de autorización personalizado para validar los roles de usuario

    public class ValidacionUtility : AuthorizeAttribute
        {
       
            private readonly int[] roles;

            // Constructor que acepta un arreglo de roles permitidos
            public ValidacionUtility(params int[] roles)
            {
                this.roles = roles;
            }

            // Método que verifica si el usuario está autorizado
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                // Obtiene el rol del usuario desde la sesión
                var userRole = (int?)httpContext.Session["UserRol"];

                // Verifica si el rol del usuario está en la lista de roles permitidos
                if (userRole == null || !roles.Contains(userRole.Value))
                {
                    return false;
                }
                return true;
            }

            // Maneja la solicitud no autorizada redirigiendo a una acción específica
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                // Redirige al usuario no autorizado a la acción "Unauthorized" del controlador "Home"
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "User", action = "PermisoDenegado" }
                    )
                );
            }
        }
    }
