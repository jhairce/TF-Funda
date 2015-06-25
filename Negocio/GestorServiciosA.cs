using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
namespace Negocio
{
    public class GestorServiciosA
    {
        IServiciosADAO sedao = new ServiciosADAODB();
        
        public string RegistrarServiciosA(ServiciosA o)
        {
            return sedao.Insertar(o);
        }
        public string ModificarServiciosA(ServiciosA o)
        {
            return sedao.Modificar(o);
        }
        public string EliminarServiciosA(int id) 
        { 
            return sedao.Eliminar(id); 
        }
        public List<ServiciosA> ListarServiciosAs()
        {
            return sedao.ListarTodo();
        }
    }
}
