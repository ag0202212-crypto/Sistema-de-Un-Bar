using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTos
{
    public class KardexDTos
    {
        public int id_kardex { get; set; }
        public string? tipo_de_movimiento { get; set; }
        public int cantidad { get; set; }
        public DateTime fecha { get; set; }
        public int id_producto { get; set; }
    }
}
