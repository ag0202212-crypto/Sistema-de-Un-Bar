using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Factura
    {
        public int id_factura { get; set; }
        public string? nombre_factura { get; set; }
        public DateTime fecha_y_hora_de_cierre { get; set; }
        public double subtotal { get; set; }
        public double iva { get; set; }
        public double total { get; set; }
        public string? estado_de_la_factura { get; set; }
        public int id_comanda { get; set; }
        public int? id_cliente { get; set; }
    }
}
