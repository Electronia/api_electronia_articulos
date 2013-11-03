using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace api_electronia_articulos.Models
{
    interface IMenuRepository
    {
        IEnumerable<Menu> GetAll(string type, string clasificacion, string aparato, string marca, string modelo, string estado, string tienda);
        IEnumerable<Menu> GetAll();
        
    }
}
