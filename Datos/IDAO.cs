using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public interface IDAO<T>
    {
        string Insertar(T o);
        string Modificar(T o);
        string Eliminar(int id);
        T BuscarPorId(int id);
        List<T> ListarTodo();
    }
}
