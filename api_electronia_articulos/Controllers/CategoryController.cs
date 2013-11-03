using api_electronia_articulos.Interfaces;
using api_electronia_articulos.Models;
using api_electronia_articulos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api_electronia_articulos.Controllers
{
    public class CategoryController : ApiController
    {
        static readonly categoryInterface categoryManagerService = new categoryManagerService();

        public IEnumerable<Category> GetCategory(string id)
        {


            IEnumerable<Category> category = categoryManagerService.Get(id);

            if (category.Count() == 0)
            {

                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No category with ID = {0}", id)),
                    ReasonPhrase = "Category ID Not Found",
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(resp);


            }
            return category;
        }

        public IEnumerable<Category> GetCategory()
        {


            IEnumerable<Category> category = categoryManagerService.Get();

            if (category.Count() == 0)
            {

                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("No categories"),
                    ReasonPhrase = "Category ID Not Found",
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(resp);


            }
            return category;
        }
    }
}
