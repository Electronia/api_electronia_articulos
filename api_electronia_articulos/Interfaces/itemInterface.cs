using api_electronia_articulos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace api_electronia_articulos.Interfaces
{
    interface itemInterface
    {
        Item Get(string id);
        Item Add(string userId, Item item);
        /*void Remove(string id);
        Item Update(string id, Item item);*/
    }
}
