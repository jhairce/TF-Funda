using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Huesped:Persona
    {
        public string NroTarjeta { get; set; }
        public string Telefono { get; set; }
        public override string ToString()
        {
            return base.Nombre;
        }
    }
}
