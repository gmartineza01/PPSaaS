using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPSaaS.Models
{
    /// <summary>
    /// Abstracción de los valores de búsqueda de una columa de la librería de js dataTables
    /// </summary>
    public class DataTableSearch
    {
        /// <summary>
        /// Patron expresion regular
        /// </summary>
        public bool regex { get; set; }
        /// <summary>
        /// Valor a buscar
        /// </summary>
        public String value { get; set; }
    }

    /// <summary>
    /// Abstracción de las columnas de filtrado de la librería de js dataTables
    /// </summary>
    public class DataTableColumn
    {
        /// <summary>
        /// Datos a ordenar
        /// </summary>
        public String data { get; set; }
        /// <summary>
        /// Identificador
        /// </summary>
        public String name { get; set; }
        /// <summary>
        /// Bandera que indica si se encuentra ordenado
        /// </summary>
        public bool orderable { get; set; }
        /// <summary>
        /// Valores de busqueda
        /// </summary>
        public DataTableSearch search { get; set; }
    }

    /// <summary>
    /// Abstracción de las opciones de ordenamiento de la librería de js dataTables
    /// </summary>
    public class DataTableOrder
    {
        /// <summary>
        /// Columna a ordenar
        /// </summary>
        public int column { get; set; }
        /// <summary>
        /// Orden de direccion
        /// </summary>
        public String dir { get; set; }
    }
}