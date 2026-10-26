using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTos
{
    public class Tipo_de_CuentaDTos
    {
        public int id_tipo_cuenta { get; set; }
        public string? nombre_tipo_cuenta { get; set; }
        public bool activo { get; set; }
    }
}
