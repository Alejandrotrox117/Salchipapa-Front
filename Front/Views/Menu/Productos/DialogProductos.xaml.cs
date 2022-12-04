using Entities;
using Front.Utilities;
using Microsoft.Win32;
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

namespace Front.Views.Menu.Productos
{
    /// <summary>
    /// Lógica de interacción para DialogProductos.xaml
    /// </summary>
    public partial class DialogProductos : UserControl
    {
        public string FileImg { get; set; }
        public DialogProductos()
        {
            InitializeComponent();
            CargarCombobox();
        }

        public void CargarForm(product products)
        {
            TxtNombreProducto.Text = products.name;
            TxtPrecioProducto.Text = products.price.ToString();
            TxtStockProducto.Text = products.stock.ToString();
            TxtDescripcionProducto.Text = products.description;
            CbCategoriaProducto.SelectedValue = products.categorie._id;
            FileImg = products.img;
            img.ImageSource = new BitmapImage(new Uri(products.img));
            BtnImage.Background = (Brush)new BrushConverter().ConvertFrom("#00695c");
            foreach (Entities.Topping topping in products.toppings)
            {
                ListToppings.Items.Add(topping);
            }
        }

        public async void CargarCombobox()
        {
            string ResponseCategorie = await Request.Get("categories");
            string ResponseToppings = await Request.Get("toppings");
            List<Categorie> categorie = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Categorie>>(ResponseCategorie);
            List<Entities.Topping> toppings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Entities.Topping>>(ResponseToppings);
            if (categorie != null)
            {
                CbCategoriaProducto.ItemsSource = categorie;
                CBToppings.ItemsSource = toppings;
            }
            else
            {
                MessageBox.Show("No hay datos");
            }
        }

        public void LimpiarForm()
        {
            CBToppings.SelectedIndex = -1;
            CbCategoriaProducto.SelectedIndex = -1;
            TxtNombreProducto.Text = "";
            TxtPrecioProducto.Text = "";
            TxtStockProducto.Text = "";
            TxtDescripcionProducto.Text = "";
            ListToppings.Items.Clear();
        
            img.ImageSource = null;
            BtnImage.Background = (Brush)new BrushConverter().ConvertFrom("#f44c58");
        }

        public void MostrarErrores(Error errores)
        {
            var bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFrom("#f44c58");

            if(errores.message != "img")
            {
                foreach (ErrorsList error in errores.errors)
                {
                    switch (error.property)
                    {
                        case "name":
                            lblNombreError.Text = error.error;
                            lblNombreError.Visibility = Visibility.Visible;
                            TxtNombreProducto.BorderBrush = color;
                            break;
                        case "price":
                            lblPrecioError.Text = error.error;
                            lblPrecioError.Visibility = Visibility.Visible;
                            TxtPrecioProducto.BorderBrush = color;
                            break;
                        case "stock":
                            lblStockError.Text = error.error;
                            lblStockError.Visibility = Visibility.Visible;
                            TxtStockProducto.BorderBrush = color;
                            break;
                        case "categorie":
                            lblCategoriaError.Text = error.error;
                            lblCategoriaError.Visibility = Visibility.Visible;
                            CbCategoriaProducto.BorderBrush = color;
                            break;
                        case "toppings":
                            lblToppingError.Text = error.error;
                            lblToppingError.Visibility = Visibility.Visible;
                            CBToppings.BorderBrush = color;
                            break;
                        case "description":
                            lblDescripError.Text = error.error;
                            lblDescripError.Visibility = Visibility.Visible;
                            TxtDescripcionProducto.BorderBrush = color;
                            break;
                    }
                }
            }
            else
            {
                lblFotoError.Visibility = Visibility.Visible;
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            var bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFrom("#00695c");
            switch (textBox.Name)
            {
                case "TxtNombreProducto":
                    TxtNombreProducto.BorderBrush = color;
                    lblNombreError.Visibility = Visibility.Hidden;
                    break;
                case "TxtPrecioProducto":
                    TxtPrecioProducto.BorderBrush = color;
                    lblPrecioError.Visibility = Visibility.Hidden;
                    break;
                case "TxtStockProducto":
                    TxtStockProducto.BorderBrush = color;
                    lblStockError.Visibility = Visibility.Hidden;
                    break;
                case "TxtDescripcionProducto":
                    TxtDescripcionProducto.BorderBrush = color;
                    lblDescripError.Visibility = Visibility.Hidden;
                    break;
            }
        }
        private void CbCategoriaProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            var bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFrom("#00695c");
            switch (combo.Name)
            {
                case "TxtCategoriaProducto":
                    CbCategoriaProducto.BorderBrush = color;
                    lblCategoriaError.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void BtnAgregarToppingList_Click(object sender, RoutedEventArgs e)
        {
            ListToppings.Items.Add(CBToppings.SelectedItem);
        }

        private void BtnEliminarToppingList_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;

            ListToppings.Items.Remove(element.DataContext as Entities.Topping);

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
                lblFotoError.Visibility = Visibility.Hidden;
            }
        }
        private void TextBoxValidation_KeyDown(object sender, KeyEventArgs e)
        {
            Validations.TextBox_ValidateNum(sender, e);
        }
    }
}
