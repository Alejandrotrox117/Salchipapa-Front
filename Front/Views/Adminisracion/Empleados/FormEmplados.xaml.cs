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

namespace Front.Views.Adminisracion.Empleados
{
    /// <summary>
    /// Lógica de interacción para DialogEmpleados.xaml
    /// </summary>
    public partial class FormEmpleados : UserControl
    {
        public FormEmpleados()
        {
            InitializeComponent();
        }

        public void CargarForm(employes employe)
        {
            string[] ci = employe.ci.Split('-');
            CBCedula.Text = ci[0] + '-';
            TxtCedula.Text = ci[1];
            TxtNombre.Text = employe.name;
            TxtApellido.Text = employe.surname;
            TxtContra.Text = employe.account.password;
            CbAccount.Text = employe.account.rol;
            CheckBoxPrivilegio.IsChecked = employe.account.admin;
        }

        public void LimpiarForm()
        {
            CBCedula.Text = "";
            TxtCedula.Text = "";
            TxtNombre.Text = "";
            TxtApellido.Text = "";
            TxtContra.Text = "";
            CbAccount.Text = "";
            CheckBoxPrivilegio.IsChecked = false;
        }

        public void MostrarErrores(Errors errores)
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
                    case "rol":
                        lblAccountError.Text = error.error;
                        lblAccountError.Visibility = Visibility.Visible;
                        CbAccount.BorderBrush = color;
                        break;
                    case "password":
                        lblContraError.Text = error.error;
                        lblContraError.Visibility = Visibility.Visible;
                        TxtContra.BorderBrush = color;
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
                case "TxtContra":
                    lblContraError.Visibility = Visibility.Hidden;
                    TxtContra.BorderBrush = color;
                    break;

            }
        }

        private void CbAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFrom("#00695c");

            lblAccountError.Visibility = Visibility.Hidden;
            CbAccount.BorderBrush = color;
        }
    }
}
