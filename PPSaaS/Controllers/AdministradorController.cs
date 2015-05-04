using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPSaaS.Models;
using System.Text;

namespace PPSaaS.Controllers
{
    public class AdministradorController : Controller
    {
        /// <summary>
        /// Accion que regresa la vista principal del controador administrador
        /// </summary>
        /// <returns>Regresa la vista index.cshtml del controlador administrador</returns>
        public ActionResult Index()
        {
            return View();
        }




        /// <summary>
        /// Accion que devuelve la vista Escuela
        /// </summary>
        /// <param name="clave">Parametro opcional. Puede recibir la clave de la escuela</param>
        /// <returns>Regresa la vista Escuela.cshtml con informacion de la escuela o para el registro de una nueva</returns>
        public ActionResult Escuela(int? clave)
        {
            //Modo de edicion/consulta escuela
            if (clave.HasValue)
            {
                ViewBag.InformacionEscuela = EscuelaService.obtener((int)clave);

                return View("Escuela");
            }
            //Modo nueva escuela
            else 
            {
                return View("Escuela");
            }
        }



        public ActionResult RegistroEscuela() 
        {
            return View();
        }


        ///Region que contiene los metodos de acción con las reglas de negocio de la aplicacion
        #region

        /// <summary>
        /// Acción que registra una nueva escuela 
        /// </summary>
        /// <param name="coleccion">Coleccion con los campos del formulario para realizar el registro</param>
        /// <returns>Regresa la vista principal de administracion de escuelas si el procedimiento se realizo de forma correcta, regresa la vista RegistroEscuela si hubo error</returns>
        [HttpPost]
        public ActionResult GuardarEscuela(FormCollection coleccion) 
        {
            //Recuperamos los datos de los campos del formulario
            String nombreEscuela = coleccion["txt_nombreEscuela"];
            String rfc = coleccion["txt_RFC"];
            String claveSEP = coleccion["txt_claveSEP"];
            String calle = coleccion["txt_calle"];
            String numero = coleccion["txt_numero"];
            String codigoPostal = coleccion["txt_cp"];
            String estado = coleccion["txt_estado"];
            String pais = coleccion["txt_pais"];
            String responsable = coleccion["txt_responsable"];
            String telefono = coleccion["txt_telefono"];
            String extension = coleccion["txt_extension"];
            int claveEscuela = Convert.ToInt32(coleccion["cve-es"]);


            Boolean resultado = false;

            //Si el valor viene en 0, el registro es para nueva escuela
            if (claveEscuela != 0)
            {
                //Guardamos los datos de esta escuela
                resultado = EscuelaService.registrar(nombreEscuela, rfc, claveSEP, calle, numero, codigoPostal, estado, pais, responsable, telefono, extension);
            }
            else 
            {
                //Actualizamos los datos recuperados del formulario
                resultado = EscuelaService.modificar(claveEscuela, nombreEscuela, rfc, claveSEP, calle, numero, codigoPostal, estado, pais, responsable, telefono, extension);
            }



            
            //Si el guardado se realizo de manera correcta
            if (resultado)
            {
                //Redireccionamos al administrador de escuelas
                return RedirectToAction("Escuela", "Administrador");
            }
            else
            {
                //En caso contrario redireccionamos a la vista de RegistroEscuela, indicando que ocurrio un error
                TempData.Add("ErrorRegistroEscuela", "Ocurrio un error al guardar la información, por favor intente nuevamente.");
                return RedirectToAction("RegistroEscuela", "Administrador");
            }


        }

        /// <summary>
        /// Acción que trae los resultados de búsqueda de una tabla de Escuelas activas
        /// </summary>
        /// <param name="draw">Secuencias de resultados</param>
        /// <param name="start">Número de página a mostrar</param>
        /// <param name="length">Cantidad de registros a mostrar por página</param>
        /// <param name="columns">Columnas a buscar</param>
        /// <param name="order">Valores de ordenamiento</param>
        /// <returns>Devuelve un json con la informacion de las escuelas recuperadas</returns>
        [HttpPost]
        public JsonResult BuscarEscuelas(int draw, int start, int length, DataTableColumn[] columns, DataTableOrder[] order)
        {
            /* Parametros de entrada customizados */
            String filtro = Request.Params["search.value"];

            /* Parseamos los valores de las columnas */
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i].data = Request.Params[String.Format("columns[{0}][data]", i)];
                columns[i].name = Request.Params[String.Format("columns[{0}][name]", i)];
                columns[i].orderable = Convert.ToBoolean(Request.Params[String.Format("columns[{0}][orderable]", i)]);
                columns[i].search = new DataTableSearch();
                columns[i].search.regex = Convert.ToBoolean(Request.Params[String.Format("columns[{0}][search][regex]", i)]);
                columns[i].search.value = Request.Params[String.Format("columns[{0}][search][value]", i)];
            }

            /* Parseamos los valores de los ordenes */
            for (int i = 0; i < order.Length; i++)
            {
                order[i].dir = Request.Params[String.Format("order[{0}][dir]", i)];
                order[i].column = Convert.ToInt32(Request.Params[String.Format("order[{0}][column]", i)]);
            }


            List<DataTableColumn> columnsFiltered = columns.ToList();
            columnsFiltered.Add(new DataTableColumn()
            {
                data = "EscuelasActivas",
                name = null,
                orderable = false,
                search = new DataTableSearch()
                {
                    regex = false,
                    value = "EscuelasActivas"
                }
            });

            /* Sustituimos el arreglo de columnas, por uno que incluye el filtro de la universidad */
            columns = columnsFiltered.ToArray();

            /* Generamos el listado de filtros a usar*/
            Dictionary<String, Object> filtros = new Dictionary<String, Object>();
            foreach (DataTableColumn item in columns)
            {
                if (!String.IsNullOrEmpty(item.search.value))
                {
                    filtros.Add(item.data, item.search.value);
                }
            }

            /* Total de registros default es 0 */
            int recordsTotal = 0;

            List<Escuela> publicaciones = EscuelaService.Buscar(filtros, columns[order[0].column].data, order[0].dir, start, length, out recordsTotal);

            /* Regresamos la respuesta */

            var jsonResult = Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = publicaciones.Count, data = publicaciones }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            //return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = publicaciones.Count, data = publicaciones}, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }


        #endregion
    }
}
