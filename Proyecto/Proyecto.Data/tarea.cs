//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tarea
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int prioridad { get; set; }
        public Nullable<System.DateTime> fecha_creada { get; set; }
        public Nullable<System.DateTime> fecha_completada { get; set; }
        public Nullable<bool> exitosa { get; set; }
    }
}
