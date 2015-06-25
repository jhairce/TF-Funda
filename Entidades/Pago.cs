using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pago
    {
        public int IdPago { get; set; }
        public Reserva reserva { get; set; }
        public List<LineaPago> lstlineapago { get; set; }
        public decimal SubTotal { get; set; }
        public decimal IGV { get; set; }//IMPLEMENTAR AKI EL GET{0.18*SUBTOTAL}
        public decimal Total { get; set; }
    }
}
