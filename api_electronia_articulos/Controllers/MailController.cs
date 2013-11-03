using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api_electronia_articulos.Models;

namespace api_electronia_articulos.Controllers
{
    public class MailController : ApiController
    {
        static readonly IMail repository = new MailRepository();

        public HttpResponseMessage PostMail(Mail item)
        {
         
            HttpResponseMessage response = repository.Add(item);
            return response;

        }

        public string GetMail(string data)
        {

            return repository.Get(data);
        }
    }
}
