using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;
namespace Datos
{
    public class CaracDAODB : ICaracDAO
    {
        Database db = new Database();
        public string Insertar(Carac o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string insert = string.Format("insert into Carac(Nombre,Precio) values('{0}',{1})", o.Nombre, o.Precio);
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.ExecuteNonQuery();
                return "Caracteristica de Hotel Registrada";
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

        public string Modificar(Carac o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string update = string.Format("update Carac set Nombre='{0}',Precio={1} where IdCarac={2}", o.Nombre, o.Precio, o.IdCarac);
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.ExecuteNonQuery();
                return "Caracteristica Actualizada";
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
                string delete = string.Format("delete from Carac where IdCarac={0}", id);
                SqlCommand cmd = new SqlCommand(delete, con);
                cmd.ExecuteNonQuery();
                return "Caracteristica Eliminada";
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

        public Carac BuscarPorId(int id)
        {
            try
            {
                Carac carac = null;
                SqlConnection con = db.ConectaDb();
                string select = string.Format("select IdCarac,Nombre,Precio from Carac where IdCarac='{0}' order by IdCarac asc", id);
                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    carac = new Carac();
                    carac.IdCarac = (int)reader["IdCarac"];
                    carac.Nombre = (string)reader["Nombre"];
                    carac.Precio = (decimal)reader["Precio"];
                }
                reader.Close();
                return carac;
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

        public List<Carac> ListarTodo()
        {
            try
            {
                List<Carac> lstcaracs = new List<Carac>();
                Carac carac = null;
                SqlConnection con = db.ConectaDb();
                SqlCommand cmd = new SqlCommand("select IdCarac,Nombre,Precio from Carac ", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    carac = new Carac();
                    carac.IdCarac = (int)reader["IdCarac"];
                    carac.Nombre = (string)reader["Nombre"];
                    carac.Precio = (decimal)reader["Precio"];
                    lstcaracs.Add(carac);
                }
                reader.Close();
                return lstcaracs;
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
