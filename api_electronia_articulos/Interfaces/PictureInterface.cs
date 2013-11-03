using api_electronia_articulos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace api_electronia_articulos.Interfaces
{
    interface PictureInterface
    {
        /*
         IEnumerable<Articulo> GetAll(string key, int type);
        IEnumerable<Articulo> GetAll(string type, string clasificacion, string aparato, string marca, string modelo, string estado, string tienda, string offset);
        IEnumerable<Articulo> GetAll(string q);
        IEnumerable<Articulo> GetAll();
         */
        IEnumerable<Picture> GetAll(string item_id);
        Picture Get(string id);
        Picture Add(string item_id, Picture picture);
        bool Remove(string id, int num_photo);
        /*Item Update(string id, Item item);*/
    }
}
