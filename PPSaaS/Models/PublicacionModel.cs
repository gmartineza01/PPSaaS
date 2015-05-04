using System.Web;
using System.Web.Mvc;
using PPSaaS.Models;
using System.Text.RegularExpressions;
using System.Linq.Dynamic;
using System.Web.Security;
using System;

namespace PPSaaS.Models
{

    /// <summary>
    /// Clase que representa una abstraccion de las noticias de la aplicacion
    /// </summary>
    public class Publicacion
    {
        /// <summary>
        /// Identificador unico de la publicación
        /// </summary>
        public int clave { get; set; }
        /// <summary>
        /// Cadena que representa el titulo de la publicación
        /// </summary>
        public String titulo { get; set; }
        /// <summary>
        /// Cadena con el contenido de una publicación
        /// </summary>
        public String contenido { get; set; }

        /// <summary>
        /// Cadena que representa una breve descripción de una publicación
        /// </summary>
        public String resumen { get; set; }

        /// <summary>
        /// Arreglo de bytes para almacenar la imagen que representa la publicación
        /// </summary>
        public Byte[] imagen { get; set; }
        /// <summary>
        /// Conversion de imagen de arreglo de bytes a base64String 
        /// </summary>
        public String imagenCodificada
        {
            set
            {
                this.imagen = imagen;
            }

            get
            {
                if (imagen != null)
                {
                    return Convert.ToBase64String(imagen);
                }
                else
                {
                    return null;
                }
            }
        }



        /// <summary>
        /// Bandera que indica si la noticia es publica
        /// </summary>
        public Boolean Publico { get; set; }
        /// <summary>
        /// Fecha de alta de una publicacion
        /// </summary>
        public DateTime? fechaPublicacion { get; set; }
        /// <summary>
        /// Fecha de modificacion de una publicacion
        /// </summary>
        public DateTime? fechaModificacion { get; set; }
        /// <summary>
        /// Fecha de baja logica de una publicacion
        /// </summary>
        public DateTime? fechaBaja { get; set; }
        /// <summary>
        /// Etiquetas de la publicación
        /// </summary>
        public Etiqueta[] etiquetas { get; set; }
        /// <summary>
        /// Clave del autor de la publicación
        /// </summary>
        public int claveAutor { get; set; }
        /// <summary>
        /// Descripción del autor de la publicación
        /// </summary>
        public String nombreAutor { get; set; }

        /// <summary>
        /// Clave del estatus de la publicacion
        /// </summary>
        public int claveEstatusPublicacion { get; set; }

        /// <summary>
        /// Descripcion del estatus de la publicacón
        /// </summary>
        public String nombreEstatusPublicacion { get; set; }
        /// <summary>
        /// Bandera que indica si una publicacion se encuentra publicada
        /// </summary>
        public Boolean estaPublicado
        {
            get
            {
                return claveEstatusPublicacion != 2 && claveEstatusPublicacion != 3;
            }
        }
        /// <summary>
        /// Bandera que indica si una noticia privada se mostrara en la sección fechas importantes
        /// </summary>
        public Boolean bFechaImportante { get; set; }

        /// <summary>
        /// Función que convierte un objeto publicación del modelo de la base de datos
        /// a un modelo abstracto de publicación
        /// </summary>
        /// <param name="publicacionBD">Instancia publicación del modelo de la base de datos</param>
        /// <returns>Instancia del modelo abstracto de publicación</returns>
        public static Publicacion mapear(publicacion publicacionBD)
        {
            Publicacion publicacion = null;

            PortalPadresEntities bd = new PortalPadresEntities();

            try
            {
                publicacion = new Publicacion();

                publicacion.clave = publicacionBD.idPublicacion;

                //Realizamos algunas comprobaciones con los campos que admiten valores null antes de asignarlos
                if (publicacionBD.titulo != null)
                {
                    publicacion.titulo = publicacionBD.titulo;
                }

                if (publicacionBD.contenido != null)
                {
                    publicacion.contenido = publicacionBD.contenido;
                    publicacion.resumen = Publicacion.generaResumen(publicacionBD.contenido);
                }


                if (publicacionBD.fechaPublicacion != null)
                {
                    publicacion.fechaPublicacion = publicacionBD.fechaPublicacion;
                }

                if (publicacionBD.fechaModificacion != null)
                {
                    publicacion.fechaModificacion = publicacionBD.fechaModificacion;
                }

                if (publicacionBD.imagen != null)
                {
                    publicacion.imagen = publicacionBD.imagen;
                }

                publicacion.claveEstatusPublicacion = (int)publicacionBD.idEstatus;
                publicacion.nombreEstatusPublicacion = bd.estatusPublicacion.SingleOrDefault(ep => ep.idEstatus == publicacionBD.idEstatus).nombre;

                publicacion.Publico = (Boolean)publicacionBD.publico;

                publicacion.etiquetas = Etiqueta.obtener(publicacionBD.idPublicacion);

                publicacion.bFechaImportante = (Boolean)publicacionBD.bFechaImportante;



            }
            catch (Exception e)
            {

            }


            return publicacion;
        }

        /// <summary>
        /// Función que convierte el contenido de una publicacion a un resumen de un parrafo 
        /// </summary>
        /// <param name="cadenaHTML">Cadena con el contenido de la publicación</param>
        /// <returns>Cadena con el contenido resumido de la publicacion</returns>
        public static String generaResumen(String cadenaHTML)
        {
            //Instancia que devuelve el resultado
            String resumen = "";

            //Patron expresion regular que obtiene un parrafo de la cadena HTML
            String patron = @"<p>\s*(.+?)\s*</p>";


            //Buscamos en la cadena html 
            Match m = Regex.Match(cadenaHTML, patron);

            //Si encontramos una coincidencia
            if (m.Success)
            {
                //Recuperamos el contenido 
                resumen = m.Groups[1].Value;
            }

            return resumen;
        }


    }


    /// <summary>
    /// Clase que representa una abstracción de las etiquetas de una publicación 
    /// </summary>
    public class Etiqueta
    {
        /// <summary>
        /// Clave de la etiqueta
        /// </summary>
        public int clave { get; set; }
        /// <summary>
        /// Descripción de la etiqueta
        /// </summary>
        public String nombre { get; set; }


        /// <summary>
        /// Función que obtiene las etiquetas de una publicación
        /// </summary>
        /// <param name="clave">Clave de la publicación</param>
        /// <returns>Arreglo del modelo abstracto de etiqueta. Devuelve null si no se encontraron etiquetas para la publicación especificada.</returns>
        public static Etiqueta[] obtener(int clave)
        {
            //Arreglo que almacena los resultados de la consulta
            Etiqueta[] etiquetas = null;

            try
            {
                //Abrimos una conexion a la base de datos  
                PortalPadresEntities bd = new PortalPadresEntities();

                //Recuperamos todas las etiquetas de la publicación
                var query = bd.relPublicacionEtiqueta.Where(pe => pe.idPublicacion == clave);

                //Si encontramos resultados
                if (query.Count() > 0)
                {
                    //Inicializamos el arreglo de resultados
                    etiquetas = new Etiqueta[query.Count()];

                    int count = 0;

                    //Iteramos en la coleccion recuperada
                    foreach (relPublicacionEtiqueta ep in query)
                    {
                        //Generamos una instancia del modelo de etiquetas
                        Etiqueta etiqueta = new Etiqueta();

                        //Mapeamos los atributos al modelo de etiqueta
                        etiqueta.clave = ep.etiquetaPublicacion.idEtiqueta;
                        etiqueta.nombre = ep.etiquetaPublicacion.nombre;

                        //Agregamos el elemento generado al arreglo
                        etiquetas[count] = etiqueta;

                        //Limpiamos la instancia actual
                        etiqueta = null;

                        //Hacemos lo mismo con el siguiente registro...
                        count++;
                    }

                }

            }
            catch (Exception e)
            {

            }


            return etiquetas;
        }

        /// <summary>
        /// Función que genera y asigna una serie de etiquetas a una publicacion
        /// </summary>
        /// <param name="clave">Clave de la publicacion</param>
        /// <param name="etiquetas">Arreglo de etiquetas</param>
        /// <returns>Valor booleano que indica confirmacion o error</returns>
        public static Boolean generarEtiqueta(int clave, String[] etiquetas)
        {
            Boolean result = false;
            PortalPadresEntities bd = new PortalPadresEntities();

            try
            {
                if (etiquetas != null)
                {
                    foreach (String s in etiquetas)
                    {
                        //Buscamos si la etiqueta ya esta registrada o hay una parecida
                        etiquetaPublicacion etiquetaPublicacionBD = bd.etiquetaPublicacion.FirstOrDefault(ep => ep.nombre.Contains(s));

                        //Si encontramos una parecida
                        if (etiquetaPublicacionBD != null)
                        {
                            //Actualizaremos esta etiqueta con la nueva informacion
                            etiquetaPublicacionBD.nombre = s.ToString();
                            etiquetaPublicacionBD.fechaModificacion = DateTime.Now;

                            //Agregamos la referencia de esta etiqueta a la publicacion

                            //Buscamos primero si existe esta referencia
                            relPublicacionEtiqueta relPublicacionEtiqueta = bd.relPublicacionEtiqueta.FirstOrDefault(pe => pe.idEtiqueta == etiquetaPublicacionBD.idEtiqueta && pe.idPublicacion == clave);

                            //Si la referencia existe, entonces la actualizamos
                            if (relPublicacionEtiqueta != null)
                            {
                                relPublicacionEtiqueta.idEtiqueta = etiquetaPublicacionBD.idEtiqueta;
                                relPublicacionEtiqueta.idPublicacion = clave;
                            }
                            else
                            //Si no existe, entonces agregamos una nuevo registro con la informacion de la etiqueta encontrada anteriormente
                            {
                                relPublicacionEtiqueta nuevaReferencia = new relPublicacionEtiqueta();
                                nuevaReferencia.idEtiqueta = etiquetaPublicacionBD.idEtiqueta;
                                nuevaReferencia.idPublicacion = clave;
                                bd.relPublicacionEtiqueta.Add(nuevaReferencia);
                            }


                            bd.SaveChanges();

                            result = true;
                        }

                        //Si no existe la etiqueta, la guardamos y la asignamos directamente a la publicacion
                        else
                        {
                            //Generamos la nueva etiqueta
                            etiquetaPublicacion nuevaEtiqueta = new etiquetaPublicacion();
                            nuevaEtiqueta.nombre = s.ToString();
                            nuevaEtiqueta.fechaModificacion = DateTime.Now;
                            bd.etiquetaPublicacion.Add(nuevaEtiqueta);
                            bd.SaveChanges();

                            //Recuperamos el valor de la clave generada para la etiqueta
                            int claveEtiqueta = bd.Entry(nuevaEtiqueta).Entity.idEtiqueta;

                            //Agregamos la referencia de la etiqueta generada a la publicacion
                            relPublicacionEtiqueta publicacionEtiqueta = new relPublicacionEtiqueta();
                            publicacionEtiqueta.idEtiqueta = claveEtiqueta;
                            publicacionEtiqueta.idPublicacion = clave;
                            bd.relPublicacionEtiqueta.Add(publicacionEtiqueta);
                            bd.SaveChanges();

                            result = true;
                        }


                    }//Fin del foreach
                }


            }
            catch (Exception e)
            {
            }


            return result;
        }

    }


      /// <summary>
    /// Clase para el manejo de acciones pertinentes a las publicaciones
    /// </summary>
    public class PublicacionService
    {
        /// <summary>
        /// Función que genera una nueva publicación como borrador
        /// </summary>
        /// <param name="clave">Parametro opcional, en caso que la publicacion ya exista como borrador</param>
        /// <param name="titulo">Titulo de la publicacion</param>
        /// <param name="contenido">Cadena con el contenido html de la publicacion</param>
        /// <param name="tipo">Valor booleano que especifica si la noticia es publica o privada</param>
        /// <param name="bFechaImportante">Bandera que indica si la noticia privada aparecera en la seccion fechas importantes</param>
        /// <returns>Instancia del modelo de publicación</returns>
        public static Publicacion generarBorrador(int clave, String titulo, String contenido, Boolean tipo, Boolean bFechaImportante)
        {
            //Instancia del modelo abstracto
            Publicacion borrador = null;

            //Instancia del modelo publicacion de la base de datos
            publicacion borradorBD = new publicacion();

            //Creamos una conexion a la base de datos
            PortalPadresEntities bd = new PortalPadresEntities();

            try
            {
                //Si la clave es distinta de 0, entonces el borrador ya existe
                if (clave != 0)
                {

                    //Buscamos el registro del borrador generado anteriormente
                    borradorBD = bd.publicacion.FirstOrDefault(p => p.idPublicacion == clave);

                    if (borradorBD != null)
                    {
                        //Actualizamos los atributos del registro
                        borradorBD.titulo = titulo;
                        borradorBD.contenido = contenido;
                        borradorBD.fechaModificacion = DateTime.Now;
                        borradorBD.idEstatus = bd.estatusPublicacion.SingleOrDefault(e => e.nombre == "Borrador").idEstatus;
                        borradorBD.publico = tipo;

                        borradorBD.bFechaImportante = bFechaImportante;

                        bd.SaveChanges();


                        //Una vez actualizado el borrador mapeamos este objeto a una instancia del modelo de publicacion

                        //Verificamos la clave de la publicacion generada
                        borradorBD.idPublicacion = bd.Entry(borradorBD).Entity.idPublicacion;

                        //Mapeamos los atributos
                        borrador = Publicacion.mapear(borradorBD);

                    }



                }
                else
                //En caso contrario, la publicación no existe
                {
                    //Asignamos los atributos para la nueva publicacion
                    borradorBD.titulo = titulo;
                    borradorBD.contenido = contenido;
                    borradorBD.fechaModificacion = DateTime.Now;
                    borradorBD.idEstatus = bd.estatusPublicacion.SingleOrDefault(e => e.nombre == "Borrador").idEstatus;
                    borradorBD.publico = tipo;
                    borradorBD.bFechaImportante = bFechaImportante;

                    bd.publicacion.Add(borradorBD);
                    bd.SaveChanges();

                    //Una vez generado el borrador mapeamos este objeto a una instancia del modelo de publicacion

                    //Recuperamos la clave de la solicitud generada
                    borradorBD.idPublicacion = bd.Entry(borradorBD).Entity.idPublicacion;

                    //Mapeamos los atributos
                    borrador = Publicacion.mapear(borradorBD);

                }
            }
            catch (Exception e)
            {

            }


            return borrador;
        }

        /// <summary>
        /// Sobrecarga del metodo generarBorrador.
        /// Genera una nueva publicacion como borrador, si se carga una imagen desde el panel de edicion.
        /// </summary>
        /// <param name="clave">Clave de la publicacion. Este parametro es opcional, puede ser 0 o un valor no negativo distinto de 0</param>
        /// <param name="imagen">Arreglo de bytes que contiene la imagen de la publicacion</param>
        /// <param name="tipoPublicacion">Parametro opcional, especifica el tipo de noticia, publica o privada</param>
        /// <returns>Retorna un objeto del modelo de publicacion</returns>
        public static Publicacion generarBorrador(int clave, byte[] imagen, Boolean tipoPublicacion)
        {

            //Instancia a devolver
            Publicacion borrador = null;

            try
            {
                //Creamos una conexion a la base de datos
                PortalPadresEntities bd = new PortalPadresEntities();

                //Instancia del modelo publicacion de la base de datos
                publicacion borradorBD = new publicacion();

                //Comprobamos primero si la publicacion existe o sera una nueva publicacion
                //La publicacion existe
                if (clave != 0)
                {

                    //Buscamos la publicacion mediante su clave
                    borradorBD = bd.publicacion.FirstOrDefault(p => p.idPublicacion == clave);

                    //Comprobamos si la publicacion es valida
                    if (borradorBD != null)
                    {

                        //Actualizamos los atributos de esta publicacion
                        borradorBD.fechaModificacion = DateTime.Now;
                        borradorBD.idEstatus = bd.estatusPublicacion.SingleOrDefault(ep => ep.nombre == "Borrador").idEstatus;
                        borradorBD.publico = tipoPublicacion;
                        borradorBD.imagen = imagen;
                        borradorBD.bFechaImportante = false;

                        bd.SaveChanges();

                        //Una vez generado el borrador mapeamos este objeto a una instancia del modelo de publicacion

                        //Recuperamos la clave de la solicitud generada
                        borradorBD.idPublicacion = bd.Entry(borradorBD).Entity.idPublicacion;

                        //Mapeamos los atributos
                        borrador = Publicacion.mapear(borradorBD);
                    }

                }
                //La publicacion no existe
                else
                {
                    //Asignamos los atributos para la nueva publicacion

                    
                    borradorBD.titulo = "Publicacion sin titulo";
                    borradorBD.contenido = "";

                    borradorBD.fechaModificacion = DateTime.Now;
                    borradorBD.idEstatus = bd.estatusPublicacion.SingleOrDefault(e => e.nombre == "Borrador").idEstatus;
                    borradorBD.publico = tipoPublicacion;

                    borradorBD.imagen = imagen;
                    borradorBD.bFechaImportante = false;

                    bd.publicacion.Add(borradorBD);
                    bd.SaveChanges();

                    //Una vez generado el borrador mapeamos este objeto a una instancia del modelo de publicacion

                    //Recuperamos la clave de la solicitud generada
                    borradorBD.idPublicacion = bd.Entry(borradorBD).Entity.idPublicacion;

                    //Mapeamos los atributos
                    borrador = Publicacion.mapear(borradorBD);

                }


            }
            catch (Exception e)
            {


            }


            return borrador;
        }


        /// <summary>
        /// Función que hace publica una noticia existente
        /// </summary>
        /// <param name="Clave">Clave de la publicación</param>
        /// <returns>Valor booleano que indica si se efectuo la operacion</returns>
        public static Boolean publicar(int clave)
        {
            Boolean publicado = false;

            try
            {
                //Creamos una conexion a la base se datos
                PortalPadresEntities bd = new PortalPadresEntities();

                //Instancia que almacenara los resultados de la consulta 
                publicacion publicacionBD = new publicacion();

                //Buscamos la publicacion guardada como borrador anteriormente
                publicacionBD = bd.publicacion.FirstOrDefault(p => p.idPublicacion == clave);

                if (publicacionBD != null)
                {
                    //Actualizamos los atributos de esta publicacion
                    publicacionBD.fechaPublicacion = DateTime.Now;
                    publicacionBD.idEstatus = bd.estatusPublicacion.SingleOrDefault(ep => ep.nombre == "Publicado").idEstatus;

                    //Guardamos los cambios
                    bd.SaveChanges();
                    publicado = true;
                }



            }
            catch (Exception e)
            {

            }

            return publicado;
        }


        /// <summary>
        /// Sobrecarga de la funcion publicar. 
        /// Hace publica una noticia nueva o existente
        /// </summary>
        /// <param name="clave">Clave de la publicacion</param>
        /// <param name="titulo">Titulo de la publicacion</param>
        /// <param name="contenido">Cadena con contenido html de la publicacion</param>
        /// <param name="noticiaPublica">Indicador booleano que especifica si la noticia es publica o privada</param>
        /// <param name="bFechaImportante">Bandera que indica si la noticia privada se mostrara en la sección fechas importantes</param>
        /// <returns>Retorna un valor booleano que indica si se completo la operacion</returns>
        public static Boolean publicar(int clave, String titulo, String contenido, Boolean noticiaPublica, Boolean bFechaImportante) 
        {
            Boolean publicado = false;

            try 
            {
                //Abrimos una conexion a la base de datos 
                PortalPadresEntities bd = new PortalPadresEntities();
            
                //Verificamos si es nueva publicacion o publicacion existente
                
                //Publicacion existente
                if (clave != 0)
                {
                    //Recuperamos la publicacion
                    publicacion publicacionBD = bd.publicacion.FirstOrDefault(p => p.idPublicacion == clave);


                    if (publicacionBD != null) 
                    {
                        //Actualizamos los campos

                        publicacionBD.titulo = titulo;
                        publicacionBD.contenido = contenido;
                        publicacionBD.publico = noticiaPublica;
                        publicacionBD.idEstatus = 1; //Estatus publicado
                        publicacionBD.fechaPublicacion = DateTime.Now;
                        publicacionBD.fechaModificacion = DateTime.Now;
                        publicacionBD.bFechaImportante = bFechaImportante;

                        //Guardamos los cambios
                        bd.SaveChanges();
                        publicado = true;
                    }


                }
               //Nueva publicacion
                else 
                {
                    publicacion nuevaPublicacion = new publicacion();
                    nuevaPublicacion.titulo = titulo;
                    nuevaPublicacion.contenido = contenido;
                    nuevaPublicacion.publico = noticiaPublica;
                    nuevaPublicacion.idEstatus = 1;//Estatus publicado
                    nuevaPublicacion.fechaPublicacion = DateTime.Now;
                    nuevaPublicacion.fechaModificacion = DateTime.Now;
                    nuevaPublicacion.bFechaImportante = bFechaImportante;

                    //Guardamos los cambios
                    bd.publicacion.Add(nuevaPublicacion);
                    bd.SaveChanges();
                    publicado = true;
                
                }

            }
            catch(Exception e)
            {
            
            }


            return publicado;
        }

        /// <summary>
        /// Función que realiza una baja logica de una publicación. 
        /// </summary>
        /// <param name="Clave">Clave de la publicacón</param>
        /// <returns>Valor booleano que indica si se efectuo la operacion</returns>
        public static Boolean baja(int clave)
        {
            Boolean baja = false;


            try
            {
                //Abrimos una conexion con la base de datos
                PortalPadresEntities bd = new PortalPadresEntities();

                //Buscamos la publicacion que se dara de baja
                publicacion publicacionBD = bd.publicacion.FirstOrDefault(p => p.idPublicacion == clave);

                //Si encontramos la publicación
                if (publicacionBD != null)
                {
                    //Actualizamos los atributos

                    publicacionBD.fechaBaja = DateTime.Now;
                    publicacionBD.idEstatus = bd.estatusPublicacion.SingleOrDefault(ep => ep.nombre == "Eliminado").idEstatus;

                    //Guardamos los cambios
                    bd.SaveChanges();

                    baja = true;
                }

            }
            catch (Exception e)
            {

            }



            return baja;
        }



        /// <summary>
        /// Función que modifica una noticia publicada a borrador
        /// </summary>
        /// <param name="clave">Clave de la publicación</param>
        /// <returns>Valor boleano que indica si se completo la operación</returns>
        public static Boolean volverBorrador(int clave)
        {
            Boolean success = false;


            try
            {

                //Creamos una conexion a la base de datos
                PortalPadresEntities bd = new PortalPadresEntities();

                //Buscamos la publicacion mediante su clave
                publicacion publicacionBD = bd.publicacion.FirstOrDefault(p => p.idPublicacion == clave);

                //Si encontramos publicación
                if (publicacionBD != null)
                {
                    //Actualizamos los atributos
                    publicacionBD.fechaModificacion = DateTime.Now;
                    publicacionBD.idEstatus = bd.estatusPublicacion.SingleOrDefault(ep => ep.nombre == "Borrador").idEstatus;

                    //Guardamos los cambios
                    bd.SaveChanges();
                    success = true;
                }



            }
            catch (Exception e)
            {

            }



            return success;
        }

        /// <summary>
        /// Función que obtiene una publicación
        /// </summary>
        /// <param name="clave">Clave de la publicación</param>
        /// <returns>Instancia del modelo de publicación</returns>
        public static Publicacion obtener(int clave)
        {
            //Instancia a devolver
            Publicacion publicacion = null;

            //Instancia que almacenara el resultado de la consulta
            publicacion publicacionBD = new publicacion();

            //Establecemos una conexion con la base de datos
            PortalPadresEntities bd = new PortalPadresEntities();

            try
            {
                //Buscamos la publicación por medio de su clave
                publicacionBD = bd.publicacion.FirstOrDefault(p => p.idPublicacion == clave);

                //Si encontramos la publicación
                if (publicacionBD != null)
                {
                    //Inicializamos el objeto a devolver
                    publicacion = new Publicacion();

                    //Mapeamos los atributos del objeto de la bd a un modelo abstracto
                    publicacion = Publicacion.mapear(publicacionBD);
                }


            }
            catch (Exception e)
            {


            }

            return publicacion;
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
        public static List<Publicacion> Buscar(Dictionary<String, Object> filtros, String orden_columna, String orden_direccion, int inicio, int limite, out int total)
        {
            /* Declaramos e inicializamos las variables de salida*/
            List<Publicacion> resultados = new List<Publicacion>();

            total = 0;

            /* Abrimos la conexión a la base de datos */
            using (PortalPadresEntities dbContext = new PortalPadresEntities())
            {
                /*Generamos el query que se usará */
                IQueryable<publicacion> query = dbContext.publicacion;

                /* Variable para las clausulas de filtrado */
                List<String> clausulas = new List<String>();

                foreach (String key in filtros.Keys)
                {
                    switch (key)
                    {

                        case "PublicacionesActivas":
                            clausulas.Add(String.Format("idEstatus == 1 || idEstatus == 2", filtros[key]));
                            break;
                        case "PublicacionesEliminadas":
                            clausulas.Add(String.Format("idEstatus == 3", filtros[key]));
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
                foreach (publicacion item in query)
                {
                    /* Creamos una nueva instancia de nuestro modelo de resultado */

                    Publicacion registro = new Publicacion();

                    registro.clave = item.idPublicacion;
                    registro.titulo = item.titulo;
                    registro.contenido = item.contenido;

                    registro.fechaPublicacion = item.fechaPublicacion;
                    registro.fechaModificacion = item.fechaModificacion;
                    registro.fechaBaja = item.fechaBaja;

                    if (item.imagen != null) 
                    {
                        registro.imagen = item.imagen;
                        registro.imagenCodificada = Convert.ToBase64String(item.imagen);
                    }

                    registro.claveEstatusPublicacion = (int)item.idEstatus;
                    registro.nombreEstatusPublicacion = item.estatusPublicacion.nombre;

                    /* Agregamos el regitro a la lista de resultados */
                    resultados.Add(registro);

                }
            }

            /* Regresamos la lista de solicitudes*/
            return resultados;
        }



        /// <summary>
        /// Función que obtiene un bloque de noticias, en base a una serie de criterios como secuencia de paginas, numero de resultados por pagina, indicadorde noticia publica o privada.
        /// </summary>
        /// <param name="pagina">Pagina actual, comienza en 1 en la primera iteración</param>
        /// <param name="registrosPorPagina">Cantidad de registros por pagina</param>
        /// <param name="tipoPublicacion">Tipo de publicacion. Si es true es noticia publica, si es false es privada</param>
        /// <param name="fechaImportante">Bandera que indica si la noticia privada se mostrara en la seccion fechas</param>
        /// <returns>Retorna una lista del modelo de publicación, con la cantidad indicada de registros por página.
        /// Si retorna null, se llego al tope de publicaciones por pagina o no existen registros con las condiciones especificadas.
        /// </returns>
        public static List<Publicacion> obtenerBloqueNoticias(int pagina, int registrosPorPagina, Boolean tipoPublicacion, Boolean fechaImportante)
        {

            List<Publicacion> bloquePublicaciones = null;

            try
            {
                //Abrimos una conexion a la base de datos
                PortalPadresEntities bd = new PortalPadresEntities();


                //Establecemos el indice desde el cual comenzaremos a recuperar registros
                int indice = (pagina - 1) * registrosPorPagina;


                //Preparamos la consulta con los registros a recuperar
                var query = (from tblPublicacion in bd.publicacion
                             where tblPublicacion.idEstatus == 1 && tblPublicacion.publico == tipoPublicacion &&  tblPublicacion.bFechaImportante == fechaImportante
                             orderby tblPublicacion.fechaPublicacion descending
                             select tblPublicacion)
                            .Skip(indice).Take(registrosPorPagina).ToList();

                //Verificamos si se encontraron resultados
                if (query.Count() > 0)
                {
                    //Inicializamos la lista 
                    bloquePublicaciones = new List<Publicacion>();

                    //Iteramos en la coleccion recuperada
                    foreach (publicacion p in query)
                    {
                        //Mapeamos los atributos del objeto recuperado y lo agregamos a la lista de resultados
                        bloquePublicaciones.Add(Publicacion.mapear(p));
                    }
                }

            }

            catch (Exception e)
            {
            }

            return bloquePublicaciones;
        }

        


     

    }
}


}