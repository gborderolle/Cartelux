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
    
    public partial class pedido_entregas
    {
        public int Pedido_Entrega_ID { get; set; }
        public int Entrega_Tipo_ID { get; set; }
        public Nullable<int> Departamento_Tipo_ID { get; set; }
        public string Direccion { get; set; }
        public string Indicaciones { get; set; }
        public string Barrio { get; set; }
        public string Ciudad { get; set; }
        public string Fecha_entrega { get; set; }
        public string Comentarios { get; set; }
        public string Google_maps_URL { get; set; }
        public string Coordenadas_Y { get; set; }
        public string Coordenadas_X { get; set; }
    }
}
