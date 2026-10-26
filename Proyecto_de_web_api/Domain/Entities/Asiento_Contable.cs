using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Asiento_Contable
    {
        public int id_asiento_contable { get; set; }
        public DateTime fecha_asiento { get; set; }
        public string? glosa { get; set; }
        public int id_factura { get; set; }
    }
}
