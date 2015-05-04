using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using PPSaaS.Models;

namespace PPSaaS.Common
{

    /// <summary>
    /// Representa un atributo para restringir el acceso de llamadas a los metodos de accion de controladores no permitidos
    /// </summary>
    public class PPSaaSAutorize: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            /* Revisamos si se definieron roles en la autorización y la persona esta autenticada */
            if (Roles.Length > 0 && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                /* Obtenemos los roles de la persona conectada */
                String[] permisos = UsuarioService.obtenerRoles(filterContext.HttpContext.User.Identity.Name);

           
                /* Revisamos que los roles de la persona conectada hagan match con los solicitados */
                if (!permisos.Intersect(Roles.Split(',')).Any())
                {
                    /* En caso de que los roles de la persona no hagan match con los requeridos, redirigimos a la página de inicio */
                    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                    redirectTargetDictionary.Add("action", "NoAutorizado");
                    redirectTargetDictionary.Add("controller", "Login");
                    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                };
            }
        }



    }
}