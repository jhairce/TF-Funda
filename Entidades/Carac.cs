﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Carac
    {
        public int IdCarac { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}