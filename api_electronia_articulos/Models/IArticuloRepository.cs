using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

namespace ProductStore.Models
{
    interface IArticuloRepository
    {
        IEnumerable<Articulo> GetAll(string key, int type);
        IEnumerable<Articulo> GetAll(string type, string clasificacion, string aparato, string marca, string modelo, string estado, string tienda, string offset);
        IEnumerable<Articulo> GetAll(string q);
        IEnumerable<Articulo> GetAll();
        Articulo Get(int id);
       /* Articulo Add(Articulo item);*/
        Articulo AddNew(Articulo item);
        Articulo AddNew(Articulo item, string token);
        void Remove(int id);
        bool Update(Articulo item);
    }
}
