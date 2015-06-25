using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
namespace Negocio
{
    public class GestorReserva
    {
        IReservaDAO redao = new ReservaDAODB();

        public string RegistrarReserva(Reserva o)
        {
            return redao.Insertar(o);
        }
        public string ModificarReserva(Reserva o)
        {
            return redao.Modificar(o);
        }
        public string EliminarReserva(int id) 
        {
            return redao.Eliminar(id); 
        }
        public List<Reserva> ListarReservas()
        {
            return redao.ListarTodo();
        }
    }
}
