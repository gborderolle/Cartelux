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
    
    public partial class clientes_frecuentes
    {
        public int Cliente_Frecuente_ID { get; set; }
        public int Cliente_ID { get; set; }
        public string Frecuencia { get; set; }
        public string Descuento { get; set; }
        public string Comentarios { get; set; }
        public Nullable<System.DateTime> Ultimo_pedido { get; set; }
    }
}
