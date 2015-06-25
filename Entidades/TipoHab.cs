using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TipoHab
    {
        public int IdTipoHab { get; set; }
        public string Nombre { get; set; }
        public decimal Tarifa { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
