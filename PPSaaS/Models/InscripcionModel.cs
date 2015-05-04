using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPSaaS.Models;

namespace PPSaaS.Models
{


    /// <summary>
    /// Clase que representa una abstraccion 
    /// </summary>
    public class Inscripcion
    {
        /// <summary>
        /// Clave de la inscripcion
        /// </summary>
        public int clave { get; set; }
        public Escuela escuela { get; set; }
        public Alumno alumno { get; set; }
        


    }


    /// <summary>
    /// Clase que contiene metodos para el manejo de inscripciones
    /// </summary>
    public static class InscripcionService 
    {
     
    
    
    
    }

    /// <summary>
    /// Clase que representa una abstraccón de una etapa de inscripción
    /// </summary>
    public class EtapaInscripcion
    {
        public int clave { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public int secuencia { get; set; }
        public Boolean completo { get; set; }

        /// <summary>
        /// Metodo que convierte un objeto de tipo paso inscripcion de la base de datos a un modelo abstracto
        /// </summary>
        /// <param name="etapaInscripcion">Instancia del modelo de inscripcion de la base de datos</param>
        /// <returns>Instancia del modelo de inscripcion del modelo abstracto</returns>
        public static EtapaInscripcion mapear(String etapaInscripcion)
        {
            EtapaInscripcion paso = null;


            return paso;
        }


        /// <summary>
        /// Metodo que obtiene un listado de las etapas del proceso de inscripción
        /// </summary>
        /// <returns>Lista con elementos de tipo EtapaInscripcion</returns>
        public static List<EtapaInscripcion> obtener()
        {
            List<EtapaInscripcion> pasos = null;



            return null;
        }


    }

}