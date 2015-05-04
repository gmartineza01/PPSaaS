using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPSaaS.Models;


namespace PPSaaS.Controllers
{

    /// <summary>
    /// Clase controladora que contiene acciones para el manejo de inscripciones
    /// </summary>
    public class InscripcionController : Controller
    {
        ///Region que contiene las acciones que devuelven las vistas  
        #region

        /// <summary>
        /// Accion que regresa la vista principal del modulo de inscripcion
        /// </summary>
        /// <returns>Regresa la vista Index del controller Inscripcion</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Accion que devuelve la vista aviso privacidad del modulo de inscripcion
        /// </summary>
        /// <returns>Regresa la vista AvisoPrivacidad del controller Inscripcion</returns>
        /// 


        //[PPSaaS.Common.PPSaaSAutorize(Roles = "DIRECTOR")]
        public ActionResult AvisoPrivacidad() 
        {
            return View();
        }

        /// <summary>
        /// Accion que devuelve la vista contratos del modulo del inscripcion
        /// </summary>
        /// <returns></returns>
        public ActionResult Contratos() 
        {
            return View();
        }

        public ActionResult InformacionFamiliar() 
        {
            return View();
        }
        
        public ActionResult SeguroMedico() 
        {
            return View();
        }
        
        public ActionResult SeguroEducativo() 
        {
            return View();
        }

        public ActionResult InformacionMedica() 
        {
            return View();
        }

        public ActionResult ActividadExtracurricular() 
        {
            return View();
        }

        public ActionResult Pagos() 
        {
            return View();
        }

        public ActionResult NuevosMiembros() 
        {
            return View();
        }

        public ActionResult Encuestas() 
        {
            return View();
        }

        #endregion


        //Region que contiene las acciones de las reglas de negocio en el modulo de inscripciones
        #region

        [HttpPost]
        public ActionResult ProcesarAvisoPrivacidad(FormCollection coleccion) 
        {

            String key = coleccion["identificador"];

            return RedirectToAction("Index", "Inscripcion");
        }

        [HttpPost]
        public ActionResult ProcesarContrato(FormCollection coleccion) 
        {
            return RedirectToAction("Index", "Inscripcion");
        }


        #endregion



    }
}
