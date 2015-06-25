using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;
namespace Datos
{
    public class ReservaDAODB : IReservaDAO
    {
        Database db = new Database();
        SqlConnection conn = null;
        public string Insertar(Reserva o)
        {
            conn = db.ConectaDb();
            try
            {
                string insertsqlReserva = @"INSERT INTO Reserva (IdHuesped,FechaIng,FechaSal,Total) VALUES (@IdHuesped,@FechaIng,@FechaSal,@Total) SELECT SCOPE_IDENTITY()";

                using (SqlCommand cmd = new SqlCommand(insertsqlReserva, conn))
                {
                    cmd.Parameters.AddWithValue("@IdHuesped", o.huesped.IdPersona);
                    cmd.Parameters.AddWithValue("@FechaIng", o.FechaIng);
                    cmd.Parameters.AddWithValue("@FechaSal", o.FechaSal);
                    cmd.Parameters.AddWithValue("@Total", o.Total);
                    o.IdReserva = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string insertsqlLineaReserva = @"INSERT INTO LineaReserva (IdReserva,IdEmpleado,IdHabitacion) 
                                            VALUES (@IdReserva, @IdEmpleado,@IdHabitacion)
                                            SELECT SCOPE_IDENTITY()";

                using (SqlCommand cmd = new SqlCommand(insertsqlLineaReserva, conn))
                {

                    foreach (LineaReserva lineareserva in o.lineareserva)
                    {
                        cmd.Parameters.Clear();//solo en foreach se limpia
                        cmd.Parameters.AddWithValue("@IdReserva", o.IdReserva);
                        cmd.Parameters.AddWithValue("@IdEmpleado", lineareserva.empleado.IdPersona);
                        cmd.Parameters.AddWithValue("@IdHabitacion", lineareserva.habitacion.IdHabitacion);
                        lineareserva.IdLineaReserva = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                return "Reserva Registrada con el siguiente ID: " + o.IdReserva;
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }
            finally
            {
                db.DesconectaDb();
            }
        }

        public string Modificar(Reserva o)
        {
            try
            {//si es error es x el executescalar
                conn = db.ConectaDb();
                string updatesqlReserva = string.Format("update Reserva set IdHuesped={0},FechaIng={1},FechaSal={2},Total={3} where IdReserva={4}", o.huesped.IdPersona, o.FechaIng, o.FechaSal, o.Total, o.IdReserva);
                SqlCommand cmd = new SqlCommand(updatesqlReserva, conn);
                cmd.ExecuteNonQuery();
                foreach (LineaReserva lineareserva in o.lineareserva)
                {
                    cmd.Parameters.Clear();
                    string updatesqlLineaCarac = string.Format("update Reserva set IdReserva={0},IdEmpleado={1},IdHabitacion={2} where IdLineaReserva={3}", o.IdReserva, lineareserva.empleado.IdPersona, lineareserva.habitacion.IdHabitacion, lineareserva.IdLineaReserva);
                    cmd.ExecuteNonQuery();
                }
                return "Reserva Actualizada Actualizada";
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
            try//SI FUNCIONA, PERO DATE CUENTA K SOLO SE ELIMINA UNA FILA DE LA TABLA HABITACION SN EMBARGO LAS LINEASCARAC SIGUEN VIVAS SI MUESTRA PROBLEMA ELIMINAR AKI TMB! Y ANTES DE DELETE HABITACION XQ PERDERIAMOS EL IGUAL DE ID HABIACION CON IDLSTCARAC
            {
                conn = db.ConectaDb();
                string deletesqlReserva = string.Format("delete from Reserva where IdReserva={0}", id);
                SqlCommand cmd = new SqlCommand(deletesqlReserva, conn);
                cmd.ExecuteNonQuery();
                return "Reserva Eliminada";
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

        public Reserva BuscarPorId(int id)
        {
            try
            {
                Reserva reserva = null;
                SqlConnection con = db.ConectaDb();
                string select = string.Format("select r.IdReserva,r.FechaIng,r.FechaSal,r.Total,h.IdHuesped from Reserva as r,Huesped as h where r.IdHuesped=h.IdHuesped and r.IdReserva={0}", id);
                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())//cuidado con el if y while aka y en listar todos
                {
                    reserva = new Reserva(); Persona a = new Persona();
                    reserva.IdReserva = (int)reader["IdReserva"];
                    reserva.FechaIng = (DateTime)reader["FechaIng"];
                    reserva.FechaSal = (DateTime)reader["FechaSal"];
                    reserva.Total = (decimal)reader["Total"];
                    a = new HuespedDAODB().BuscarPorId((int)reader["IdHuesped"]);//CONVERSION DIOS
                    reserva.huesped = a as Huesped;
                }
                reader.Close();
                return reserva;
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
        public List<Reserva> ListarTodo()
        {
            try
            {
                List<Reserva> lstReservas = new List<Reserva>();
                Reserva reserva = null;
                SqlConnection conn = db.ConectaDb();
                SqlCommand cmd = new SqlCommand("select r.IdReserva,r.FechaIng,r.FechaSal,r.Total,h.IdHuesped from Reserva as r,Huesped as h where r.IdHuesped=h.IdHueped", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reserva = new Reserva(); Persona a = new Persona();
                    reserva.IdReserva = (int)reader["IdReserva"];
                    reserva.FechaIng = (DateTime)reader["FechaIng"];
                    reserva.FechaSal = (DateTime)reader["FechaSal"];
                    reserva.Total = (decimal)reader["Total"];
                    a = new HuespedDAODB().BuscarPorId((int)reader["IdHuesped"]);//CONVERSION DIOS
                    reserva.huesped = a as Huesped;
                    lstReservas.Add(reserva);
                }
                reader.Close();
                return lstReservas;
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
        public List<LineaReserva> ListarLineaReservaReserva(Reserva o)
        {
            try
            {
                List<LineaReserva> lstreserva = new List<LineaReserva>();
                LineaReserva lreserva = null;
                SqlConnection conn = db.ConectaDb();
                string select = string.Format("select e.IdEmpleado,e.Nombre,e.Apellido,h.IdHabitacion,h.NumHabitacion,h.Tarifa,l.Cantidad from Reserva as r, LineaReserva as l,Empleado as e,Habitacion as h where r.IdReserva=l.IdReserva and h.IdHabitacion=l.IdHabitacion and e.IdEmpleado=l.IdEmpleado and r.IdReserva={0}", o.IdReserva);
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lreserva = new LineaReserva();
                    Persona a = new Persona();
                    a = new EmpleadoDAODB().BuscarPorId((int)reader["IdEmpleado"]);
                    lreserva.empleado = a as Empleado;
                    lreserva.habitacion = new HabitacionDAODB().BuscarPorId((int)reader["IdHabitacion"]);
                    //     lcarac.carac.Precio = new CaracDAODB().BuscarPorId((int)reader["IdCarac"]).Precio;  SI DESEAS EL PRECIO DE CADA CARACTERISTICA
                    lstreserva.Add(lreserva);
                }
                reader.Close();
                return lstreserva;
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
        public List<Reserva> ListarReservaHuesped(Huesped o)
        {
            try
            {
                List<Reserva> lstreservas = new List<Reserva>();
                Reserva reserva = null;
                SqlConnection conn = db.ConectaDb();
                string select = string.Format("select r.IdReserva,r.FechaIng,r.FechaSal,r.Total, from Reserva as r, Huesped as h where r.IdHuesped=h.IdHuesped and h.Nombre like '%{0}%'", o.Nombre);
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reserva = new Reserva();
                    reserva.IdReserva = (int)reader["IdReserva"];
                    reserva.FechaIng = (DateTime)reader["FechaIng"];
                    reserva.FechaSal = (DateTime)reader["FechaSal"];
                    reserva.Total = (decimal)reader["Total"];
                    lstreservas.Add(reserva);
                }
                reader.Close();
                return lstreservas;
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
