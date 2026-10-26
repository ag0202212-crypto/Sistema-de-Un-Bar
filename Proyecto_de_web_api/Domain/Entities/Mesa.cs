using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Mesa
    {
        public int id_mesa {  get; set; }
        public int numero_de_mesa { get; set; }
        public int capacidad_de_persona { get; set; }
        public string? estado { get; set; }
        public bool activo { get; set; }
        public int id_zona { get; set; }
    }
}
