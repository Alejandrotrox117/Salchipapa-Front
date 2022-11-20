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

namespace Front.Views.Caja.Pagos
{
    /// <summary>
    /// Lógica de interacción para DialogPagos.xaml
    /// </summary>
    public partial class FormPagos : UserControl
    {
        public FormPagos()
        {
            InitializeComponent();
        }
        
        public void CargarForm(payments pago)
        {
            TxtNombrePago.Text = pago.name;
            CBMoneda.Text = pago.money;
        }
        public void LimpiarForm()
        {
            CBMoneda.Text = "";
            TxtNombrePago.Text = "";;
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
                        TxtNombrePago.BorderBrush = color;
                        break;
                    case "money":
                        lblMonedaError.Text = error.error;
                        lblMonedaError.Visibility = Visibility.Visible;
                        CBMoneda.BorderBrush = color;
                        break;
                }
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFrom("#00695c");

            lblNombreError.Visibility = Visibility.Hidden;
            TxtNombrePago.BorderBrush = color;
        }

        private void CBMoneda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFrom("#00695c");

            lblMonedaError.Visibility = Visibility.Hidden;
            CBMoneda.BorderBrush = color;
        }
    }
}
