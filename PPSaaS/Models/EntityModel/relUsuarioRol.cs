//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PPSaaS.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class relUsuarioRol
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public int idRol { get; set; }
        public Nullable<int> idEscuela { get; set; }
        public bool bActivo { get; set; }
        public Nullable<System.DateTime> fechaModificacion { get; set; }
    
        public virtual escuela escuela { get; set; }
        public virtual rol rol { get; set; }
        public virtual usuario usuario { get; set; }
    }
}
