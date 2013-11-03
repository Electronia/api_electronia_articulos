using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_electronia_articulos.Models
{
    interface IAccess
    {
        Access Get(string login, string password);
    }
}