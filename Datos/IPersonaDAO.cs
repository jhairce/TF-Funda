﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
namespace Datos
{
    public interface IPersonaDAO:IDAO<Persona>
    {
        Empleado verificarempleado(string usuario, string clave);
    }
}
