using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPSaaS.Controllers
{
    public class EscolarController : Controller
    {
        
        /// <summary>
        /// Accion que regresa la vista principal del controlador Escolar
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
