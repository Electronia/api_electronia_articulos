using api_electronia_articulos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace api_electronia_articulos.Interfaces
{
    interface categoryInterface
    {
        IEnumerable<Category> Get();
        IEnumerable<Category> Get(string id);
        //Category Add(string userid);
    }
}
