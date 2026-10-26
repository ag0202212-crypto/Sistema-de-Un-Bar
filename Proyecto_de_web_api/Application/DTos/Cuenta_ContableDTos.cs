using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTos
{
    public class Cuenta_ContableDTos
    {
        public int id_cuenta_contable { get; set; }
        public string? cod_cuenta { get; set; }
        public string? nombre_cuenta { get; set; }
        public bool activo { get; set; }
        public int id_tipo_cuenta { get; set; }
    }
}
