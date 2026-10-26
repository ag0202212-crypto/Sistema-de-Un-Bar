using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTos
{
    public class ProductoDTos
    {
        public int id_producto { get; set; }
        public string? nombre { get; set; }
        public double precio_venta { get; set; }
        public double precio_de_costo { get; set; }
        public int stock_actual { get; set; }
        public int stock_minimo { get; set; }
        public bool activo { get; set; }
        public int id_categoria { get; set; }
    }
}
