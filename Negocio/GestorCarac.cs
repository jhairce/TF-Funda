using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
namespace Negocio
{
    public class GestorCarac
    {
        ICaracDAO cadao = new CaracDAODB();

        public string RegistrarCaracteristica(Carac o)
        {
            return cadao.Insertar(o);
        }
        public string ModificarHabitacion(Carac o)
        {
            return cadao.Modificar(o);
        }
        public string EliminarHabitacion(int id) 
        {
            return cadao.Eliminar(id); 
        }
        public List<Carac> ListarCaracteristicas()
        {
            return cadao.ListarTodo();
        }    
    }
}
