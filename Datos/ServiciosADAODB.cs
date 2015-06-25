using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;
namespace Datos
{
    public class ServiciosADAODB : IServiciosADAO
    {
        Database db = new Database();
        public string Insertar(ServiciosA o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string insert = string.Format("insert into ServiciosA(Nombre,Precio) values('{0}',{1})", o.Nombre, o.Precio);
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.ExecuteNonQuery();
                return "Servicio Adicional Registrado";
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

        public string Modificar(ServiciosA o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string update = string.Format("update ServiciosA set Nombre='{0}',Precio={1} where IdServiciosA={2}", o.Nombre, o.Precio, o.IdServiciosA);
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.ExecuteNonQuery();
                return "Servicio Adicional Actualizado";
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
                string delete = string.Format("delete from ServiciosA where IdServiciosA={0}", id);
                SqlCommand cmd = new SqlCommand(delete, con);
                cmd.ExecuteNonQuery();
                return "Servicio Adicional Eliminado";
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

        public ServiciosA BuscarPorId(int id)
        {
            try
            {
                ServiciosA serviciosa = null;
                SqlConnection con = db.ConectaDb();
                string select = string.Format("select IdServiciosA,Nombre,Precio from ServiciosA where IdServiciosA='{0}' order by IdServiciosA asc", id);
                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    serviciosa = new ServiciosA();
                    serviciosa.IdServiciosA = (int)reader["IdServiciosA"];
                    serviciosa.Nombre = (string)reader["Nombre"];
                    serviciosa.Precio = (decimal)reader["Precio"];
                }
                reader.Close();
                return serviciosa;
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

        public List<ServiciosA> ListarTodo()
        {
            try
            {
                List<ServiciosA> lstserviciosas = new List<ServiciosA>();
                ServiciosA serviciosa = null;
                SqlConnection con = db.ConectaDb();
                SqlCommand cmd = new SqlCommand("select IdCarac,Nombre,Precio from ServiciosA ", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    serviciosa = new ServiciosA();
                    serviciosa.IdServiciosA = (int)reader["IdCarac"];
                    serviciosa.Nombre = (string)reader["Nombre"];
                    serviciosa.Precio = (decimal)reader["Precio"];
                    lstserviciosas.Add(serviciosa);
                }
                reader.Close();
                return lstserviciosas;
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
