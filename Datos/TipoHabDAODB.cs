using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;
namespace Datos
{
    public class TipoHabDAODB : ITipoHabDAO
    {
        Database db = new Database();
        public string Insertar(TipoHab o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string insert = string.Format("insert into TipoHab(Nombre,Tarifa) values('{0}',{1})", o.Nombre, o.Tarifa);
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.ExecuteNonQuery();
                return "Tipo de Habitacion Registrado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                db.DesconectaDb();
            }
        }

        public string Modificar(TipoHab o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string update = string.Format("update TipoHab set Nombre='{0}',Tarifa={1} where IdTipoHab={2}", o.Nombre, o.Tarifa, o.IdTipoHab);
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.ExecuteNonQuery();
                return "Tipo de habitacion Actualizado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                db.DesconectaDb();
            }
        }

        public string Eliminar(int id)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string delete = string.Format("delete from TipoHab where IdTipoHab={0}", id);
                SqlCommand cmd = new SqlCommand(delete, con);
                cmd.ExecuteNonQuery();
                return "TipoHab Eliminado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                db.DesconectaDb();
            }
        }

        public TipoHab BuscarPorId(int id)
        {
            try
            {
                TipoHab tipohab = null;
                SqlConnection con = db.ConectaDb();
                string select = string.Format("Select IdTipoHab,Nombre,Tarifa from TipoHab where IdTipoHab={0}, order by IdTipoHab asc", id);
                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    tipohab = new TipoHab();
                    tipohab.IdTipoHab = (int)reader["IdTipoHab"];
                    tipohab.Nombre = (string)reader["Nombre"];
                    tipohab.Tarifa = (decimal)reader["Tarifa"];
                }
                reader.Close();
                return tipohab;
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

        public List<TipoHab> ListarTodo()
        {
            try
            {
                List<TipoHab> lstTipoHabs = new List<TipoHab>();
                TipoHab tipohab = null;
                SqlConnection con = db.ConectaDb();
                SqlCommand cmd = new SqlCommand("Select IdTipoHab,Nombre,Tarifa from TipoHab ", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tipohab = new TipoHab();
                    tipohab.IdTipoHab = (int)reader["IdTipoHab"];
                    tipohab.Nombre = (string)reader["Nombre"];
                    tipohab.Tarifa = (decimal)reader["Tarifa"];
                    lstTipoHabs.Add(tipohab);
                }
                reader.Close();
                return lstTipoHabs;
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
    }
}
