using Entities;
using Nancy.Json;
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

namespace Front.Views.Menu.Toppings
{
    /// <summary>
    /// Lógica de interacción para DialogToppings.xaml
    /// </summary>
    public partial class FormTopping : UserControl
    {
        public FormTopping()
        {
            InitializeComponent();
        }
        public void CargarForm(toppings toppings)
        {
            TxtNombreTopping.Text = toppings.name;
            TxtPrecioTopping.Text = toppings.price.ToString();
            CheckboxTp.IsChecked = toppings.stock;
        }
        public void LimpiarForm()
        {
            TxtNombreTopping.Text = "";
            TxtPrecioTopping.Text = "";
            CheckboxTp.IsChecked = false;
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
                        TxtNombreTopping.BorderBrush = color;
                        break;
                    case "price":
                        lblPrecioError.Text = error.error;
                        lblPrecioError.Visibility = Visibility.Visible;
                        TxtPrecioTopping.BorderBrush =color;
                        break;
                    case "stock":
                        lblCheckboxError.Text = error.error;
                        lblCheckboxError.Visibility = Visibility.Visible;
                        CheckboxTp.BorderBrush = color;
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
                    TxtNombreTopping.BorderBrush = color;
                    break;
                case "TxtPrecio":
                    lblPrecioError.Visibility = Visibility.Hidden;
                    TxtPrecioTopping.BorderBrush = color;
                    break;

            }
        }
    }
}
