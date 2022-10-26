using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities
{
    public class clientes
    {

        public clientes()
        {
            //Nombre = name;
            //Apellido = surname;
            //Direccion = address;
            //Cedula = ci;
            //Telefono = phone;

        }


        public string ci { get; set; }
        public string name { get; set; }

        public string surname { get; set; }
        public string address { get; set; }
        public string phone { get; set; }

        //public bool Error()
        //{
        //    if (ci is null || name is null || surname is null ||
        //        dir is null || telf is null)
        //    {
        //        return true  
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

    }
}





