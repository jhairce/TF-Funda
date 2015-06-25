using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
namespace Datos
{
    public interface IReservaDAO:IDAO<Reserva>
    {
        List<LineaReserva> ListarLineaReservaReserva(Reserva o);
        List<Reserva> ListarReservaHuesped(Huesped o);
    }
}
