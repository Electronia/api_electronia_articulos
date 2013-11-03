using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using api_electronia_articulos.Models;
using System.Web.Http;

namespace api_electronia_articulos.Controllers
{
    public class ListadoController : ApiController
    {
       

        static readonly IListaRepositorio repository = new ListaRepository();


        public IEnumerable<Lista> GetAllListado(string type)
        {

            return repository.GetAll(type);
        }

    }
}
