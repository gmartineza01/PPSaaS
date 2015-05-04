using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPSaaS.Models;
using System.Web.Security;

namespace PPSaaS.Controllers
{
    /// <summary>
    /// Clase controladora que contiene acciones para la autenticacion de usuarios
    /// </summary>
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            // Si el usuario esta autenticado, redirigimos hacia el distribuidor de inicio
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Inicio");
            }

            /* Mensaje de error de login, recibido de la acción Autentica */
            ViewBag.msg = TempData["login-error"];


            return View();
        }


        [HttpPost]
        public ActionResult iniciarSesion(FormCollection coleccion) 
        {            
            String usuario = coleccion["usuario"];
            String contrasena = coleccion["password"];

          
            // Revisamos si el usuario y contraseña son válidos
            if (Membership.ValidateUser(usuario, contrasena))
            {
                String[] roles = Roles.GetRolesForUser(usuario);


                if (roles == null || roles.Length == 0)
                {
                    // Ningún rol del usuario esta autorizado
                    TempData.Add("login-error", "Usuario o contraseña incorrectos.");
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    // Iniciamos sesión
                    FormsAuthentication.SetAuthCookie(usuario, false);

                   //Usuario datosUsuario = new Usuario();
                       
                   //datosUsuario = UsuarioService.obtener(usuario);

                   //if (!TempData.ContainsKey("datosUsuario")) 
                   //{
                   //    TempData.Add("datosUsuario", datosUsuario.nombreCompleto);
                   //}
                    
                

                    // Redirigimos al distribuidor de inicio
                    return RedirectToAction("Inicio", "Login");
                }

            }
            else
            {
                TempData.Add("login-error", "Usuario o contraseña incorrectos.");
                return RedirectToAction("Index", "Login");
            }
            
        }

        /// <summary>
        /// Acción que redirige al usuario conectado a su inicio correspondiente por su rol
        /// </summary>
        /// <returns>Devuleve la vista correspondiente con el usuario conectado</returns>
        [PPSaaS.Common.PPSaaSAutorize]
        public ActionResult Inicio() 
        {
            // Obtenemos los roles del usuario conectado
            String[] roles = Roles.GetRolesForUser(User.Identity.Name);
            String controlador = "";
            String accion = "";



            //if (TempData.ContainsKey("datosUsuario"))
            //{
            //    ViewBag.nombreCompleto = TempData["datosUsuario"];
            //}

  



            // Asignamos el inicio del usuario dependiendo del rol
            if (roles.Contains("ADMINISTRADOR GENERAL"))
            {
                controlador = "Inscripcion";
                accion = "Index";
            }
            //else if (roles.Contains("ADMINISTRADOR ESCOLAR"))
            //{
            //    controlador = "Administrador";
            //    accion = "Index";
            //}
            //else if (roles.Contains("DIRECTOR"))
            //{
            //    controlador = "Seguimiento";
            //    accion = "Index";
            //}
            //else if (roles.Contains("EMPLEADO ESCOLAR"))
            //{
            //    controlador = "Seguimiento";
            //    accion = "Index";
            //}
            //else if (roles.Contains("MAESTRO"))
            //{
            //    controlador = "Seguimiento";
            //    accion = "Index";
            //}
            else if (roles.Contains("PADRE DE FAMILIA"))
            {
                controlador = "Padres";
                accion = "Index";
            }
            else
            {
                /*En caso de no tener ningun rol de los autorizados, desconectamos la usuario y lo redirigmos al login*/

                /* Eliminamos la galleta de sesión del usuario */
                FormsAuthentication.SignOut();

                // Ningún rol del usuario esta autorizado
                TempData.Add("login-error", "Usuario o contraseña incorrectos.");
                return RedirectToAction("Index", "Login");
            }

            /* Redirigimos al inicio apropiado para el tipo de usuario */
            return RedirectToAction(accion, controlador);
        }

        /// <summary>
        /// Accion que finaliza la sesión de un usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PPSaaS.Common.PPSaaSAutorize]
        public ActionResult Desconectar()
        {
             /* Si el usuario se encuentra conectado, eliminamos la galleta de autenticacion*/
            if (User.Identity.IsAuthenticated)
            {
                /* Eliminamos la galleta de sesión del usuario */
                FormsAuthentication.SignOut();
            }

            return RedirectToAction("Index", "Login");
        }

        /// <summary>
        /// Vista que muestra un mensaje de acceso no autorizado
        /// </summary>
        /// <returns>Retorna la vista NoAutorizado</returns>
        [HttpGet]
        [PPSaaS.Common.PPSaaSAutorize]
        public ActionResult NoAutorizado()
        {
            return View();
        }




    }
}
