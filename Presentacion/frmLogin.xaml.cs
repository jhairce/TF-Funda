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
    public partial class frmLogin : Window
    {
        GestorPersona gp = new GestorPersona();
        Persona p = new Persona();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            p = gp.autenticarempleado(txtUsuario.Text, txtClave.Text);
            if (p != null)
            {
                frmPrincipal frmprincipal = new frmPrincipal();
                frmprincipal.p = p;
                frmprincipal.gp = gp;
                frmprincipal.Show();
            }
            else
                MessageBox.Show("Incorrecto");
    //        Empleado a = persona as Empleado;

      //      if ( a.cargo.IdCargo== 1)//aki no necesita saber  q cargo tiene sino en el formulario principal
       //         MessageBox.Show("AKI ABRES EL FORMULARIO PRINCIPAL");
            //if (persona != null)
            //{
            //    frmPrincipal frmprincipal= new frmPrincipal();
            //    frmprincipal.persona = persona;
            //    frmprincipal.Show();
            //    this.Hide();
            //}
            //else
            //    MessageBox.Show("Credenciales incorrectas!", "Pulpin!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
