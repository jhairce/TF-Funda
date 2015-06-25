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
using Negocio;
using Entidades;
namespace Presentacion
{
    public partial class frmRegistroHuesped : Window
    {
        public Huesped huespedseleccionado= new Huesped();
        public GestorPersona gp = new GestorPersona();
        public frmRegistroHuesped()
        {
            InitializeComponent();
        }
        private void Window_Activated_1(object sender, EventArgs e)
        {
            txtNombre.Text = huespedseleccionado.Nombre;
            txtApellidos.Text = huespedseleccionado.Apellido;
            txtDNI.Text = huespedseleccionado.Dni;
            txtEmail.Text = huespedseleccionado.Email;
            txtTelefono.Text = huespedseleccionado.Telefono;
            txtCredito.Text = huespedseleccionado.NroTarjeta;
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = "";
            txtApellidos.Text = "";
            txtDNI.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtCredito.Text = "";
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            Huesped o=new Huesped()
            {
                Nombre=txtNombre.Text,
                Apellido=txtApellidos.Text,
                Dni=txtDNI.Text,
                Email=txtEmail.Text,
                Telefono=txtTelefono.Text,
                NroTarjeta=txtCredito.Text
            };
            MessageBox.Show(gp.RegistrarHuesped(o));
        }
    }
}
