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
    public partial class DialogToppings : UserControl
    {
        public DialogToppings()
        {
            InitializeComponent();
        }
        public void CargarForm(Entities.Toppings toppings)
        {
            TxtNombreTopping.Text = toppings.name;
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
            TxtNombreTopping.BorderBrush = color;
            lblNombreError.Visibility = Visibility.Hidden;
        }




    }
}
