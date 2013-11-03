using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_electronia_articulos.Models
{
    
    interface IListaRepositorio
    {
        IEnumerable<Lista> GetAll(string type);
       // IEnumerable<Lista> GetAll();

    }
}