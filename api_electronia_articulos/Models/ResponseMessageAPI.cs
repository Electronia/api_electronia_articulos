using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace api_electronia_articulos.Models
{
    public class ResponseMessageAPI
    {
        public string status { get; set; }
        public string menssage { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }

}