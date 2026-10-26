using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTos
{
    public class ClienteDTos
    {
        public int id_cliente { get; set; }
        public string? nombre { get; set; }
        public string? cedula { get; set; }
        public string? telefono { get; set; }
        public bool activo { get; set; }
        public int id_direccion { get; set; }
        public string? departamento { get; set; }
        public string? municipio { get; set; }
        public string? detalle_direccion { get; set; }
    }
}
