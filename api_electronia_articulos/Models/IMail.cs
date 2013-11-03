using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace api_electronia_articulos.Models
{
   
    interface IMail
    {
        HttpResponseMessage Add(Mail email);
        string Get(string  data);

    }
}