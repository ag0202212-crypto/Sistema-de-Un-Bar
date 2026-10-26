using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Zona
    {
        public int  id_zona { get; set; }
        public string? nombre_zona { get; set; }
        public bool activo { get; set; }
    }
}
