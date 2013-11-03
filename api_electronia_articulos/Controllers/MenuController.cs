using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api_electronia_articulos.Models;

namespace api_electronia_articulos.Controllers
{
    public class MenuController : ApiController
    {
        static readonly IMenuRepository repository = new MenuRepository();

        public IEnumerable<Menu> GetAllMenu()
        {
           

            return repository.GetAll();
        }

        public IEnumerable<Menu> GetAllMenu(string type, string clasificacion, string aparato, string marca, string modelo, string estado, string tienda)
        {
            

            return repository.GetAll(type, clasificacion, aparato, marca, modelo, estado, tienda);
        }
       
    }
}
