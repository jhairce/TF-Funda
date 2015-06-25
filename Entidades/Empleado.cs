using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Empleado:Persona
    {
        public Cargo cargo { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public override string ToString()
        {
            return base.Nombre;
        }
    }
}
