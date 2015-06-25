using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;
namespace Datos
{
    public class HabitacionDAODB : IHabitacionDAO
    {
        Database db = new Database();
        SqlConnection conn = null;
        public string Insertar(Habitacion o)
        {
            conn = db.ConectaDb();
            try
            {
                string insertsqlHabitacion = @"INSERT INTO Habitacion (IdTipoHab,NumHabitacion,Tarifa) VALUES (@IdTipoHab,@NumHabitacion,@Tarifa) SELECT SCOPE_IDENTITY()";

                using (SqlCommand cmd = new SqlCommand(insertsqlHabitacion, conn))
                {
                    cmd.Parameters.AddWithValue("@IdTipoHab", o.tipohab.IdTipoHab);
                    cmd.Parameters.AddWithValue("@NumHabitacion", o.NumHabitacion);
                    cmd.Parameters.AddWithValue("@Tarifa", o.Tarifa);
                    o.IdHabitacion = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string insertsqlLineaCarac = @"INSERT INTO LineaCarac (IdHabitacion,IdCarac,Importe) 
                                            VALUES (@IdHabitacion, @IdCarac,@Importe)
                                            SELECT SCOPE_IDENTITY()";

                using (SqlCommand cmd = new SqlCommand(insertsqlLineaCarac, conn))
                {
                    foreach (LineaCarac lineacarac in o.lstlineacarac)
                    {
                        cmd.Parameters.Clear();//solo en foreach se limpia
                        cmd.Parameters.AddWithValue("@IdHabitacion", o.IdHabitacion);
                        cmd.Parameters.AddWithValue("@IdCarac", lineacarac.carac.IdCarac);
                        cmd.Parameters.AddWithValue("@Importe", lineacarac.Importe);
                        lineacarac.IdLineaCarac = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                return "Habitacion Registrada con el siguiente ID: " + o.IdHabitacion;
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

        public string Modificar(Habitacion o)
        {
            conn = db.ConectaDb();
            try
            {
                conn = db.ConectaDb();
                string updatesqlHabitacion = string.Format("update Habitacion set IdTipoHab={0},NumHabitacion='{1}',Tarifa={2} where IdHabitacion={3}", o.tipohab.IdTipoHab, o.NumHabitacion, o.Tarifa, o.IdHabitacion);
                SqlCommand cmd = new SqlCommand(updatesqlHabitacion, conn);
                cmd.ExecuteNonQuery();
                foreach (LineaCarac lineacarac in o.lstlineacarac)
                {
                    cmd.Parameters.Clear();
                    string updatesqlLineaCarac = string.Format("update Habitacion set IdHabitacion={0},IdCarac={1},Importe={2} where IdLineaCarac={3}", o.IdHabitacion, lineacarac.carac.IdCarac, lineacarac.Importe, lineacarac.IdLineaCarac);
                    cmd.ExecuteNonQuery();
                }
                return "Habitacion Actualizada";
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
                conn = db.ConectaDb();
                string deletesqlHabitacion = string.Format("delete from Habitacion where IdHabitacion={0}", id);
                SqlCommand cmd = new SqlCommand(deletesqlHabitacion, conn);
                cmd.ExecuteNonQuery();
                return "Habitacion Eliminada";
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

        public Habitacion BuscarPorId(int id)
        {
            try
            {
                Habitacion habitacion = null;
                SqlConnection con = db.ConectaDb();
                string select = string.Format("select h.IdHabitacion,h.NumHabitacion,h.Tarifa,t.IdTipoHab from Habitacion as h,TipoHab as t where h.IdTipoHab=t.IdTipoHab and h.IdHabitacion={0}", id);
                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())//cuidado con el if y while aka y en listar todos
                {
                    habitacion = new Habitacion();
                    habitacion.IdHabitacion = (int)reader["IdHabitacion"];
                    habitacion.NumHabitacion = (string)reader["NumHabitacion"];
                    habitacion.Tarifa = (decimal)reader["Tarifa"];
                    habitacion.tipohab = new TipoHabDAODB().BuscarPorId((int)reader["IdTipoHab"]);
                }
                reader.Close();
                return habitacion;
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

        public List<Habitacion> ListarTodo()
        {
            try
            {
                List<Habitacion> lstHabitacions = new List<Habitacion>();
                Habitacion habitacion = null;
                SqlConnection conn = db.ConectaDb();
                SqlCommand cmd = new SqlCommand("select h.IdHabitacion,h.NumHabitacion,h.Tarifa,t.IdTipoHab from Habitacion as h,TipoHab as t where h.IdTipoHab=t.IdTipoHab", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    habitacion = new Habitacion();
                    habitacion.IdHabitacion = (int)reader["IdHabitacion"];
                    habitacion.NumHabitacion = (string)reader["NumHabitacion"];
                    habitacion.Tarifa = (decimal)reader["Tarifa"];
                    habitacion.tipohab = new TipoHabDAODB().BuscarPorId((int)reader["IdTipoHab"]);
                    lstHabitacions.Add(habitacion);
                }
                reader.Close();
                return lstHabitacions;
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

        public List<LineaCarac> ListarLineaCaracHabitacion(Habitacion o)
        {
            try
            {
                List<LineaCarac> lstlcarac = new List<LineaCarac>();
                LineaCarac lcarac = null;
                SqlConnection conn = db.ConectaDb();
                string select = string.Format("select c.IdCarac,c.Nombre,c.Precio from Habitacion as h, LineaCarac as l,Carac as c where h.IdHabitacion=l.IdHabitacion and c.IdCarac=l.IdCarac and h.IdHabitacion={0}", o.IdHabitacion);
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lcarac = new LineaCarac();
                    lcarac.carac = new CaracDAODB().BuscarPorId((int)reader["IdCarac"]);
                    lstlcarac.Add(lcarac);
                }
                reader.Close();
                return lstlcarac;
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

        public List<Habitacion> ListarHabitacionTipoHab(TipoHab o)
        {
            try
            {
                List<Habitacion> lshabitacions = new List<Habitacion>();
                Habitacion habitacion = null;
                SqlConnection conn = db.ConectaDb();
                string select = string.Format("select h.IdHabitacion,h.NumHabitacion,h.Tarifa from Habitacion h, TipoHab t where h.IdTipoHab=t.IdTipoHab and t.Nombre like '%{0}%'", o.Nombre);
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    habitacion = new Habitacion();
                    habitacion.IdHabitacion = (int)reader["IdHabitacion"];
                    habitacion.NumHabitacion = (string)reader["NumHabitacion"];
                    habitacion.Tarifa = (decimal)reader["Tarifa"];
                    lshabitacions.Add(habitacion);
                }
                reader.Close();
                return lshabitacions;
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
