using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api_electronia_articulos.Models;

namespace api_electronia_articulos.Controllers
{
    public class accessController : ApiController
    {
        static readonly IAccess repository = new AccessRepository();
        public Access GetAccess(string login, string password)
        {
            Access item = repository.Get(login, password);

            return item;

           
        }
    }
}
