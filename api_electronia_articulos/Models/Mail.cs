using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_electronia_articulos.Models
{
    public class Mail
    {
        public string emailDestino { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string idService { get; set; }
    }
}