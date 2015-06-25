using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;
namespace Datos
{
    public class HuespedDAODB : IPersonaDAO
    {
        Database db = new Database();
        public string Insertar(Persona o)
        {
            try
            {
                Huesped a = o as Huesped;
                SqlConnection con = db.ConectaDb();
                string insert = string.Format("insert into Huesped(Nombre,Apellido,Email,Telefono,Dni,NroTarjeta) values('{0}','{1}','{2}','{3}','{4}','{5}')", a.Nombre, a.Apellido, a.Email,a.Telefono, a.Dni, a.NroTarjeta);
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.ExecuteNonQuery();
                return "Huesped Registrado";
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

        public string Modificar(Persona o)
        {
            try
            {
                Huesped a = o as Huesped;
                SqlConnection con = db.ConectaDb();
                string update = string.Format("update Huesped set Nombre='{0}',Apellido='{1}',Email='{2}',Telefono='{3}',Dni='{4}',NroTarjeta='{5}' where IdHuesped={6}", a.Nombre, a.Apellido, a.Email,a.Telefono, a.Dni, a.NroTarjeta, a.IdPersona);
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.ExecuteNonQuery();
                return "Boleta Actualizada";
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
                string delete = string.Format("delete from Huesped where IdHuesped={0}", id);
                SqlCommand cmd = new SqlCommand(delete, con);
                cmd.ExecuteNonQuery();
                return "Huesped Eliminado";
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

        public Persona BuscarPorId(int id)
        {
            try
            {
                Huesped huesped = null;
                SqlConnection con = db.ConectaDb();
                string select = string.Format("select IdHuesped,Nombre,Apellido,Email,Telefono,Dni,NroTarjeta from Huesped where IdHuesped='{0}' order by IdHuesped asc", id);
                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    huesped = new Huesped();
                    huesped.IdPersona = (int)reader["IdHuesped"];
                    huesped.Nombre = (string)reader["Nombre"];
                    huesped.Apellido = (string)reader["Apellido"];
                    huesped.Email = (string)reader["Email"];
                    huesped.Telefono = (string)reader["Telefono"];
                    huesped.Dni = (string)reader["Dni"];
                    huesped.NroTarjeta = (string)reader["NroTarjeta"];
                }
                reader.Close();
                return huesped as Persona;
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

        public List<Persona> ListarTodo()
        {
            try
            {
                List<Persona> lstHuespeds = new List<Persona>();
                Huesped huesped = null;
                SqlConnection con = db.ConectaDb();
                SqlCommand cmd = new SqlCommand("select IdHuesped,Nombre,Apellido,Email,Telefono,Dni,NroTarjeta from Huesped ", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    huesped = new Huesped();
                    huesped.IdPersona = (int)reader["IdHuesped"];
                    huesped.Nombre = (string)reader["Nombre"];
                    huesped.Apellido = (string)reader["Apellido"];
                    huesped.Email = (string)reader["Email"];
                    huesped.Telefono = (string)reader["Telefono"];
                    huesped.Dni = (string)reader["Dni"];
                    huesped.NroTarjeta = (string)reader["NroTarjeta"];
                    lstHuespeds.Add(huesped);
                }
                reader.Close();
                return lstHuespeds;
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

        public Empleado verificarempleado(string usuario, string clave)
        {
            throw new NotImplementedException();
        }
    }
}
