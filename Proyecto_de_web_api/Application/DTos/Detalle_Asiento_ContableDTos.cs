using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTos
{
    public class Detalle_Asiento_ContableDTos
    {
        public int id_detalle_asiento_contable { get; set; }
        public double monto_debe { get; set; }
        public double monto_haber { get; set; }
        public int id_asiento_contable { get; set; }
        public int id_cuenta_contable { get; set; }
    }
}
