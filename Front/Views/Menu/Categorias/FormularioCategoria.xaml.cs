using Entities;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Front.Views.Menu.Categorias
{
    /// <summary>
    /// Lógica de interacción para DialogHostCategorias.xaml
    /// </summary>
    public partial class FormularioCategoria : UserControl
    {
        public string FileImg { get; set; }
        public FormularioCategoria()
        {
            InitializeComponent();
        }
        public void CargarForm(Categorie categorie)
        {
            MessageBox.Show(categorie.updatedAt.ToString("DD-MM-YYYY-HH-mm-ss"));
            TxtNombreCategoria.Text = categorie.name;
            FileImg = categorie.img;
            img.ImageSource = new BitmapImage(new Uri(categorie.img));
            BtnImage.Background =  (Brush)new BrushConverter().ConvertFrom("#00695c");

        }
        public void LimpiarForm()
        {
            FileImg = "";
            TxtNombreCategoria.Text = "";
            img.ImageSource = null;
            BtnImage.Background = (Brush) new BrushConverter().ConvertFrom("#f44c58");
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
                        TxtNombreCategoria.BorderBrush = color;
                        break;
                }
            }
        }


        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            Brush color = (Brush) bc.ConvertFrom("#00695c");
            TxtNombreCategoria.BorderBrush = color;
            lblNombreError.Visibility = Visibility.Hidden;
        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files|*.jpg";
            file.FilterIndex = 1;
            if (file.ShowDialog()==true)
            {
                FileImg = file.FileName;
                img.ImageSource = new BitmapImage(new Uri(file.FileName));
                BtnImage.Background =  (Brush) new BrushConverter().ConvertFrom("#00695c");
            }
        }
    }
}
