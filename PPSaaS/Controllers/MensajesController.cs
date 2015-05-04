using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPSaaS.Controllers
{
    public class MensajesController : Controller
    {
        
        /// <summary>
        /// Acción que devuelve la vista principal del controlador Mensajes
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
