using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entidades
{
    public class LineaCarac
    {
        public int IdLineaCarac { get; set; }
        public Carac carac { get; set; }
        public Habitacion habitacion { get; set; }
        public decimal Importe
        {
            get { return carac.Precio; }
        }
        public override string ToString()
        {
            return carac.Nombre;
        }
    }
}
