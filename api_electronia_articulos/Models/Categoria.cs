using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_electronia_articulos.Models
{
    public class Categoria
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int catPadreID { get; set; }
    }
}