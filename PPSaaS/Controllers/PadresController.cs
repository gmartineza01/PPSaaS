using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPSaaS.Controllers
{
    public class PadresController : Controller
    {
  
        /// <summary>
        /// Accion que devuelve la vista principal del controlador padres
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
