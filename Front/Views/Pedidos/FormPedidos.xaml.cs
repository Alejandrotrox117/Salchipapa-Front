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

namespace Front.Views.Pedidos
{
    /// <summary>
    /// Lógica de interacción para FormPedidos.xaml
    /// </summary>
    public partial class FormPedidos : UserControl
    {
        public FormPedidos()
        {
            InitializeComponent();
        }

        public void CargarForm(Orders order)
        {
            ListPedidosActuales.ItemsSource = order.products;
            
        }
        public void LimpiarForm()
        {
            ListBoxToppings.Items.Clear();
            ListPedidosActuales.Items.Clear();
            CbCategoria.Text = "";
            CbProducto.Text = "";    
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
                        LblErrorPActual.Text = error.error;
                        LblErrorPActual.Visibility = Visibility.Visible;
                        CbCategoria.BorderBrush = color;
                        break;
                }
            }
        }
    }
}
