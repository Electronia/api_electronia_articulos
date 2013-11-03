using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductStore.Models;
using System.IO;

namespace ProductStore.Controllers
{
    public class ArticulosController : ApiController
    {
        static readonly IArticuloRepository repository = new ArticuloRepository();

        public IEnumerable<Articulo> GetAllArticulos(string key, int type)
        {
            string _key;
            Encriptador enc = new Encriptador();
            _key =enc.Desencriptar(key);

            return repository.GetAll(_key, type);
        }

        public IEnumerable<Articulo> GetAllArticulos(string type, string clasificacion, string aparato, string marca, string modelo, string estado, string tienda, string offset)
        {
            

            return repository.GetAll(type,  clasificacion,  aparato,  marca,  modelo,  estado,  tienda, offset);
        }

        public IEnumerable<Articulo> GetAllArticulos(string q)
        {


            return repository.GetAll(q);
        }

        public Articulo GetArticulo(int id)
        {
            Articulo item = repository.Get(id);
            
            if (item == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return item;
        }


        public IEnumerable<Articulo> GetArticulosByCategory(string clasificacion)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.clasificacion, clasificacion, StringComparison.OrdinalIgnoreCase));
        }

       /*
        public HttpResponseMessage PostArticulo(Articulo item)
        {
            
            
            item = repository.Add(item);
            var response = Request.CreateResponse<Articulo>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.articuloID });
            response.Headers.Location = new Uri(uri);
            return response;
        }*/

        public Articulo PostArticulo(Articulo item)
        {

            
            //item = repository.Add(item);
            StreamWriter writer = File.AppendText("D:\\electroniaLogs\\pruebasPostdave.txt");
            writer.WriteLine("mi itme creado = " + item.marca + item.modelo);
            writer.Close();
            //var response = Request.CreateResponse<Articulo>(HttpStatusCode.Created, item);
            //string uri = Url.Link("DefaultApi", new { id = item.articuloID });
            //var relativePath = "/api/articulos?id=31";
            //response.Headers.Location = new Uri(Request.RequestUri, relativePath);
            //return response;
            Articulo newItem = new Articulo();
            newItem = repository.AddNew(item);
            return newItem;

        }

        public Articulo PostArticulo(Articulo item, string token)
        {
            item = repository.AddNew(item, token);

            return item;
        }


        public void PutArticulo(int id, Articulo contact)
        {
            contact.articuloID = id;
            if (!repository.Update(contact))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
        }


        public HttpResponseMessage DeleteArticulo(int id)
        {
            repository.Remove(id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

    }
}
