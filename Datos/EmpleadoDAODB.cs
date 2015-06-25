using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;
namespace Datos
{
    public class EmpleadoDAODB : IPersonaDAO
    {
        Database db = new Database();
        public string Insertar(Persona o)
        {
            try
            {
                Empleado a = o as Empleado;
                SqlConnection con = db.ConectaDb();
                string insert = string.Format("insert into Empleado(Nombre,Apellido,Email,Dni,Usuario,Clave,IdCargo) values('{0}','{1}','{2}','{3}','{4}','{5}',{6})", a.Nombre, a.Apellido, a.Email, a.Dni, a.Usuario, a.Clave, a.cargo.IdCargo);
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.ExecuteNonQuery();
                return "Empleado Registrado";
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
                Empleado a = o as Empleado;
                SqlConnection con = db.ConectaDb();
                string update = string.Format("update Empleado set Nombre='{0}',Apellido='{1}',Email='{2}',Dni='{3}',Usuario='{4}',Clave='{5}',IdCargo={6} where IdEmpleado={7}", a.Nombre, a.Apellido, a.Email, a.Dni, a.Usuario, a.Clave, a.cargo.IdCargo, a.IdPersona);
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.ExecuteNonQuery();
                return "Empleado Actualizado";
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
                string delete = string.Format("delete from Empeado where IdEmpleado={0}", id);
                SqlCommand cmd = new SqlCommand(delete, con);
                cmd.ExecuteNonQuery();
                return "Empleado Eliminado";
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
                Empleado empleado = null;
                SqlConnection con = db.ConectaDb();
                string select = string.Format("select em.IdEmpleado,em.Nombre,em.Apellido,em.Email,em.Dni,em.Usuario,em.Clave,ca.IdCargo from Cargo as ca,Empleado as em where ca.IdCargo=em.IdCargo and em.IdEmpleado={0}", id);
                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    empleado = new Empleado();
                    empleado.IdPersona = (int)reader["IdEmpleado"];
                    empleado.Nombre = (string)reader["Nombre"];
                    empleado.Apellido = (string)reader["Apellido"];
                    empleado.Email = (string)reader["Email"];
                    empleado.Dni = (string)reader["Dni"];
                    empleado.Usuario = (string)reader["Usuario"];
                    empleado.Clave = (string)reader["Clave"];
                    empleado.cargo = new CargoDAODB().BuscarPorId((int)reader["IdCargo"]);
                }
                reader.Close();
                return empleado as Persona;
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
                List<Persona> lstEmpleados = new List<Persona>();
                Empleado empleado = null;
                SqlConnection con = db.ConectaDb();
                SqlCommand cmd = new SqlCommand("select em.IdEmpleado,em.Nombre,em.Apellido,em.Email,em.Dni,em.Usuario,em.Clave,ca.IdCargo from Cargo as ca,Empleado as em where ca.IdCargo=em.IdCargo  ", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    empleado = new Empleado();
                    empleado.IdPersona = (int)reader["IdEmpleado"];
                    empleado.Nombre = (string)reader["Nombre"];
                    empleado.Apellido = (string)reader["Apellido"];
                    empleado.Email = (string)reader["Email"];
                    empleado.Dni = (string)reader["Dni"];
                    empleado.Usuario = (string)reader["Usuario"];
                    empleado.Clave = (string)reader["Clave"];
                    empleado.cargo = new CargoDAODB().BuscarPorId((int)reader["IdCargo"]);
                    lstEmpleados.Add(empleado);
                }
                reader.Close();
                return lstEmpleados;
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
            try
            {
                Empleado empleado = null;
                SqlConnection conn = db.ConectaDb();
                string select = string.Format("SELECT e.IdEmpleado,e.Nombre,e.Apellido,e.Email,e.Dni,c.IdCargo FROM Empleado as e,Cargo as c where e.IdCargo=c.IdCargo and Usuario='{0}' and Clave='{1}'", usuario, clave);
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    empleado = new Empleado();
                    empleado.IdPersona = (int)reader["IdEmpleado"];
                    empleado.Nombre = (string)reader["Nombre"];
                    empleado.Apellido = (string)reader["Apellido"];
                    empleado.Email = (string)reader["Email"];
                    empleado.Dni = (string)reader["Dni"];
                    empleado.cargo = new CargoDAODB().BuscarPorId((int)reader["IdCargo"]);
                }
                reader.Close();
                return empleado;
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
