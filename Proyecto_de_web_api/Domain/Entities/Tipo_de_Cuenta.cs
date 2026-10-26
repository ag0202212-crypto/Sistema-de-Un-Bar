using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Tipo_de_Cuenta
    {
        public int id_tipo_cuenta { get; set; }
        public string? nombre_tipo_cuenta { get; set; }
        public bool activo { get; set; }
    }
}
