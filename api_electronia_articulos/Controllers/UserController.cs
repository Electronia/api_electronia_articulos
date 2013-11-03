using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api_electronia_articulos.Models;
using System.IO;
using System.Web.Mvc;

namespace api_electronia_articulos.Controllers
{
    public class UserController :ApiController
    {
        static readonly IUserRepository repository = new UserRepository();
       
        public User GetUser(int id)
        {
            
            
            User item = repository.Get(id);

            if (item == null || item.userID == 0)
            {
               
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "Product ID Not Found",
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(resp);
                
                
            }
            return item;
        }


        public ResponseMessageAPI GetUser(string email)
        {
            ResponseMessageAPI responseMessage = new ResponseMessageAPI();
            if (repository.Get(email))
            {
                responseMessage.status = "exists";
                responseMessage.menssage = "El email ya esta registrado";
                responseMessage.HttpStatusCode = HttpStatusCode.Conflict;
            }
            else
            {
                responseMessage.status = "no exists";
                responseMessage.menssage = "El email  no se encuentra registrado";
                responseMessage.HttpStatusCode = HttpStatusCode.Accepted;
            }

             return responseMessage;
        }

        public ResponseMessageAPI GetUser(string id, string token)
        {
           

            ResponseMessageAPI responseMessage = new ResponseMessageAPI();
            if (repository.Get(id, token))
            {
                responseMessage.status = "actived";
                responseMessage.menssage = "Usuario Activo";
                responseMessage.HttpStatusCode = HttpStatusCode.Accepted;
            }
            else
            {
                responseMessage.status = "not actived";
                responseMessage.menssage = "Usuario Inactivo";
                responseMessage.HttpStatusCode = HttpStatusCode.Conflict;
            }

            return responseMessage;
        }

        public User PostUser(User item)
        {

           
            item = repository.Add(item);

            var response = new HttpResponseMessage(HttpStatusCode.Created);

            if (item == null || item.userID == 0)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se pudo crear el item"),
                    ReasonPhrase = "Item not create",
                    StatusCode = HttpStatusCode.BadRequest
                };
                throw new HttpResponseException(resp);
            }
            return item;

        }



        public User PutUser(int id, User user)
        {
            user = repository.Update(id, user);

            var response = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent("Item modificado "),
                ReasonPhrase = "Item update",
                StatusCode = HttpStatusCode.Accepted
            };

            if (user == null || user.userID == 0)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se pudo actualizar el item"),
                    ReasonPhrase = "Item not update",
                    StatusCode = HttpStatusCode.BadRequest
                };
                throw new HttpResponseException(resp);
            }
            return user;
        }

        
        public HttpResponseMessage DeleteUser(int id)
        {
            
            var resp = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)
            {
                Content = new StringContent("Este metodo no esta disponible"),
                ReasonPhrase = "Metodo no diponible",
                StatusCode = HttpStatusCode.MethodNotAllowed
            };
            return resp;

        }

        

    }
}
