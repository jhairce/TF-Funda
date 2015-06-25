using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
namespace Negocio
{
    public class GestorPersona
    {
        IPersonaDAO pedao1 = new HuespedDAODB(), pedao2 = new EmpleadoDAODB();

        #region Huesped
        public string RegistrarHuesped(Huesped o)
        {
            return pedao1.Insertar(o);
        }
        public string ModificarHuesped(Huesped o)
        {
            return pedao1.Modificar(o);
        }
        public string EliminarHuesped(int id) 
        {
            return pedao1.Eliminar(id);
        }
        public List<Persona> ListarHuespeds()
        {
            return pedao1.ListarTodo();
        }
        #endregion

        #region Empleado
        public string RegistrarEmpleado(Empleado o)
        {
            return pedao2.Insertar(o);
        }
        public string ModificarEmpleado(Empleado o)
        {
            return pedao2.Modificar(o);
        }
        public string EliminarEmpleado(int id) 
        {
            return pedao2.Eliminar(id);
        }
        public List<Persona> ListarEmpleados()
        {
            return pedao2.ListarTodo();
        }
        public Empleado autenticarempleado(string usuario, string clave)
        {
            Empleado empleado = pedao2.verificarempleado(usuario, clave);
            return empleado;
        }
        #endregion
    }
}
