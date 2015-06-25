using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Entidades;
using Negocio;
namespace Presentacion
{
    public partial class frmPrincipal : Window
    {
        public Persona p = new Persona();
        public GestorPersona gp = new GestorPersona();
        public GestorHabitacion gh = new GestorHabitacion();
        public GestorReserva gr = new GestorReserva();
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void Window_Activated_1(object sender, EventArgs e)
        {
            lblBienvenido.Content = p.Nombre + " " + p.Apellido;
            lblHora.Content = DateTime.Now;
            if ((p as Empleado).cargo.IdCargo == 1)//1Administrador
            {
     //           grpAdministracion.IsVisible = true;
                //actualizar1();
            }
            if ((p as Empleado).cargo.IdCargo == 2)//2Agente
            {
                //           grpAdministracion.IsVisible = true;
                actualizar2();

            }
            if ((p as Empleado).cargo.IdCargo == 3)//3Recepcionista
            {
                //           grpAdministracion.IsVisible = true;
                //actualizar3();
            }
        }

        #region Administrador

        private void btnHabitaciones_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCaracHabitaciones_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnServiciosAdicionales_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Agente
        private void btnAsignarServ_Click(object sender, RoutedEventArgs e)
        {
            if (dgServiciosA.SelectedItems.Count >= 0)
            {

            }
            else
                MessageBox.Show("selecciona un servicio Adicional");
        }
        private void actualizar2()
        {
            Huesped a=cbHuesped.SelectedItem as Huesped;
            foreach (Reserva b in gr.ListarReservas())
            {
                if (a.IdPersona == b.huesped.IdPersona)
                {
                    dgHabitacionesAsignadas.ItemsSource = b.lineareserva;
                }
            }
        }
        private void dgHabitacionesAsignadas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            decimal Total = 0;
            foreach (ServiciosA a in dgServiciosAdicionales.ItemsSource)
            {
                Total += a.Precio;
            }
            lblTotal.Content = Total;
        }
        #endregion

        #region Recepcionista
        private void btnRegistrarHuesped_Click(object sender, RoutedEventArgs e)
        {
            frmRegistroHuesped fh = new frmRegistroHuesped();
            fh.gp = gp;
            fh.Show();
        }

        private void btnModificarHuesped_Click(object sender, RoutedEventArgs e)
        {
            if (dgHuespedes.SelectedItems.Count == 1)
            {
                frmRegistroHuesped fh = new frmRegistroHuesped();
                fh.huespedseleccionado = dgHuespedes.SelectedItem as Huesped;
                fh.Show();
            }
            else
                MessageBox.Show("Debe seleccionar solo 1 huesped");
        }

        private void btnEliminarHuesped_Click(object sender, RoutedEventArgs e)
        {
            if (dgHuespedes.SelectedItems.Count == 1)
            {
                Huesped huespedseleccionado= dgHuespedes.SelectedItem as Huesped;
                gp.EliminarHuesped((huespedseleccionado).IdPersona);
            }
            else
                MessageBox.Show("Debe seleccionar solo 1 huesped");

        }

        private void btnRegistrarReserva_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModificarReserva_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelarReserva_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion 

        

    }
}
