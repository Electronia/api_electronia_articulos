using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Base_Datos;
using System.Data.Common;
using ProductStore.Models;

namespace api_electronia_articulos.Models
{
    public class AccessRepository : IAccess
    {

        private Access access = new Access();
        public Access Get(string login, string password)
        {
            UserRepositoryAccess(login, password);
            return access; // articulos.Find(p => p.articuloID == id);
        }

        // para la autenticacion
        public void UserRepositoryAccess(string login, string password)
        {

            access = new Access();
            decimal user_id = 0;
            string user_email = "";
            int user_error = 1;
            string code_error = "";
            string user_nombre = "";

            string valor1 = "";
            string valor2 = "";
            string key = "";
            
            String _query = "exec sp_login '"+login+"','"+password+"' ";
            BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            while (DRConexion.Read())
            {
                user_id = DRConexion.GetDecimal(0);
                user_email = DRConexion.GetString(1);
                user_error = DRConexion.GetInt32(2);
                user_nombre = DRConexion.GetString(3);
            }
            conexion.Desconectar();


            // procesamos token
            if (user_id > 0)
            {
                valor1 = "{\"personaid\":\""+user_id+"\", \"email\":\""+user_email+"\", \"error\":\""+user_error+"\", \"nombre\":\""+user_nombre+"\"}";
                valor2 = "enia2013";
                // encriptamos el valor 
                Encriptador enc = new Encriptador();
                valor1 = enc.Encriptar(valor1);
                //valor2 = enc.Encriptar(valor2);
                key =  valor1+"_"+valor2;

            }

            if (user_error != 0)
            {
                code_error = "Invalid AccessToken";
                key = "";

            }
            else
            {
                code_error = "Valid AccessToken";
            }

            access = new Access
            {
                //userID = DRConexion.GetDecimal(0),
                token = key.Replace("+","%2B").Replace("/","%2F"),
                codigo = code_error
            };


        }
    }
}