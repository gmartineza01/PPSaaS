using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPSaaS.Models.EntityModel;
using PPSaaS.Models;
using System.Linq.Dynamic;

namespace PPSaaS.Models
{
    /// <summary>
    /// Clase que representa una abstracción de una escuela/cliente
    /// </summary>
    public class Escuela 
    {
        public int clave { get; set; }
        public String nombre { get; set; }
        public String RFC { get; set; }
        public String claveSEP { get; set; }
        public String calle { get; set; }
        public String numero { get; set; }
        public String estado { get; set; }
        public String pais { get; set; }
        public String codigoPostal { get; set; }
        public String responsable { get; set; }
        public String telefono { get; set; }
        public String extension { get; set; }
        public Boolean activo { get; set; }

        public String estatus
        {
            get
            {
                if (activo)
                {
                    return "Activo";
                }
                else
                {
                    return "Inactivo";
                }
            }
        }
 

        

        /// <summary>
        /// Metodo que convierte un objeto escuela de la base de datos a un modelo abstracto de escuela
        /// </summary>
        /// <param name="escuelaBD">Instancia de escuela del modelo de la base de datos</param>
        /// <returns>Instancia del modelo abstracto de Escuela</returns>
        public static Escuela mapear(escuela escuelaBD) 
        {
            Escuela escuela = null;

            if (escuelaBD != null) 
            {
                escuela = new Escuela();

                escuela.clave = escuelaBD.idEscuela;
                escuela.nombre = escuelaBD.nombre;
                escuela.RFC = escuelaBD.RFC;
                escuela.claveSEP = escuelaBD.claveSEP;
                escuela.calle = escuelaBD.direccion_calle;
                escuela.numero = escuelaBD.direccion_numero;
                escuela.estado = escuelaBD.direccion_estado;
                escuela.pais = escuelaBD.direccion_pais;
                escuela.codigoPostal = escuelaBD.direccion_cp;
                escuela.responsable = escuelaBD.contacto_responsable;
                escuela.telefono = escuelaBD.contacto_extension;
                escuela.activo = escuelaBD.bActivo;
                
            }



            return escuela;
        }



    }

    /// <summary>
    /// Clase que contiene metodos para el manejo de Escuelas
    /// </summary>
    public static class EscuelaService 
    {

        /// <summary>
        /// Metodo que registra una nueva escuela
        /// </summary>
        /// <param name="nombre">Nombre de la escuela</param>
        /// <param name="RFC">Clave RFC</param>
        /// <param name="claveSEP">Clave SEP</param>
        /// <param name="calle">Calle</param>
        /// <param name="numero">Numero</param>
        /// <param name="codigoPostal">Codigo postal</param>
        /// <param name="estado">Estado</param>
        /// <param name="pais">Pais</param>
        /// <param name="responsable">Nombre del responsable</param>
        /// <param name="telefono">Telefono de contacto</param>
        /// <param name="extension">Extension de contacto</param>
        /// <returns>Regresa un valor boleano, inidcando si se completo la operacion</returns>
        public static Boolean registrar(String nombre, String RFC, String claveSEP, String calle, String numero, String codigoPostal, String estado, String pais, String responsable, String telefono, String extension) 
        {
            Boolean res = false;

            using(SaaSEntities dbContext = new SaaSEntities())
            {
                try
                {

                    escuela nuevaEscuela = new escuela();

                    nuevaEscuela.nombre = nombre;



                    nuevaEscuela.RFC = RFC;
                    nuevaEscuela.claveSEP = claveSEP;
                    nuevaEscuela.direccion_calle = calle;
                    nuevaEscuela.direccion_numero = numero;
                    nuevaEscuela.direccion_cp = codigoPostal;
                    nuevaEscuela.direccion_estado = estado;
                    nuevaEscuela.direccion_pais = pais;
                    nuevaEscuela.contacto_responsable = responsable;
                    nuevaEscuela.contacto_telefono = telefono;
                    nuevaEscuela.contacto_extension = extension;
                    nuevaEscuela.bActivo = true;


                    dbContext.escuela.Add(nuevaEscuela);

                    dbContext.SaveChanges();

                    res = true;

                
                }catch(Exception){}
               
            }

            return res;
        }

        public static Boolean modificar(int claveEscuela, String nombre, String RFC, String claveSEP, String calle, String numero, String codigoPostal, String estado, String pais, String responsable, String telefono, String extension) 
        {
            Boolean res = false;
            
            using (SaaSEntities dbContext = new SaaSEntities())
            {
                try
                {
                    escuela escuelaBD = dbContext.escuela.FirstOrDefault(e => e.idEscuela == claveEscuela);

                    if (escuelaBD != null)
                    {
                        escuelaBD.RFC = RFC;
                        escuelaBD.claveSEP = claveSEP;
                        escuelaBD.direccion_calle = calle;
                        escuelaBD.direccion_numero = numero;
                        escuelaBD.direccion_cp = codigoPostal;
                        escuelaBD.direccion_estado = estado;
                        escuelaBD.direccion_pais = pais;
                        escuelaBD.contacto_responsable = responsable;
                        escuelaBD.contacto_telefono = telefono;
                        escuelaBD.contacto_extension = extension;
                        escuelaBD.bActivo = true;
                        escuelaBD.fechaModificacion = DateTime.Now;

                        dbContext.SaveChanges();

                    }


                }
                catch (Exception) { }
            
            }





            return res;
        }


        /// <summary>
        /// Metodo que recupera una escuela
        /// </summary>
        /// <param name="clave">Clave de la escuela</param>
        /// <returns>Instancia del modelo de escuela</returns>
        public static Escuela obtener(int clave) 
        {
            Escuela escuela = null;

            using(SaaSEntities dbContext = new SaaSEntities())
            {
                escuela escuelaBD = dbContext.escuela.FirstOrDefault(e => e.idEscuela == clave);

                if(escuelaBD != null)
                {
                    //Mapeamos los atributos de cada uno de los atributos del objeto a un modelo abstracto de escuela
                    escuela = Escuela.mapear(escuelaBD);
                }
            
            
            }

            return escuela;
        }








        /// <summary>
        /// Funcion que obtiene un listado de publicaciones en base a una serie de criterios de filtrado, ordenamiento y páginación.
        /// </summary>
        /// <param name="filtros">Dictionario con filtros de búsqueda. El key es el nombre del campo y el value es el valor a filtrar</param>
        /// <param name="orden_columna">Nombre de la columna por la cual ordenaremos</param>
        /// <param name="orden_direccion">Dirección del ordenamiento: asc o desc</param>
        /// <param name="inicio">Número de registro en el cual iniciamos.</param>
        /// <param name="limite">Número de registros a mostrar por página. -1 Indica que no se limitaran los resultados</param>
        /// <param name="total">Total de registros encontrados en los filtros antes de realizar la páginación</param>
        /// <returns>Listado de publicaciones que coinciden con los criterios de búsqueda y páginación</returns>
        public static List<Escuela> Buscar(Dictionary<String, Object> filtros, String orden_columna, String orden_direccion, int inicio, int limite, out int total)
        {
            /* Declaramos e inicializamos las variables de salida*/
            List<Escuela> resultados = new List<Escuela>();

            total = 0;

            /* Abrimos la conexión a la base de datos */
            using (SaaSEntities dbContext = new SaaSEntities())
            {
                /*Generamos el query que se usará */
                IQueryable<escuela> query = dbContext.escuela;

                /* Variable para las clausulas de filtrado */
                List<String> clausulas = new List<String>();

                foreach (String key in filtros.Keys)
                {
                    switch (key)
                    {

                        case "EscuelasActivas":
                            clausulas.Add(String.Format("bActivo == true", filtros[key]));
                            break;
                      
                        default:
                            /* Agregamos cada filtro como un LIKE/Contains a la lista de clausulas */
                            clausulas.Add(String.Format("{0}.Contains(\"{1}\")", key, filtros[key]));
                            break;
                    }
                }

                /* Si existen clausulas de filtrado agregamos un Where al filtro de la consulta */
                if (clausulas.Count > 0)
                {
                    /* Agregamos las clausulas como un listado de clausulas speradas por AND */
                    query = query.Where(String.Join(" AND ", clausulas.ToArray()), new { });
                };

                /* Contamos el total de registros antes de reducir a los resultados de la página */
                total = query.Count();

                /* Cambiamos la columna de orden en caso de que no pertenezca a la tabla principal */
                if (orden_columna == "")
                {
                    orden_columna = "titulo";
                }

                else if (orden_columna == "clave")
                {
                    orden_columna = "idEscuela";
                }
                else if (orden_columna == "nombreEstatusPublicacion")
                {
                    orden_columna = "estatusPublicacion.nombre";
                }
                else if (orden_columna == "fechaModificacion")
                {
                    orden_columna = "fechaModificacion";
                }
                else if (orden_columna == "imagenCodificada")
                {
                    orden_columna = "idPublicacion";
                }

                /* Dependiendo de la dirección, agregamos ordenamiendo descenciente o ascendiente*/
                if (orden_direccion == "desc")
                {
                    query = query.OrderBy(String.Format("{0} descending", orden_columna));
                }
                else
                {
                    query = query.OrderBy(orden_columna);
                }

                /* Agreagamos las clausulas de paginación solo cuando el limite sea distinto a -1 */
                if (limite != -1)
                {
                    query = query.Skip(inicio).Take(limite);
                }

                /* Executamos el query y usamos los resultados para llenar la variabla de salida*/
                foreach (escuela item in query)
                {
                    /* Creamos una nueva instancia de nuestro modelo de resultado */

                    //Publicacion registro = new Publicacion();

                    //registro.clave = item.idPublicacion;
                    //registro.titulo = item.titulo;
                    //registro.contenido = item.contenido;

                    //registro.fechaPublicacion = item.fechaPublicacion;
                    //registro.fechaModificacion = item.fechaModificacion;
                    //registro.fechaBaja = item.fechaBaja;

                    //if (item.imagen != null)
                    //{
                    //    registro.imagen = item.imagen;
                    //    registro.imagenCodificada = Convert.ToBase64String(item.imagen);
                    //}

                    //registro.claveEstatusPublicacion = (int)item.idEstatus;
                    //registro.nombreEstatusPublicacion = item.estatusPublicacion.nombre;

                    ///* Agregamos el regitro a la lista de resultados */
                    //resultados.Add(registro);




                    Escuela registro = new Escuela();

                    registro.clave = item.idEscuela;
                    registro.nombre = item.nombre;
                    registro.RFC = item.RFC;
                    registro.claveSEP = item.claveSEP;
                    registro.calle = item.direccion_calle;
                    registro.numero = item.direccion_numero;
                    registro.estado = item.direccion_estado;
                    registro.pais = item.direccion_pais;
                    registro.codigoPostal = item.direccion_cp;
                    registro.responsable = item.contacto_responsable;
                    registro.telefono = item.contacto_telefono;
                    registro.extension = item.contacto_extension;
                    registro.activo = item.bActivo;



                    resultados.Add(registro);
                }
            }

            /* Regresamos la lista de solicitudes*/
            return resultados;
        }
    
    
    
    }


}