



using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities
{
    public class clientes
    {

        public clientes(string name, string surname, string dir, string ci, string telf)
        {
            Nombre = name;
            Apellido = surname;
            Direccion = dir;
            Cedula = ci;
            Telefono = telf;

        }


        public string Cedula { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }


        //public bool Error()
        //{
        //    if (ci is null || name is null || surname is null ||
        //        dir is null || telf is null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

    }
}





