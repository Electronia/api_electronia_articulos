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
    public class ItemController : ApiController
    {
        static readonly itemInterface itemManagerService = new itemManagerService();

        public Item GetItem(string id)
        {


            Item item =  itemManagerService.Get(id);

            if (item == null || item.itemId == "")
            {

                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "Product ID Not Found",
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(resp);


            }

            PictureManagerService pictureService = new PictureManagerService();
            
            item.pictures =  pictureService.GetAll(item.itemId);
            return item;
        }

        public Item PostItem(string userID, Item itemNew)
        {

            Item item = itemManagerService.Add(userID, itemNew);

                if (item == null || item.itemId == "")
                {

                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("No product with ID = {0}", userID)),
                        ReasonPhrase = "Product ID Not Found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(resp);


               }
        
            return item;
        }
    }
}
