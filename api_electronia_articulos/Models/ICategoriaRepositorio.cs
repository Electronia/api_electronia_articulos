using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_electronia_articulos.Models
{
    
    interface ICategoriaRepositorio
    {
        IEnumerable<Categoria> GetAll(string type, string marcaID, string aparatoID);
        IEnumerable<Categoria> GetAll();

    }
}