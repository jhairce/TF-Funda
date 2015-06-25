using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class LineaReserva
    {
        public int IdLineaReserva { get; set; }
        public Empleado empleado { get; set; }
        public Habitacion habitacion { get; set; }
        public decimal Precio
        {
            get { return habitacion.Tarifa; }
        }
    }
}
