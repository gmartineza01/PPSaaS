using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using PPSaaS.Models;

namespace PPSaaS.Common
{
    /// <summary>
    /// Clase que proporciona servicios para la administracion de roles
    /// </summary>
    public class PPSaaSRoleProvider: RoleProvider
    {

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

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

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo que obtiene todos los roles de la aplicación
        /// </summary>
        /// <returns>Arreglo de string que contiene los roles utilizados por la aplicación</returns>
        public override string[] GetAllRoles()
        {
            return new string[] { "ADMINISTRADOR GENERAL", "ADMINISTRADOR ESCOLAR", "DIRECTOR", "EMPLEADO ESCOLAR", "MAESTRO", "PADRE DE FAMILIA" };
        }

        /// <summary>
        /// Metodo que obtiene los roles para el usuario especificado
        /// </summary>
        /// <param name="username">Nombre de la cuenta, nomina o correo electronico</param>
        /// <returns>Regresa un arreglo de string con los roles para el usuario especificado</returns>
        public override string[] GetRolesForUser(string username)
        {
            return UsuarioService.obtenerRoles(username);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo que verifica si un usuario tiene el rol especificado
        /// </summary>
        /// <param name="username">Nombre del usuario</param>
        /// <param name="roleName">Nombre del rol</param>
        /// <returns>Valor booleano que indica si el usuario tiene el rol especificado</returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            String[] roles = UsuarioService.obtenerRoles(username);

            return roles != null && roles.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo que verifica si el rol especificado existe
        /// </summary>
        /// <param name="roleName">Nombre del rol</param>
        /// <returns>Valor boleano que indica si el rol existe</returns>
        public override bool RoleExists(string roleName)
        {
            return GetAllRoles().Contains(roleName);
        }

    }
}