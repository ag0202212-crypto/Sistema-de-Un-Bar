using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Comanda
    {
        public int id_comanda { get; set; }
        public DateTime fecha_y_hora_de_apertura { get; set; }
        public string? estado { get; set; }
        public int id_mesa { get; set; }
        public int id_usuario { get; set; }
    }
}
