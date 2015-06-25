using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
namespace Negocio
{
    public class GestorPago
    {
        IPagoDAO padao=new PagoDAODB();

        public string RegistrarPago(Pago o)
        {
            return padao.Insertar(o);
        }
        public string ModificarPago(Pago o)
        {
            return padao.Modificar(o);
        }
        public string EliminarPago(int id) 
        {
            return padao.Eliminar(id);
        }
        public List<Pago> ListarPagos()
        {
            return padao.ListarTodo();
        }
    }
}
