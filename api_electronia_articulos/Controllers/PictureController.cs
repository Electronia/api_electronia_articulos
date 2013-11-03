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
    public class PictureController : ApiController
    {

        static readonly PictureInterface pictureManagerService = new PictureManagerService();
        // GET api/picture
        
        public Picture Get(string id, string picture_id)
        {
            Picture picture = new Picture();
            picture = pictureManagerService.Get(picture_id);
            if (picture == null || picture.url_general == null)
            {

                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No pictures with ID = {0}", id)),
                    ReasonPhrase = "Pictures ID Not Found",
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(resp);


            }
            return picture;

        }

        // GET api/picture/5
        public IEnumerable<Picture> Get(string id)
        {
            IEnumerable<Picture> pictures = new List<Picture>();

            pictures = pictureManagerService.GetAll(id);
            if (pictures == null || pictures.Count() == 0)
            {

                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No pictures with ID = {0}", id)),
                    ReasonPhrase = "Pictures ID Not Found",
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(resp);


            }
            return pictures;
        }

        
        // POST api/picture
        public Picture Post(string id, Picture pictureBase)
        {
            Picture newPicture = pictureManagerService.Add(id, pictureBase);

            if (newPicture == null || newPicture.url_general == "")
            {

                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No picture with ID = {0}", id)),
                    ReasonPhrase = "Picture ID Not Found",
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(resp);


            }

            return newPicture;
        }

        /*
        // PUT api/picture/5
        public void Put(int id, [FromBody]string value)
        {
        }*/

        // DELETE api/picture/5
        public void Delete(string id, int num_photo)
        {
            if (pictureManagerService.Remove(id, num_photo))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("deleted picture with ID = {0} and photo = {1}", id,num_photo)),
                    ReasonPhrase = "Picture ID deleted",
                    StatusCode = HttpStatusCode.OK
                };
                throw new HttpResponseException(resp);
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("No picture with ID = {0} and photo = {1}", id,num_photo)),
                    ReasonPhrase = "Picture ID Not Found",
                    StatusCode = HttpStatusCode.BadRequest
                };
                throw new HttpResponseException(resp);
            }
        }
    }
}
