using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Front.Views.Menu.Categorias
{
    /// <summary>
    /// Lógica de interacción para Categorias.xaml
    /// </summary>
    public partial class Categorias : UserControl
    {
        public ObservableCollection<categories> categories;
        public string titulo { get; set; }
        public Categorias()
        {
            InitializeComponent();
        }
        public async void GetCategorias()
        {
            string RespondCategorie = await Request.Get("categories");
            List<categories> categorie = Newtonsoft.Json.JsonConvert.DeserializeObject<List<categories>>(RespondCategorie);
            categories = new ObservableCollection<categories>(categorie);

            if (categorie != null)
            {
                itemsCardsCategorias.ItemsSource = categories;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
    }
}
