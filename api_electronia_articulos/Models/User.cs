using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_electronia_articulos.Models
{
    public class User
    {
        public decimal userID { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime fechaNan { get; set; }
        public string sexo { get; set; }
        public string telefonos { get; set; }
        public string status { get; set; }
        public Location location { get; set; }

    }

    public class Location
    {
        public int estadoID { get; set; }
        public string estado { get; set; }
    }
}