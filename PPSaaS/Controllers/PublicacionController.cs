using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPSaaS.Controllers
{
    public class PublicacionController : Controller
    {
        
        /// <summary>
        /// Accion que devuelve la vista principal del controlador publicacion
        /// </summary>
        /// <returns>Regresa la vista index.cshtml del controlador publicacion</returns>
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Detalle(int ? clave) 
        {

            if (clave.HasValue)
            {
               // ViewBag.publicacion = PublicacionService.obtener((int)clave);
                return View("Detalle");
            }
            else 
            {
                return View("Detalle");
            }

        }
    }
}
