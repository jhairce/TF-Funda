using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
namespace Negocio
{
    public class GestorTipoHab
    {
        ITipoHabDAO tidao = new TipoHabDAODB();

        public string RegistrarTipoHab(TipoHab o)
        {
            return tidao.Insertar(o);
        }
        public string ModificarTipoHab(TipoHab o)
        {
            return tidao.Modificar(o);
        }
        public string EliminarTipoHab(int id)
        {
            return tidao.Eliminar(id);
        }
        public List<TipoHab> ListarTipoHabs()
        {
            return tidao.ListarTodo();
        }
    }
}
