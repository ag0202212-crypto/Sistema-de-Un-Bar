using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Categoria
    {
        public int id_categoria { get; set; }
        public string? nombrecat { get; set; }
        public bool activo { get; set; }
    }
}
