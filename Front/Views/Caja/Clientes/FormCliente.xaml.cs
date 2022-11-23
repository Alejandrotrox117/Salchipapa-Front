using Entities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Front.Views.Caja.Clientes
{
    /// <summary>
    /// Lógica de interacción para DialogClientes.xaml
    /// </summary>
    public partial class FormCliente : UserControl
    {
        public FormCliente()
        {
            InitializeComponent();
        }

        public void CargarForm(Client client)
        {
            string[] ci = client.ci.Split('-');
            CBCedula.Text = ci[0] + '-';
            TxtCedula.Text = ci[1];
            TxtNombre.Text = client.name;
            TxtApellido.Text = client.surname;
            string[] phone = client.phone.Split('-');
            CBTelefono.Text = phone[0] + '-';
            TxtTelefono.Text = phone[1];
            TxtDireccion.Text = client.address;
        }

        public void LimpiarForm()
        {
            CBCedula.Text = "";
            TxtNombre.Text = "";
            TxtApellido.Text = "";
            TxtCedula.Text = "";
            TxtDireccion.Text = "";
            CBTelefono.Text = "";
            TxtTelefono.Text = "";
        }

        public void MostrarErrores(Error errores)
        {
            var bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFrom("#f44c58");

            foreach (ErrorsList error in errores.errors)
            {
                switch (error.property)
                {
                    case "name":
                        lblNombreError.Text = error.error;
                        lblNombreError.Visibility = Visibility.Visible;
                        TxtNombre.BorderBrush = color;
                        break;
                    case "surname":
                        lblApellidoError.Text = error.error;
                        lblApellidoError.Visibility = Visibility.Visible;
                        TxtApellido.BorderBrush = color;
                        break;
                    case "ci":
                        lblCedulaError.Text = error.error;
                        lblCedulaError.Visibility = Visibility.Visible;
                        TxtCedula.BorderBrush = color;
                        break;
                    case "phone":
                        lblTelefonoError.Text = error.error;
                        lblTelefonoError.Visibility = Visibility.Visible;
                        TxtTelefono.BorderBrush = color;
                        break;
                    case "address":
                        lblDireccionError.Text = error.error;
                        lblDireccionError.Visibility = Visibility.Visible;
                        TxtDireccion.BorderBrush = color;
                        break;
                }
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFrom("#00695c");

            TextBox txt = sender as TextBox;
            switch (txt.Name)
            {
                case "TxtNombre":
                    lblNombreError.Visibility = Visibility.Hidden;
                    TxtNombre.BorderBrush = color;
                    break;
                case "TxtApellido":
                    lblApellidoError.Visibility = Visibility.Hidden;
                    TxtApellido.BorderBrush = color;
                    break;
                case "TxtCedula":
                    lblCedulaError.Visibility = Visibility.Hidden;
                    TxtCedula.BorderBrush = color;
                    break;
                case "TxtTelefono":
                    lblTelefonoError.Visibility = Visibility.Hidden;
                    TxtTelefono.BorderBrush = color;
                    break;
                case "TxtDireccion":
                    lblDireccionError.Visibility = Visibility.Hidden;
                    TxtDireccion.BorderBrush = color;
                    break;

            }
        }
    }
}
