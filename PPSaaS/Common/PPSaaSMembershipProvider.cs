using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using PPSaaS.Models;

namespace PPSaaS.Common
{
    /// <summary>
    /// Clase que proporciona servicios para la administracion de sesiones de usuario
    /// </summary>
    public class PPSaaSMembershipProvider: MembershipProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Función para obtener los datos del usuario en base a su username (nómina o matrícula)
        /// </summary>
        /// <param name="username">Cuenta o Nómina a buscar</param>
        /// <param name="userIsOnline">Indicador de usuarios en línea</param>
        /// <returns></returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipUser usuario = null;


            // Revisamos si el usuario esta en la base de datos
            if (UsuarioService.existeUsuarioBD(username))
            {
                // Si el usuario existe en la base de datos, tomamos sus datos para el membership user de respuesta               
                Usuario userdata = UsuarioService.obtener(username);
               
                usuario = new MembershipUser(userdata.nombreCuenta ?? userdata.nomina,
                                             //userdata.nombreCuenta ?? userdata.nomina,
                                             userdata.nombreCompleto,
                                             userdata.clave,
                                             userdata.correo,
                                             "",
                                             "",
                                             true,
                                             false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.MinValue);
            };



            return usuario;
        }

        /// <summary>
        /// Función para obtener los datos del usuario en base a su clave de usuario
        /// </summary>
        /// <param name="providerUserKey">Clave de usuario. Es númerica</param>
        /// <param name="userIsOnline">Indicador de usuarios en línea</param>
        /// <returns></returns>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            //MembershipUser usuario = null;
            //// Variable para almacenar la clave de forma númerica
            //int clave = 0;
            //// Si la clave recibida es númerica, procedemos a buscar el usuario en la base de datos
            //if (Int32.TryParse(providerUserKey.ToString(), out clave))
            //{

            //    // Revisamos si el usuario esta en la base de datos    
            //    if (UsuarioService.ExisteUsuarioBD(clave))
            //    {
            //        // Si el usuario existe en la base de datos, tomamos sus datos para el membership user de respuesta
            //        Usuario userdata = UsuarioService.Obtener(clave);
            //        usuario = new MembershipUser(userdata.matricula ?? userdata.nomina, userdata.matricula ?? userdata.nomina, userdata.clave, userdata.correo, "", "", true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.MinValue);
            //    };
            //}
            //// Regrsamos el usaurio
            //return usuario;

            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo que verifica si el usuario y contraseña son válidos
        /// </summary>
        /// <param name="username">Nombre de usuario a validar</param>
        /// <param name="password">Contraseña para validar</param>
        /// <returns>Bandera que indica si la combinación de usuario y contraseña son correctos</returns>
        public override bool ValidateUser(string username, string password)
        {
            return !String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password) && UsuarioService.validarCredenciales(username, password);
        }


    }
}