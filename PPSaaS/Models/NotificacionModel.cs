using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPSaaS.Models.EntityModel;

namespace PPSaaS.Models
{
    /// <summary>
    /// Clase que representa una abstracción de una notificacion
    /// </summary>
    public class Notificacion
    {
        public int clave { get; set; }
        public Evento evento { get; set; }
        public int claveUsuario { get; set; }
        public Boolean bActivo { get; set; }
        public DateTime fechaNotificacion { get; set; }


    }


    /// <summary>
    /// Clase que representa una abstraccion de un evento para 
    /// una notificación
    /// </summary>
    public class Evento 
    {
        public int clave { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public String accion { get; set; }
        public String vista { get; set; }
        public Usuario usuario { get; set; }//Campos opcional, habra que ver si se cambia dependiendo de quien genera la notificacion
    
    }


    /// <summary>
    /// Clase que contiene metodos para el manejo de notificaciones
    /// </summary>
    public static class NotificacionService 
    {
        /// <summary>
        /// Metodo que obtiene las notificaciones que no han sido leidas
        /// </summary>
        /// <param name="clave">Clave del usuario</param>
        /// <returns>Regresa una lista del modelo de notificacion</returns>
        public static List<Notificacion> obtenerNoLeidas(int clave) 
        {
            List<Notificacion> notificaciones = null;


            using(SaaSEntities DBContext = new SaaSEntities())
            {
                var resultados = DBContext.notificacion.Where(n => n.idUsuario == clave && n.bActivo).OrderByDescending(n => n.fechaEvento);

                if (resultados != null) 
                {
                
                    foreach(notificacion n in resultados)
                    {
                        
                    
                    
                    }
                
                }


            }




            return notificaciones;
        }


        /// <summary>
        /// Metodo que marca como leida una notificación
        /// </summary>
        /// <param name="clave">Clave de la notificacion</param>
        /// <returns>Valor booleano que indica si se completo la operacion</returns>
        public static Boolean marcarComoLeido(int clave) 
        {
            Boolean success = false;


            return success;
        }


       
    
    }


}