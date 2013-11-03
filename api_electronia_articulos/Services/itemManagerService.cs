using api_electronia_articulos.Interfaces;
using api_electronia_articulos.Models;
using Base_Datos;
using ProductStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace api_electronia_articulos.Services
{
    public class itemManagerService : itemInterface
    {
        private Item item = new Item();
        BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
        public itemManagerService() { }

        // ----- metodos de la interface ------

        public Item Get(string id)
        {
            ItemGet(id);
            return item;
        }

        public Item Add(string id, Item itemNew)
        {
            Encriptador enc = new Encriptador();
           
            try
            {
                id = enc.Desencriptar(id);
               
            }
            catch
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NonAuthoritativeInformation)
                {
                    Content = new StringContent(string.Format("User invalid = {0}", id)),
                    ReasonPhrase = "User ID Not Found",
                    StatusCode = HttpStatusCode.NonAuthoritativeInformation
                };
                throw new HttpResponseException(resp);
            }

            ItemPost(id, itemNew);

            return item;

           
        }


        // ----- metodos de servicio internos -----
        private void ItemGet(string id)
        {

            item = new Item();
            try
            {
                String _query = "exec [sp_api_getItem] '" + id + "'";
                conexion.Conectar();
                conexion.CrearComando(_query); //Comando indica que quiero encontrar
                DbDataReader DRConexion = conexion.EjecutarConsulta();
                while (DRConexion.Read())
                {

                    item = new Item
                    {
                        itemId = DRConexion.GetString(0),
                        title = DRConexion.GetString(1),
                        categoryId = DRConexion.GetString(2),
                        state = DRConexion.GetInt32(4),
                        price = DRConexion.GetDecimal(5),
                        typeCurrency = DRConexion.GetString(6),
                        status = DRConexion.GetString(7),
                        typeItem = DRConexion.GetString(8),
                        dateRegistration = DRConexion.GetDateTime(9),
                        dateExpired = DRConexion.GetDateTime(10),
                        dateActivation = DRConexion.GetDateTime(11),
                        dateRenovation = DRConexion.GetDateTime(12),
                        description = DRConexion.GetString(13),
                        usage = DRConexion.GetString(14),
                        stateName = DRConexion.GetString(15)
                    };
                    
                }
                conexion.Desconectar();
            }
            catch
            {
                item = new Item
                {
                    itemId = null
                };
            }

        }

        private void ItemPost(string userId, Item itemNew)
        {
            item = new Item();
            try
            {
                String _query = "exec [sp_api_newItem] '" + itemNew.categoryId + "', "
                    + "'" + itemNew.title + "', '" + userId + "'," + itemNew.state + "," + itemNew.price + ","
                    + "'" + itemNew.typeCurrency + "', '" + itemNew.status + "',"
                    + "'" + itemNew.typeItem + "', '" + itemNew.description + "', '" + itemNew.usage + "'";

              
                conexion.Conectar();
                conexion.CrearComando(_query); 
                DbDataReader DRConexion = conexion.EjecutarConsulta();
                while (DRConexion.Read())
                {

                    item = new Item
                    {
                        itemId = DRConexion.GetString(0),
                        title = DRConexion.GetString(1),
                        categoryId = DRConexion.GetString(2),
                        state = DRConexion.GetInt32(4),
                        price = DRConexion.GetDecimal(5),
                        typeCurrency = DRConexion.GetString(6),
                        status = DRConexion.GetString(7),
                        typeItem = DRConexion.GetString(8),
                        dateRegistration = DRConexion.GetDateTime(9),
                        dateExpired = DRConexion.GetDateTime(10),
                        dateActivation = DRConexion.GetDateTime(11),
                        dateRenovation = DRConexion.GetDateTime(12),
                        description = DRConexion.GetString(13),
                        usage = DRConexion.GetString(14)
                    };

                }
                conexion.Desconectar();
            }
            catch
            {
                item = new Item
                {
                    itemId = null
                };
            }

        }
    }
}