using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Base_Datos;
using System.Data.Common;
using System.IO;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using api_electronia_articulos.Models;

namespace ProductStore.Models
{
    public class ArticuloRepository : IArticuloRepository
    {
        private List<Articulo> articulos = new List<Articulo>();
        private Articulo articulo = new Articulo();
        //private int _nextId = 1;

        //******** METODO GET ***************************
        public ArticuloRepository()
        {
            
            // vamos a generar un listado de anuncios
            articulos.Clear();
            String _query = "exec [sp_consulta_anuncios] ''";
            BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            while (DRConexion.Read())
            {

                //generamos el elemento
                //elemento elemento = new elemento();
                // asignamos los valores al elemento
                // obtenemos los datos de la consulta
                articulos.Add(new Articulo
                {
                    articuloID = int.Parse(DRConexion.GetDecimal(0).ToString()),
                    identificador = DRConexion.GetString(1),
                    clasificacion = DRConexion.GetString(11),
                    aparato = DRConexion.GetString(10),
                    marca = DRConexion.GetString(8),
                    precio = DRConexion.GetDecimal(17),
                    fechaRegistro = DRConexion.GetDateTime(34),
                    fechaModificacion = DRConexion.GetDateTime(35),
                    descripcion = DRConexion.GetString(12),
                    estadoID = DRConexion.GetInt32(13),
                    estado = DRConexion.GetString(14),
                    tipoAnuncioID = DRConexion.GetInt32(2),
                    tipoAnuncio = DRConexion.GetString(3),
                    marcaID = DRConexion.GetDecimal(4),
                    modeloID = DRConexion.GetDecimal(5),
                    aparatoID = DRConexion.GetInt32(6),
                    clasificacionID = DRConexion.GetInt32(7),
                    modelo = DRConexion.GetString(9),
                    numFotos = DRConexion.GetInt32(15),
                    fotoPrincipal = DRConexion.GetString(16),
                    tipoMoneda = DRConexion.GetString(18),
                    color = DRConexion.GetString(19),
                    archivoVideo = DRConexion.GetString(20),
                    caracteristicas = DRConexion.GetString(21),
                    personaID = DRConexion.GetDecimal(22),
                    tiendaID = DRConexion.GetDecimal(23),
                    persona = DRConexion.GetString(24),
                    tienda = EvaluaDato(DRConexion.GetString(25)),
                    telefonos = EvaluaDato(DRConexion.GetString(26)) + EvaluaDato(DRConexion.GetString(27)) + EvaluaDato(DRConexion.GetString(28)) + EvaluaDato(DRConexion.GetString(29)),
                    direccion = EvaluaDato(DRConexion.GetString(30)),
                    statusAnuncioID = DRConexion.GetInt32(33),
                    financiamiento = EvaluaDato(DRConexion.GetString(36))

                });

                
            }
            conexion.Desconectar();
            
        }

        // metodo para obtener los articulos para el admin

        public void ArticuloRepositoryAdmin(string key, int type)
        {

            // vamos a generar un listado de anuncios
            articulos.Clear();

            String _query = "exec [sp_consulta_anuncios_admin] " + key + ", "+ type;
            BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            while (DRConexion.Read())
            {

                articulos.Add(new Articulo
                {
                    articuloID = int.Parse(DRConexion.GetDecimal(0).ToString()),
                    identificador = DRConexion.GetString(1),
                    tipoAnuncioID = DRConexion.GetInt32(2),
                    tipoAnuncio = DRConexion.GetString(3),
                    marcaID = DRConexion.GetDecimal(4),
                    modeloID = DRConexion.GetDecimal(5),
                    aparatoID = DRConexion.GetInt32(6),
                    clasificacionID = DRConexion.GetInt32(7),
                    marca = DRConexion.GetString(8),
                    modelo = DRConexion.GetString(9),
                    aparato = DRConexion.GetString(10),
                    clasificacion = DRConexion.GetString(11),
                    descripcion = DRConexion.GetString(12),
                    estadoID = DRConexion.GetInt32(13),
                    estado = DRConexion.GetString(14),
                    numFotos = DRConexion.GetInt32(15),
                    fotoPrincipal = DRConexion.GetString(16),
                    precio = DRConexion.GetDecimal(17),
                    tipoMoneda = DRConexion.GetString(18),
                    color = DRConexion.GetString(19),
                    archivoVideo = DRConexion.GetString(20),
                    caracteristicas = DRConexion.GetString(21),
                    personaID = DRConexion.GetDecimal(22),
                    tiendaID = DRConexion.GetDecimal(23),
                    persona = DRConexion.GetString(24),
                    tienda = EvaluaDato(DRConexion.GetString(25)),
                    telefonos = EvaluaDato(DRConexion.GetString(26)) + EvaluaDato(DRConexion.GetString(27)) + EvaluaDato(DRConexion.GetString(28)) + EvaluaDato(DRConexion.GetString(29)),
                    direccion = EvaluaDato(DRConexion.GetString(30)),
                    statusAnuncioID = DRConexion.GetInt32(33),
                    fechaRegistro = DRConexion.GetDateTime(34),
                    fechaModificacion = DRConexion.GetDateTime(35),
                    financiamiento = EvaluaDato(DRConexion.GetString(36))

                });

            }
            conexion.Desconectar();

        }

        public void ArticuloRepositoryListado(string type, string clasificacion, string aparato, string marca, string modelo, string estado, string tienda, string offset)
        {

            // vamos a generar un listado de anuncios
            articulos.Clear();
            String _query = "execute [sp_consultaListados] '" + type + "'," + clasificacion + "," + aparato + "," + marca + "," + modelo + "," + estado + "," + tienda + "," + offset + "";
            BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            while (DRConexion.Read())
            {

                articulos.Add(new Articulo
                {
                    
                    
                    articuloID = int.Parse(DRConexion.GetDecimal(0).ToString()),
                    identificador = DRConexion.GetString(1),
                    fechaRegistro = DRConexion.GetDateTime(2),
                    tiendaID = DRConexion.GetDecimal(3),
                    clasificacionID = DRConexion.GetInt32(4),
                    clasificacion = DRConexion.GetString(5),
                    aparatoID = DRConexion.GetInt32(6),
                    aparato = DRConexion.GetString(7),
                    marcaID = DRConexion.GetDecimal(8),
                    marca = DRConexion.GetString(9),
                    modeloID = DRConexion.GetDecimal(10),
                    modelo = DRConexion.GetString(11),
                    precio = DRConexion.GetDecimal(12),
                    estado = DRConexion.GetString(13),
                    numFotos = DRConexion.GetInt32(14),
                    financiamiento = EvaluaDato(DRConexion.GetString(15))
                    

                });

            }
            conexion.Desconectar();

        }


        // para la busqueda por texto abierto

        public void ArticuloRepositoryFree(string query)
        {

            // vamos a generar un listado de anuncios
            if (string.IsNullOrEmpty(query)) { query = "*";}
            articulos.Clear();
            String _query = "execute [sp_consulta_anuncios_free] '" + query + "'";
            BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            while (DRConexion.Read())
            {

                articulos.Add(new Articulo
                {


                    articuloID = int.Parse(DRConexion.GetDecimal(0).ToString()),
                    identificador = DRConexion.GetString(1),
                    fechaRegistro = DRConexion.GetDateTime(2),
                    tiendaID = DRConexion.GetDecimal(3),
                    clasificacionID = DRConexion.GetInt32(4),
                    clasificacion = DRConexion.GetString(5),
                    aparatoID = DRConexion.GetInt32(6),
                    aparato = DRConexion.GetString(7),
                    marcaID = DRConexion.GetDecimal(8),
                    marca = DRConexion.GetString(9),
                    modeloID = DRConexion.GetDecimal(10),
                    modelo = DRConexion.GetString(11),
                    precio = DRConexion.GetDecimal(12),
                    estado = DRConexion.GetString(13),
                    numFotos = DRConexion.GetInt32(14),
                    financiamiento = EvaluaDato(DRConexion.GetString(15))


                });

            }
            conexion.Desconectar();

        }

        public void ArticuloRepositoryDetalle(int id)
        {

            // vamos a generar un listado de anuncios


            String _query = "exec [sp_consulta_anuncio] " + id;
            BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            while (DRConexion.Read())
            {

                articulo = new Articulo
                {
                    articuloID = int.Parse(DRConexion.GetDecimal(0).ToString()),
                    identificador = DRConexion.GetString(1),
                    fechaRegistro = DRConexion.GetDateTime(34),
                    tiendaID = DRConexion.GetDecimal(23),
                    tienda = DRConexion.GetString(25),
                    clasificacionID = DRConexion.GetInt32(7),
                    clasificacion = DRConexion.GetString(11),
                    aparatoID = DRConexion.GetInt32(6),
                    aparato = DRConexion.GetString(10),
                    marcaID = DRConexion.GetDecimal(4),
                    marca = DRConexion.GetString(8),
                    modeloID = DRConexion.GetDecimal(5),
                    modelo = DRConexion.GetString(9),
                    precio = DRConexion.GetDecimal(17),
                    estado = DRConexion.GetString(14),
                    numFotos = DRConexion.GetInt32(15),
                    financiamiento = EvaluaDato(DRConexion.GetString(36)),
                    tipoAnuncio = DRConexion.GetString(3),    
                    descripcion = DRConexion.GetString(12),
                    tipoMoneda = DRConexion.GetString(18),
                    caracteristicas = DRConexion.GetString(21),
                    persona = DRConexion.GetString(24),
                    telefonos = DRConexion.GetString(26)+DRConexion.GetString(27)+DRConexion.GetString(28)+DRConexion.GetString(29),
                    statusAnuncioID = DRConexion.GetInt32(33)
                };

            }
            conexion.Desconectar();

        }

        //********** METODO POST *****************************

        public Articulo ArticuloRepositoryNew(Articulo item)
        {

            articulo = new Articulo();
            
            // validamos el email
            String _query = " exec [dbo].[sp_api_newArticulo] " +
                item.clasificacionID + "," + item.aparatoID + "," +
               item.marcaID + "," + item.modeloID + "," + item.personaID + "," +
               item.estadoID + "," + item.precio + ",'" + item.color + "'," + item.numFotos +
               ",'" + item.fotoPrincipal + "'," + item.statusAnuncioID + "," + item.tipoAnuncioID +
               "," + item.tiendaID + ",'" + item.clasificacion + "','" + item.marca + "','" + item.modelo +
               "','" + item.aparato + "'," +
               "'" + item.descripcion + "','192.168.121.2','" + item.archivoVideo +
               "','" + item.tipoMoneda + "'," + item.financiamiento + "," +
            "0, 1, '"+item.caracteristicas+"'" ;


            // generar un stored procedure referente a el post del articulo


            StreamWriter writer = File.AppendText("D:\\electroniaLogs\\pruebasPostArticulo2.txt");
            writer.WriteLine("mi itme creado = " + _query);
            writer.Close();
            BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            while (DRConexion.Read())
            {

                
                articulo = new Articulo
                {
                    articuloID = int.Parse(DRConexion.GetDecimal(0).ToString()),
                    identificador = DRConexion.GetString(1),
                    fechaRegistro = DRConexion.GetDateTime(34),
                    tiendaID = DRConexion.GetDecimal(23),
                    tienda = DRConexion.GetString(25),
                    clasificacionID = DRConexion.GetInt32(7),
                    clasificacion = DRConexion.GetString(11),
                    aparatoID = DRConexion.GetInt32(6),
                    aparato = DRConexion.GetString(10),
                    marcaID = DRConexion.GetDecimal(4),
                    marca = DRConexion.GetString(8),
                    modeloID = DRConexion.GetDecimal(5),
                    modelo = DRConexion.GetString(9),
                    precio = DRConexion.GetDecimal(17),
                    estado = DRConexion.GetString(14),
                    numFotos = DRConexion.GetInt32(15),
                    financiamiento = EvaluaDato(DRConexion.GetString(36)),
                    tipoAnuncio = DRConexion.GetString(3),
                    tipoAnuncioID = DRConexion.GetInt32(2),
                    descripcion = DRConexion.GetString(12),
                    tipoMoneda = DRConexion.GetString(18),
                    caracteristicas = DRConexion.GetString(21),
                    persona = DRConexion.GetString(24),
                    telefonos = DRConexion.GetString(26) + DRConexion.GetString(27) + DRConexion.GetString(28) + DRConexion.GetString(29),
                    statusAnuncioID = DRConexion.GetInt32(33)
                };

            }
            conexion.Desconectar();

            return articulo;
        }

        public Articulo ArticuloRepositoryNew(Articulo item, string token)
        {
            StreamWriter writer = File.AppendText("D:\\electroniaLogs\\pruebasPostTokenEncr.txt");
            writer.WriteLine("mi itme creado = " + token);
            writer.Close();
            
            item.personaID = validaAccessToken(token);
            if (item.personaID > 0)
            {
                item = ArticuloRepositoryNew(item);
            }
            else
            {
                item = new Articulo();
            }
            return item;
        }

        private decimal validaAccessToken(string token)
        {

            string data_token = "";
            string origen = "_enia2013";
            token = token.Replace(origen, "");
            Encriptador enc = new Encriptador();
            data_token = enc.Desencriptar(token);
            bool mailValido = false;
            bool token_valido = false;
            decimal personaid = 0;

            StreamWriter writer = File.AppendText("D:\\electroniaLogs\\pruebasPostTokenDEcri.txt");
            writer.WriteLine("mi itme creado = " + data_token);
            writer.Close();

            User user = new User();
            try
            {
                JObject myJson = JObject.Parse(data_token);
                user.email = (string)myJson["email"];
                user.userID = decimal.Parse((string)myJson["personaid"]);
                user.nombre = (string)myJson["nombre"];
                if ((string)myJson["error"] == "0") { token_valido = true; }
            }
            catch (Exception e)
            {
                token_valido = false;
                writer = File.AppendText("D:\\electroniaLogs\\pruebasPostTokenException.txt");
                writer.WriteLine("mi itme creado = " + e + " -mitkn="+data_token);
                writer.Close();
            }

            

            if (token_valido)
            {
                // validamos el email
                String _query = "exec [sp_validaMail] '" + user.email + "'";
                BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
                conexion.Conectar();
                conexion.CrearComando(_query); //Comando indica que quiero encontrar
                DbDataReader DRConexion = conexion.EjecutarConsulta();
                while (DRConexion.Read())
                {

                    if (DRConexion.GetInt32(0) == 0) { mailValido = true; }

                }
                conexion.Desconectar();

                // si el mail es valido
                if (mailValido)
                {
                     //agregamos al item la persona id para que la procese
                    personaid = user.userID;   
                }

               

            }



            //data_token = " {“personaid”:”valor”, “email”:”valor”, “error”:”valor”, “nombre”:”valor”}";
            /*
             *   List<Menu> menu = new List<Menu>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.electronia.com.mx");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Send an HTTP GET request. Blocking!
            HttpResponseMessage resp = client.GetAsync("/api/api/menu?type=" + typeMenu + "&clasificacion=" + clasificacion + "&aparato=" + aparato + "&marca=" + marca +
                "&modelo=" + modelo + "&estado=" + estado + "&tienda=" + tienda + "").Result;
            if (resp.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var products = resp.Content.ReadAsAsync<IEnumerable<Menu>>().Result;
                //ViewData["listado"] = "";
                menu.Clear();
                foreach (var p in products)
                {
                    menu.Add(
                      new Menu
                      {
                          id = p.id,
                          tipo = typeMenu,
                          item = p.item,
                          cantidad = p.cantidad,
                          url = "http:electronia.mx/laurldefinir"
                          
                      });
                    // + p.articuloID + "-" + p.identificador + "-" + p.clasificacion + "-" + p.aparato + "-" + p.precio;

                }
            }
            else
            {
                // Response.Write((int)resp.StatusCode + resp.ReasonPhrase);
            }

            return menu;
             */

            return personaid;
        }



        private string EvaluaDato(string _dato)
        {
            if (string.IsNullOrEmpty(_dato))
            {
                return "";
            }
            else
            {
                return _dato;
            }
        }

        public IEnumerable<Articulo> GetAll()
        {
            return articulos;
        }

        // para la busqueda por palabra
         public IEnumerable<Articulo> GetAll(string q)
        {
            ArticuloRepositoryFree(q);
             return articulos;
        }
        

        // para los listados de administracion
        public IEnumerable<Articulo> GetAll( string key, int type)
        {
            ArticuloRepositoryAdmin(key, type);
            return articulos;
        }
        
        // para los listados por filtros
        public IEnumerable<Articulo> GetAll(string type, string clasificacion, string aparato, string marca, string modelo, string estado, string tienda, string offset)
        {
            // procesamiento de variables
            if (string.IsNullOrEmpty(type)) { type = "listado"; }
            clasificacion = procesaVariable(clasificacion);
            aparato = procesaVariable(aparato);
            marca = procesaVariable(marca);
            modelo = procesaVariable(modelo);
            estado = procesaVariable(estado);
            tienda = procesaVariable(tienda);
            offset = procesaVariable(offset);
            
            ArticuloRepositoryListado( type, clasificacion, aparato,  marca,  modelo, estado,  tienda,  offset);
            return articulos;
        }

        public Articulo Get(int id)
        {
            ArticuloRepositoryDetalle(id);
            return articulo; // articulos.Find(p => p.articuloID == id);
        }

       /* public Articulo Add(Articulo item)
        {
           // item = ArticuloRepositoryNew(item);
            //articulos.Add(item);
            return item;
        }*/

        public Articulo AddNew(Articulo item)
        {            
            item = ArticuloRepositoryNew(item);
            return item;
        }

        public Articulo AddNew(Articulo item, string token)
        {
            item = ArticuloRepositoryNew(item, token);
            return item;
        }

        public void Remove(int id)
        {
            articulos.RemoveAll(p => p.articuloID == id);
        }

        public bool Update(Articulo item)
        {
            int index = articulos.FindIndex(p => p.articuloID == item.articuloID);
            if (index == -1)
            {
                return false;
            }
            articulos.RemoveAt(index);
            articulos.Add(item);
            return true;
        }

        private string procesaVariable(string value)
        {
            if (string.IsNullOrEmpty(value)) { return "0"; } else { return value; }
        }
        
    }
}