using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using api_electronia_articulos.Models;
using System.Web.Http;

namespace api_electronia_articulos.Controllers
{
    public class CategoriaController : ApiController
    {
       

        static readonly ICategoriaRepositorio repository = new CategoriaRepository();

        public IEnumerable<Categoria> GetAllCategoria()
        {

            return repository.GetAll();
        }

        public IEnumerable<Categoria> GetAllCategoria(string type, string marcaID, string aparatoID)
        {

            return repository.GetAll(type, marcaID, aparatoID);
        }

    }
}
