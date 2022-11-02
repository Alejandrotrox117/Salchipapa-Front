﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
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
using Entities;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;

namespace Front.Views.Productos
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : UserControl
    {


        public Productos()
        {
            InitializeComponent();
            //List<categories> categories = new List<categories>();
            //categories.Add(new categories()
            //{
            //    name = "Batidos",
            //    products = new List<productos>()
            //    {
            //        new productos{  name ="Oreo", price = 2,stock=3,description="Batido de Oreo" },
            //        new productos{  name ="Fresa", price = 2,stock=3,description="Batido de Fresa" },
            //        new productos{  name ="Piña", price = 2,stock=3,description="Batido de Piña" }
            //    },

            //    toppings = new List<Toppings>() { new Toppings { name = "Flips", price = 2 } }

            //});

            //categories.Add(new categories()
            //{
            //    name = "Merengadas",
            //    products = new List<productos>() { new productos { name = "Limon", price = 2, stock = 3, description = "Batido de Oreo" } },
            //    toppings = new List<Toppings>() { new Toppings { name = "Flips", price = 2 } }

            //});
            

        }

        



        public async Task<string> GetHttp(string url)
        {
            WebRequest oRequest = WebRequest.Create(url);  
            WebResponse oResponse = oRequest.GetResponse();  
            StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            return await sr.ReadToEndAsync();
        }



        private async void Main()
        {
//            string response = await GetHttp("http://localhost:3000/api/products?extendeData=true");
  //          string responseTwo = await GetHttp("http://localhost:3000/api/categories");

            List<Grupos> Grupo = new List<Grupos>();
    //        List<categories> categories = JsonConvert.DeserializeObject<List<categories>>(responseTwo);
            //List<productos> productos = JsonConvert.DeserializeObject<List<productos>>(response);
            List<productos> productos = new List<productos>();
            productos.Add(new productos() { name = "Batido de Oreo",price=1,stock=2,description="Batido",categorie="Batidos", toppings = new List<Toppings>() { new Toppings { name = "Flips", price = 2 } }});
            productos.Add(new productos() { name = "Batido de Oreo",price=1,stock=2,description="Batido",categorie="Batidos", toppings = new List<Toppings>() { new Toppings { name = "Flips", price = 2 } }});


            if (productos  != null)
            {
             

            }
            else
            {
                MessageBox.Show("Error");
            }
        }


      

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            


        }

        private void TabControlItem_Initialized(object sender, EventArgs e)
        {

        }

        private void BtnEditarProducto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

      
        private void BtnAgregarCategoria_Click(object sender, RoutedEventArgs e)
        {

           
        }

        private void Border_Initialized(object sender, EventArgs e)
        {

            //Main();
            //FrameAgregarCategoria.NavigationService.Navigate(new AgregarCategoria());
            
        }

        private void ItemsProductos_Initialized(object sender, EventArgs e)
        {
           
        }
    }
}
