using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Detalle_Comanda
    {
        public int id_detalle_comanda { get; set; }
        public int cantidad_pedida { get; set; }
        public double precio_de_venta { get; set; }
        public string? nota_especial { get; set; }
        public int id_comanda { get; set; }
        public int id_producto { get; set; }
    }
}
