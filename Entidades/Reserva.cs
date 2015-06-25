using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public Huesped huesped { get; set; }
        public List<LineaReserva> lineareserva { get; set; }
        public DateTime FechaIng { get; set; }
        public DateTime FechaSal { get; set; }
        public decimal Total { get; set; }
    }
}
