﻿using Nancy.Json;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Front.Utilities;

namespace Front
{
    /// <summary>
    /// Lógica de interacción para Session.xaml
    /// </summary>
    
    public partial class Session : Window
    {
        public Employe session { get; set; }
        public Session()
        {
            InitializeComponent();
        }

        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Dialog.IsOpen = true;
            string js = new JavaScriptSerializer().Serialize(new
            {
                ci = TxtUsuario.Text,
                password = TxtContrasena.Password
            });
            var httpResponse = await Request.Post("session", js);
            string result = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {
                this.session = Newtonsoft.Json.JsonConvert.DeserializeObject<Employe>(result);
                MostrarMensaje(null);
            }
            else
            {
                Error error = Newtonsoft.Json.JsonConvert.DeserializeObject<Error>(result);
                MostrarMensaje(error);
            }
        }
        private void MostrarMensaje(Error error)
        {
            var bc = new BrushConverter();
            Progress.Visibility = Visibility.Hidden;
            TxtSesion.Visibility = Visibility.Visible;
            BtnIniciarSesion.Visibility = Visibility.Visible;
            if (error is null)
            {
                Brush color = (Brush)bc.ConvertFrom("#00695c");
                TxtSesion.Text = "Has iniciado sesión";
                TxtSesion.Foreground = color;
                BtnIniciarSesion.Foreground = color;
                BtnIniciarSesion.Click+= IniciarSesion_Click;
            }
            else
            {
                Brush color = (Brush)bc.ConvertFrom("#f44c58");
                TxtSesion.Text = error.message;
                TxtSesion.Foreground = color;
                BtnIniciarSesion.Foreground = color;
                BtnIniciarSesion.Click += Cerrar_Click;
            }

        }

        private void Cerrar_Click(object sender,RoutedEventArgs e) 
        {
            Dialog.IsOpen = false;
            Progress.Visibility = Visibility.Visible;
            TxtSesion.Visibility = Visibility.Hidden;
            BtnIniciarSesion.Visibility = Visibility.Hidden;

        }
        private void IniciarSesion_Click(object sender,RoutedEventArgs e) 
        {
            Dialog.IsOpen = false;
            this.Close();
        }

        private void Dialog_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            BtnIniciarSesion.Click -= IniciarSesion_Click;
        }
    }
}
