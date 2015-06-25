using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
namespace Negocio
{
    public class GestorHabitacion
    {
        IHabitacionDAO hadao = new HabitacionDAODB();

        public string RegistrarHabitacion(Habitacion o)
        {
            return hadao.Insertar(o);
        }
        public string ModificarHabitacion(Habitacion o)
        {
            return hadao.Modificar(o);
        }
        public string EliminarHabitacion(int id) 
        {
            return hadao.Eliminar(id); 
        }
        public List<Habitacion> ListarHabitaciones()
        {
            return hadao.ListarTodo();
        }
        public List<LineaCarac> Listarlineacarac(int idhabitacion)
        {
            Habitacion a = new Habitacion()
            {
                IdHabitacion=idhabitacion
            };
            return hadao.ListarLineaCaracHabitacion(a);
        }
    }
}
