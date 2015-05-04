using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPSaaS.Models.EntityModel;

namespace PPSaaS.Models
{
    /// <summary>
    /// Abstracción de un servicio
    /// </summary>
    public class Servicio 
    {
        public int clave { get; set; }
        public String nombre { get; set; }
        public String controlador { get; set; }
        public String vista { get; set; }
        public Boolean activo { get; set; }
        public DateTime fechaModificacion { get; set; }


        /// <summary>
        /// Metodo que convierte un objeto servicio de la base de datos a un modelo abstracto
        /// </summary>
        /// <param name="servicioBD">Instancia del modelo de servicio de la base de datos</param>
        /// <returns>Regresa una instancia del modelo abstracto de servicio</returns>
        public static Servicio mapear(String servicioBD) 
        {
            Servicio servicio = null;

            return servicio;
        }
    
    }

    /// <summary>
    /// Clase que contiene metodos para el manejo de servicios
    /// </summary>
    public static class ServicioService 
    {
        /// <summary>
        /// Metodo que obtiene un servicio
        /// </summary>
        /// <param name="clave">Clave del servicio</param>
        /// <returns>Instancia del modelo de servicio</returns>
        public static Servicio obtener(int clave) 
        {
            Servicio servicio = null;


            return servicio;
        }




    }



    /// <summary>
    /// Clase que representa una relación Escuela-Servicio
    /// </summary>
    public class EscuelaServicio 
    {
        public int clave { get; set; }
        

    }

    /// <summary>
    /// Clase que representa una abstracción de un aviso de privacidad
    /// </summary>
    public class AvisoPrivacidad 
    {

        public int clave{get;set;}
        public String instrucciones { get; set; }
        public String contenido{get;set;}
        public Escuela Escuela { get; set; }
        public DateTime vigencia { get; set; }
        public DateTime ultimaModificacion { get; set; }
        public Boolean activo { get; set; }
        public int version { get; set; }
        public Usuario tutor { get; set; }

        /// <summary>
        /// Metodo que genera un aviso de privacidad
        /// </summary>
        /// <param name="escuela">Instancia del modelo asbtracto de escuela</param>
        /// <param name="instrucciones">Cadena con instrucciones generales</param>
        /// <param name="contenido">Cadena con el contenido del aviso de privacidad</param>
        /// <param name="vigencia">Fecha de la vigencia del aviso de privacidad</param>
        /// <param name="fechaModificacion">Fecha de modificacion del aviso de privacidad</param>
        /// <param name="bActivo">Bandera que indica si esta activo o no</param>
        /// <param name="version">Version del aviso</param>
        /// <returns>Regersa un valor booleano que indica si se registro correctamente</returns>
        public static Boolean registraAvisoPrivacidad(Escuela escuela, String instrucciones, String contenido, DateTime? vigencia, DateTime? fechaModificacion, Boolean bActivo, int version)
        {
            Boolean res = false;


            using (SaaSEntities dbContext = new SaaSEntities())
            {



            }



            return res;
        }

        


    

    
    }



    /// <summary>
    /// Clase que representa una abstraccion de un contrato escuela
    /// </summary>
    public class Contrato 
    {
        public int clave { get; set; }
        public String instrucciones { get; set; }
        public String contenido { get; set; }
        public Escuela Escuela { get; set; }
        public DateTime vigencia { get; set; }
        public DateTime ultimaModificacion { get; set; }
        public Boolean activo { get; set; }
        public int version { get; set; }

        public Usuario tutor { get; set; }


    }



}