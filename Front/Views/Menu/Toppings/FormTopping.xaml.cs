using Entities;
using Front.Utilities;
using Microsoft.Win32;
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
        public string FileImg { get; set; }
        public FormTopping()
        {
            InitializeComponent();
        }
        public void CargarForm(Entities.Topping toppings)
        {
            TxtNombreTopping.Text = toppings.name;
            TxtPrecioTopping.Text = toppings.price.ToString();
            CheckboxTp.IsChecked = toppings.stock;
            FileImg = toppings.img;
            img.ImageSource = new BitmapImage(new Uri(toppings.img));
            BtnImage.Background = (Brush)new BrushConverter().ConvertFrom("#00695c");
        }
        public void LimpiarForm()
        {
            TxtNombreTopping.Text = "";
            TxtPrecioTopping.Text = "";
            CheckboxTp.IsChecked = false;
            img.ImageSource = null;
            BtnImage.Background = (Brush)new BrushConverter().ConvertFrom("#f44c58");
        }

        public void MostrarErrores(Error errores)
        {
            var bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFrom("#f44c58");

            if (errores.message != "img")
            {
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
                    }
                }
            }
            else
            {
                lblImgError.Visibility = Visibility.Visible;
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

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files|*.jpg";
            file.FilterIndex = 1;
            if (file.ShowDialog() == true)
            {
                FileImg = file.FileName;
                img.ImageSource = new BitmapImage(new Uri(file.FileName));
                BtnImage.Background = (Brush)new BrushConverter().ConvertFrom("#00695c");
                lblImgError.Visibility = Visibility.Hidden;
            }
        }
        private void TextBoxValidation_KeyDown(object sender, KeyEventArgs e)
        {

            Validations.TextBox_ValidateNum(sender, e);


        }

    }
}
