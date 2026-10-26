using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string? nombre { get; set; }
        public string? cedula { get; set; }
        public string? rol { get; set; }
        public string? contraseña { get; set; }

        public bool activo { get; set; }
    }
}
