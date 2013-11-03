using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_electronia_articulos.Models
{
    public class Picture
    {
        public int id { set; get; }
        public string item_id { set; get; }
        public int num_photo { set; get; }
        public string url_general { set; get; }
        public string path_general { set; get; }
    }
}