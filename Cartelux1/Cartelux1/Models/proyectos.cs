//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cartelux1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class proyectos
    {
        public int Proyecto_ID { get; set; }
        public int Proyecto_estado_ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Nullable<System.DateTime> Fecha_estimada { get; set; }
        public string Contacto_1_nombre { get; set; }
        public string Contacto_1_telefono { get; set; }
        public string Contacto_1_email { get; set; }
        public string Contacto_2_nombre { get; set; }
        public string Contacto_2_telefono { get; set; }
        public string Contacto_2_email { get; set; }
        public string Comentarios { get; set; }
        public System.DateTime Fecha_creado { get; set; }
        public System.DateTime Fecha_update { get; set; }
    }
}
