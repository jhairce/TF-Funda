using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class LineaPago
    {
        public int IdLineaPago { get; set; }
        public ServiciosA serviciosa { get; set; }
        public decimal Tarifa
        {
            get { return serviciosa.Precio; }
        }
    }
}
