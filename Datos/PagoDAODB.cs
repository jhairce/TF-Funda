using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;
namespace Datos
{
    public class PagoDAODB : IPagoDAO
    {
        Database db = new Database();
        SqlConnection conn = null;
        public string Insertar(Pago o)
        {
            conn = db.ConectaDb();
            try
            {
                string insertsqlPago = @"INSERT INTO Pago (IdReserva,SubTotal,IGV,Total) VALUES (@IdReserva,@SubTotal,@IGV,@Total) SELECT SCOPE_IDENTITY()";

                using (SqlCommand cmd = new SqlCommand(insertsqlPago, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", o.reserva.IdReserva);
                    cmd.Parameters.AddWithValue("@SubTotal", o.SubTotal);
                    cmd.Parameters.AddWithValue("@IGV", o.IGV);
                    cmd.Parameters.AddWithValue("@Total", o.Total);
                    o.IdPago = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string insertsqlLineaPago = @"INSERT INTO LineaPago (IdPago,IdServiciosA,Tarifa) 
                                            VALUES (@IdPago,@IdServiciosA, @Tarifa)
                                            SELECT SCOPE_IDENTITY()";

                using (SqlCommand cmd = new SqlCommand(insertsqlLineaPago, conn))
                {
                    foreach (LineaPago lineapago in o.lstlineapago)
                    {
                        cmd.Parameters.Clear();//solo en foreach se limpia
                        cmd.Parameters.AddWithValue("@IdPago", o.IdPago);
                        cmd.Parameters.AddWithValue("@IdServiciosA", lineapago.serviciosa.IdServiciosA);
                        cmd.Parameters.AddWithValue("@Tarifa", lineapago.Tarifa);
                        lineapago.IdLineaPago = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                return "Pago Registrado con el siguiente ID: " + o.IdPago;
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

        public string Modificar(Pago o)
        {
            conn = db.ConectaDb();
            try
            {
                conn = db.ConectaDb();
                string updatesqlHabitacion = string.Format("update Pago set IdReserva={0},SubTotal={1},IGV={2},Total={3} where IdPago={4}", o.reserva.IdReserva, o.SubTotal, o.IGV, o.Total, o.IdPago);
                SqlCommand cmd = new SqlCommand(updatesqlHabitacion, conn);
                cmd.ExecuteNonQuery();
                foreach (LineaPago lineapago in o.lstlineapago)
                {
                    cmd.Parameters.Clear();
                    string updatesqlLineaPago = string.Format("update Pago set IdPago={0},IdServiciosA={1} ,Tarifa={2} where IdLineaCarac={3}", o.IdPago, lineapago.serviciosa.IdServiciosA, lineapago.Tarifa, lineapago.IdLineaPago);
                    cmd.ExecuteNonQuery();
                }
                return "Pago Actualizado";
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
                string deletesqlPago = string.Format("delete from Pago where IdPago={0}", id);
                SqlCommand cmd = new SqlCommand(deletesqlPago, conn);
                cmd.ExecuteNonQuery();
                return "Pago Eliminado";
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

        public Pago BuscarPorId(int id)
        {
            try
            {
                Pago pago = null;
                SqlConnection con = db.ConectaDb();
                string select = string.Format("select p.IdPago,p.SubTotal,p.IGV,p.Total,R.IdReserva from Pago as p,Reserva as r where p.IdReserva=R.IdReserva and p.IdPago={0}", id);
                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    pago = new Pago();
                    pago.IdPago = (int)reader["IdPago"];
                    pago.SubTotal = (decimal)reader["SubTotal"];
                    pago.IGV = (decimal)reader["IGV"];
                    pago.Total = (decimal)reader["Total"];
                    pago.reserva = new ReservaDAODB().BuscarPorId((int)reader["IdReserva"]);
                }
                reader.Close();
                return pago;
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

        public List<Pago> ListarTodo()
        {
            try
            {
                List<Pago> lstPagos = new List<Pago>();
                Pago pago = null;
                SqlConnection conn = db.ConectaDb();
                SqlCommand cmd = new SqlCommand("select p.IdPago,p.SubTotal,p.IGV,p.Total,R.IdReserva from Pago as p,Reserva as r where p.IdReserva=R.IdReserva", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pago = new Pago();
                    pago.IdPago = (int)reader["IdPago"];
                    pago.SubTotal = (decimal)reader["SubTotal"];
                    pago.IGV = (decimal)reader["IGV"];
                    pago.Total = (decimal)reader["Total"];
                    pago.reserva = new ReservaDAODB().BuscarPorId((int)reader["IdReserva"]);
                    lstPagos.Add(pago);
                }
                reader.Close();
                return lstPagos;
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
