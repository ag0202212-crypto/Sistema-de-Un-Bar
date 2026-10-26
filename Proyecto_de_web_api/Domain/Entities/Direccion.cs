using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Direccion
    {
        public int id_direccion { get; set; }
        public string? departamento { get; set; }
        public string? Municipio { get; set; }
        public string? detalle_de_direccion { get; set; }
        public bool activo { get; set; }
    }
}
