using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Habitacion
    {
        public int IdHabitacion { get; set; }
        public TipoHab tipohab { get; set; }
        public List<LineaCarac> lstlineacarac { get; set; }
        public string NumHabitacion { get; set; }
        public decimal Tarifa{get;set;}
        public override string ToString()
        {
            return NumHabitacion;
        }
    }
}