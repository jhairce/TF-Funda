using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
namespace Datos
{
    public interface IHabitacionDAO:IDAO<Habitacion>
    {
        List<LineaCarac> ListarLineaCaracHabitacion(Habitacion o);
        List<Habitacion> ListarHabitacionTipoHab(TipoHab o);
    }
}
