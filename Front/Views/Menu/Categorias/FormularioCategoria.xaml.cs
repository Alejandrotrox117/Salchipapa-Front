using Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Front.Views.Menu.Categorias
{
    /// <summary>
    /// Lógica de interacción para DialogHostCategorias.xaml
    /// </summary>
    public partial class FormularioCategoria : UserControl
    {

        public FormularioCategoria()
        {
            InitializeComponent();
        }
        public void CargarForm(categories categorie)
        {
            TxtNombreCategoria.Text = categorie.name;
        }
        public void LimpiarForm()
        {
            TxtNombreCategoria.Text = "";
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
                        TxtNombreCategoria.BorderBrush = color;
                        break;
                }
            }
        }


        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFrom("#00695c");
            TxtNombreCategoria.BorderBrush = color;
            lblNombreError.Visibility = Visibility.Hidden;
        }


    }
}
