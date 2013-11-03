using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_electronia_articulos.Models
{
    public class Item
    {
        public string itemId { get; set; }
        public string title { get; set; }
        public string categoryId { get; set; }
        //public int userId { get; set; } // podemos pedirlo dentro del un parametro de authenticacion
        public int state { get; set; }
        public string stateName { get; set; }
        public decimal price { get; set; }
        public string typeCurrency { get; set; }
        public string status { get; set; }
        public string typeItem { get; set; }
        public DateTime dateRegistration { get; set; }
        public DateTime dateExpired { get; set; }
        public DateTime dateActivation { get; set; }
        public DateTime dateRenovation { get; set; }
        public string description { get; set; }
        public string usage { get; set; }
        public IEnumerable<Picture> pictures { get; set; }

    }
}