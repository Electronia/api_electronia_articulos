using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Base_Datos;
using System.Data.Common;
using System.IO;
using ProductStore.Models;

namespace api_electronia_articulos.Models
{
    public class UserRepository: IUserRepository
    {
      
        private User user = new User();

        // constructor no hace nada
        public UserRepository(){   }

        // obtenemos los datos de un usuario en particular
        public void UserRepositoryGet(int id)
        {
          
            user = new User();
            try
            {
                String _query = "exec [sp_api_getUser] " + id;
                BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
                conexion.Conectar();
                conexion.CrearComando(_query); //Comando indica que quiero encontrar
                DbDataReader DRConexion = conexion.EjecutarConsulta();
                while (DRConexion.Read())
                {

                    user = new User
                    {
                        userID = DRConexion.GetDecimal(0),
                        nombre = DRConexion.GetString(1),
                        apellidos = DRConexion.GetString(2),
                        email = DRConexion.GetString(3),
                        password = DRConexion.GetString(4),
                        fechaNan = DRConexion.GetDateTime(5),
                        sexo = DRConexion.GetString(6),
                        telefonos = DRConexion.GetString(7),
                        status = DRConexion.GetInt32(8).ToString(),
                        location = new Location
                        {
                            estadoID = DRConexion.GetInt32(9),
                            estado = DRConexion.GetString(10)
                        }
                    };

                }
                conexion.Desconectar();
            }
            catch
            {
                   
            }

        }

        //buscamos un usuario con algun criterio de busqueda by query
        public void UserRepositoryGet(string q)
        {
            // exec [sp_api_SearchUser] 'gio.paz@electronia.com.mx'
        }

        public bool UserRepositoryGetExisteEmail(string email)
        {
            int mailValido = -1;
            bool response = false;
            // validamos el email
            String _query = "exec [sp_validaMail] '" + email + "'";
            BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            while (DRConexion.Read())
            {

                mailValido = DRConexion.GetInt32(0);

            }
            if (mailValido == 0)
            {
                response = true;
            }
            conexion.Desconectar();
            return response;

        }


        // registramos un usuario nuevo
        public User UserRepositoryNew(User usuario)
        {
            user = new User();
            String _query = "";
            BaseDatos conexion = new BaseDatos("SQL");

            // si el mail es valido
            if (!UserRepositoryGetExisteEmail(usuario.email))
            {
                _query = "exec  [sp_api_newUser] '" + usuario.nombre + "','"+
                    usuario.apellidos+"','"+usuario.fechaNan.Year+"/"+usuario.fechaNan.Day+"/"+usuario.fechaNan.Month+"','"+
                    usuario.sexo+"','"+usuario.telefonos+"',"+usuario.location.estadoID+",'"+usuario.email+"','"+usuario.password+"'"; 
                conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
                conexion.Conectar();
                conexion.CrearComando(_query); //Comando indica que quiero encontrar
                DbDataReader DRConexion = conexion.EjecutarConsulta();
                while (DRConexion.Read())
                {


                    user = new User
                    {
                        userID = DRConexion.GetDecimal(0),
                        nombre = DRConexion.GetString(1),
                        apellidos = DRConexion.GetString(2),
                        email = DRConexion.GetString(3),
                        password = DRConexion.GetString(4),
                        fechaNan = DRConexion.GetDateTime(5),
                        sexo = DRConexion.GetString(6),
                        telefonos = DRConexion.GetString(7),
                        status = DRConexion.GetInt32(8).ToString(),
                        location = new Location
                        {
                            estadoID = DRConexion.GetInt32(9),
                            estado = DRConexion.GetString(10)
                        }
                    };

                }
                conexion.Desconectar();
            }            
            return user;
        }

        // modificamos los datos de los usuarios [solo datos permitidos]
        public User UserRepositoryUpdate(int userid, User usuario)
        {
            user = new User();
            

                String _query = "exec  [sp_api_updateUser] "+userid+",'" + usuario.nombre + "','" +
                    usuario.apellidos + "','" + usuario.fechaNan.Year + "/" + usuario.fechaNan.Day + "/" + usuario.fechaNan.Month + "','" +
                    usuario.sexo + "','" + usuario.telefonos + "'," + usuario.location.estadoID + ",'" + usuario.password + "'";
                BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
                conexion.Conectar();
                conexion.CrearComando(_query); //Comando indica que quiero encontrar
                DbDataReader DRConexion = conexion.EjecutarConsulta();
                while (DRConexion.Read())
                {


                    user = new User
                    {
                        userID = DRConexion.GetDecimal(0),
                        nombre = DRConexion.GetString(1),
                        apellidos = DRConexion.GetString(2),
                        email = DRConexion.GetString(3),
                        password = DRConexion.GetString(4),
                        fechaNan = DRConexion.GetDateTime(5),
                        sexo = DRConexion.GetString(6),
                        telefonos = DRConexion.GetString(7),
                        status = DRConexion.GetInt32(8).ToString(),
                        location = new Location
                        {
                            estadoID = DRConexion.GetInt32(9),
                            estado = DRConexion.GetString(10)
                        }
                    };

                }
                conexion.Desconectar();

            return user;
        }

        // metodo para activar al usuario registrado
        public bool UserActived(int personaid)
        {
            int actived = 1;
            bool response = false;
            // validamos el email
            String _query = "exec [sp_api_activeUser] " + personaid;
            BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            while (DRConexion.Read())
            {

                actived = DRConexion.GetInt32(0);

            }
            if (actived == 0)
            {
                response = true;
            }
            conexion.Desconectar();
            return response;

        }

        private string EvaluaDato(string _dato)
        {
            if (string.IsNullOrEmpty(_dato)){return "";}
            else{return _dato;}
        }


        public User Get(int id)
        {
            UserRepositoryGet(id);
            return user; // articulos.Find(p => p.articuloID == id);
        }

        public bool Get(string id, string token)
        {
            
            Encriptador enc = new Encriptador();
            try
            {
                string data_token = enc.Desencriptar(token);
                return UserActived(int.Parse(data_token));
            }
            catch
            {
                return false;
            }

        }
        
        public bool Get(string email)
        {
            return (UserRepositoryGetExisteEmail(email));
        }

        public User Add(User item)
        {
            user = UserRepositoryNew(item);
            return user;
        }

        public void Remove(int id)
        {
           // user.RemoveAll(p => p.articuloID == id);
        }

        public User Update(int userid, User item)
        {

            user = UserRepositoryUpdate(userid, item);
            return user;
        }

        
        

        private string procesaVariable(string value)
        {
            if (string.IsNullOrEmpty(value)) { return "0"; } else { return value; }
        }
        
    }
}