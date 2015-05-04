using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPSaaS.Models.EntityModel;


namespace PPSaaS.Models
{
    /// <summary>
    /// Clase que representa una abstracción de un usuario
    /// </summary>
    public class Usuario
    {
        public int clave { get; set; }
        public String nombre { get; set; }
        public String apellidoPaterno { get; set; }
        public String apellidoMaterno { get; set; }
        public String nombreCompleto{ get { return nombre + " " + apellidoPaterno + " " + apellidoMaterno; }} 
        
        public String nombreCuenta { get; set; }
        public String nomina { get; set; }

        public String contrasena { get; set; }
        public String correo { get; set; }
        public String sal { get; set; }
        
        
        public Boolean activo { get; set; }
        public Domicilio domicilio { get; set; }
        public DateTime? ultimoAcceso { get; set; }

        public String telefonoFijo { get; set; }
        public String extension { get; set; }
        public String telefonoMovil { get; set; }

        public String[] roles { get; set; }

        public String tituloTrato { get; set; }

  


        /// <summary>
        /// Metodo que convierte un objeto usuario de la base de datos a un modelo abstracto
        /// </summary>
        /// <param name="usuarioBD">Modelo de usuario de la base de datos</param>
        /// <returns>Modelo abstracto de usuario</returns>
        public static Usuario mapear(usuario usuarioBD) 
        {
            Usuario usuario = null;

            try
            {

                if(usuarioBD != null)
                {

                    usuario = new Usuario();

                    usuario.clave = usuarioBD.idUsuario;
                    usuario.nombre = usuarioBD.nombre;
                    usuario.apellidoPaterno = usuarioBD.apellidoPaterno;
                    usuario.apellidoMaterno = usuarioBD.apellidoMaterno;

                    usuario.nombreCuenta = usuarioBD.cuenta;
                    usuario.nomina = usuarioBD.nomina;
                    usuario.correo = usuarioBD.correoElectronico;

                    usuario.activo = usuarioBD.bActivo;
                    
                    Domicilio domicilio = new Domicilio();

                    domicilio.calle = usuarioBD.direccion_calle;
                    domicilio.ciudad = usuarioBD.direccion_ciudad;
                    domicilio.codigoPostal = usuarioBD.direccion_cp;
                    //domicilio.colonia = usuarioBD.dire
                    //domicilio.entreCalles = usuarioBD.

                    domicilio.estado = usuarioBD.direccion_estado;
                    domicilio.numeroExterior = usuarioBD.direccion_numero;
                    domicilio.pais = usuarioBD.direccion_pais;

                    usuario.domicilio = domicilio;

                    usuario.ultimoAcceso = usuarioBD.fechaUltimoAcceso;
                    usuario.telefonoFijo = usuarioBD.contacto_telefono;
                    usuario.extension = usuarioBD.contacto_extension;
                    usuario.telefonoMovil = usuarioBD.contacto_movil;

                    usuario.tituloTrato = usuarioBD.tituloTrato;
                    usuario.roles = UsuarioService.obtenerRoles(usuarioBD.cuenta);
               
                }
            }
            catch(Exception e)
            {            
            }

            return usuario;
        }


  

    }

    /// <summary>
    /// Clase que representa una abstraccion de un alumno
    /// </summary>
    public class Alumno 
    {
        public int clave { get; set; }
        public Escuela escuela { get; set; }
        public String nombre { get; set; }
        public String apellidoPaterno { get; set; }
        public String apellidoMaterno { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public String tipoSangre { get; set; }
        public String nacionalidad{get;set;}
        public Boolean bActivo { get; set; }
        public Boolean autorizacionMedicamento { get; set; }
            

        /// <summary>
        /// Constructor, inicializa atributos de tipo clase
        /// </summary>
        public Alumno()
        {
            escuela = new Escuela();
        }

        /// <summary>
        /// Metodo que convierte un objeto alumno de la base de datos a un modelo abstracto de alumno
        /// </summary>
        /// <param name="alumnoBD">Instancia del modelo de alumno de la base de datos</param>
        /// <returns>Instancia del modelo abstracto de alumno</returns>
        public static Alumno mapear(alumno alumnoBD)
        {
            Alumno alumno = null;
        
            try
            {
                if(alumnoBD != null)
                {
                    alumno.clave = alumnoBD.idAlumno;
                    alumno.nombre = alumnoBD.nombre;
                    alumno.apellidoPaterno = alumnoBD.apellidoPaterno;
                    alumno.apellidoMaterno = alumnoBD.apellidoMaterno;
                    alumno.fechaNacimiento = alumnoBD.fechaNacimiento;
                    alumno.nacionalidad = alumnoBD.nacionalidad;
                    alumno.tipoSangre = alumnoBD.tipoSangre;
                    alumno.autorizacionMedicamento = alumnoBD.bAutorizacionMedicamento;
                    alumno.bActivo = alumnoBD.bActivo;
                    alumno.escuela = alumno.escuela;
                }
            }
            catch(Exception e)
            {
            }

            return alumno;
        }




    }

    /// <summary>
    /// Clase que representa una abstracción de un domicilio
    /// </summary>
    public class Domicilio 
    {
        public String calle { get; set; }
        public String numeroExterior { get; set; }
        public String numeroInterior { get; set; }
        public String entreCalles { get; set; }
        public String colonia { get; set; }
        public String ciudad { get; set; }
        public String estado { get; set; }
        public String pais { get; set; }
        public String codigoPostal { get; set; }


        public static Boolean crear(String calle, String numeroExterior, String numeroInterior, String aaa ) 
        {
            Boolean res = false;




            return res;
        }



    
    }

    /// <summary>
    /// Clase que proporciona servicios para la administracion de usuarios
    /// </summary>
    public static class UsuarioService 
    {
        /// <summary>
        /// Metodo que verifica si las credenciales proporcionadas son validas para el inicio de sesion
        /// </summary>
        /// <param name="cuenta">Nombre de la cuenta, nomina o correo electronico del usuario</param>
        /// <param name="contrasena">Contraseña de acceso</param>
        /// <returns>Regresa un valor boleano indicando si se completo la operacion</returns>
        public static Boolean validarCredenciales(String cuenta, String contrasena)
        {
            Boolean res = false;

            //Abrimos una conexion a la base de datos
            using (SaaSEntities dbContext = new SaaSEntities()) 
            {
                usuario usuarioBD = dbContext.usuario.FirstOrDefault(x => x.cuenta == cuenta && x.contrasena == contrasena || 
                                                                     x.nomina == cuenta &&  x.contrasena == contrasena || 
                                                                     x.correoElectronico == cuenta && x.contrasena == contrasena &&
                                                                     x.bActivo);
                if (usuarioBD != null) res = true;
            }
      

            return res;
        }


        /// <summary>
        /// Metodo que obtiene un usuario
        /// </summary>
        /// <param name="username">Nombre de usuario, nomina o correo electronico</param>
        /// <returns>Instancia del modelo abstracto de usuario</returns>
        public static Usuario obtener(String username) 
        {
            Usuario usuario = null;

            using(SaaSEntities DBContext = new SaaSEntities())
            {
                usuario usuarioBD = DBContext.usuario.FirstOrDefault(u => u.cuenta == username || u.nomina == username || u.correoElectronico == username);

                if (usuarioBD != null) 
                {
                    usuario = Usuario.mapear(usuarioBD);
                }
            }


            return usuario;

        }

        /// <summary>
        /// Metodo que obtiene los roles de un usuario
        /// </summary>
        /// <param name="username">Nombre de la cuenta, numero de nomina o correo electronico</param>
        /// <returns>Regresa un arreglo con los roles del usuario</returns>
        public static String[] obtenerRoles(String username)
        {
            String[] roles = null;

            //username = "gmartinez";

            using (SaaSEntities DBContext = new SaaSEntities())
            {
                //Obtenemos los roles activos del usuario
                var rolUsuario = DBContext.relUsuarioRol.Where(u => u.usuario.cuenta == username || u.usuario.nomina == username || u.usuario.correoElectronico == username && u.bActivo);

                if (rolUsuario != null)
                {
                    roles = new String[rolUsuario.Count()];

                    int count = 0;
                    foreach (relUsuarioRol r in rolUsuario)
                    {
                        roles[count] = r.rol.nombre;
                        count++;
                    }
                }
            }


            return roles;
        } 

        /// <summary>
        /// Funcion que comprueba si la cuenta especificada existe en la base de datos
        /// </summary>
        /// <param name="username">Nombre de la cuenta, nomina o correo electronico</param>
        /// <returns>Regresa una bandera que indica si se encuentra esta cuenta registrada</returns>
        public static Boolean existeUsuarioBD(String username) 
        {
            Boolean res = false;

            using(SaaSEntities DBContext = new SaaSEntities())
            {
                res = DBContext.usuario.Where(u => u.cuenta == username || u.nomina == username || u.correoElectronico == username).Any();    
            }

            return res;
        }



        public static Boolean crear(String nombre, String apellidoPaterno, String apellidoMaterno, 
                                    String cuenta, String nomina, String contraseña, String[] roles)
        {


            Boolean exito = false;
        
            
            try
            {
                using(SaaSEntities DBContext = new SaaSEntities())
                {


                
                
                }
            
            
            }
            catch(Exception e)
            {
            }

            return exito;
        }




    

    
    }



}