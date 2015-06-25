using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;
namespace Datos
{
    public class CargoDAODB : ICargoDAO
    {
        Database db = new Database();
        public string Insertar(Cargo o)
        {
            throw new NotImplementedException();
        }

        public string Modificar(Cargo o)
        {
            throw new NotImplementedException();
        }

        public string Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Cargo BuscarPorId(int id)
        {
            try
            {
                Cargo cargo = null;
                SqlConnection con = db.ConectaDb();
                string select = string.Format("select IdCargo,Nombre from Cargo where IdCargo='{0}' order by IdCargo asc", id);
                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cargo = new Cargo();
                    cargo.IdCargo = (int)reader["IdCargo"];
                    cargo.Nombre = (string)reader["Nombre"];
                }
                reader.Close();
                return cargo;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                db.DesconectaDb();
            }
        }

        public List<Cargo> ListarTodo()
        {
            throw new NotImplementedException();
        }
    }
}
