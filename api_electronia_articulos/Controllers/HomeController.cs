using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using api_electronia_articulos.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;

namespace ProductStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
               ViewBag.parametros = Request.Params["callback"];
          
             
           return View();
        }

        public void Test()
        {
            string json = "{\"personaid\":12457, \"email\":\"david@yo.com\", \"error\":\"valor\", \"nombre\":\"valor\"}";
            json = "{....}";
            Response.Write("el json es:" + json);
            User Usuario = new User();
            try
            {
                JObject myJson = JObject.Parse(json);
                Usuario.email = (string)myJson["email"];
                Usuario.userID = (decimal)myJson["personaid"];
                Response.Write("<br>El nombre es:" + Usuario.email);
                Response.Write("<br>El id es:" + Usuario.userID);
            }
            catch(Exception e)
            {
                Response.Write("token invalido code:" + e);
            }
            
            //var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(json);
        }

        public void ValidaEmail(string email)
        {
            string parametros = Request.Params["callback"];
            IUserRepository repository = new UserRepository();
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

            parametros = parametros + " ([{\"status\": \" " + responseMessage.status + " \",\"menssage\": \"" + responseMessage.menssage + "\",\"HttpStatusCode\": \"" + responseMessage.HttpStatusCode+ "\"}]) ";

            Response.Write(parametros);
        }

        
    }
}
