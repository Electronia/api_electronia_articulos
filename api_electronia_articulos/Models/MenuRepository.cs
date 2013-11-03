using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Base_Datos;
using System.Data.Common;

namespace api_electronia_articulos.Models
{
    public class MenuRepository : IMenuRepository
    {
        private List<Menu> menu = new List<Menu>();
        
        public IEnumerable<Menu> GetAll()
        {
            generaMenu("categorias");
            return menu;
        }

        
        public IEnumerable<Menu> GetAll(string type, string clasificacion, string aparato, string marca, string modelo, string estado, string tienda)
        {
            // procesamiento de variables
            if (string.IsNullOrEmpty(type)) { type = "categorias"; }
            clasificacion = procesaVariable(clasificacion);
            aparato = procesaVariable(aparato);
            marca = procesaVariable(marca);
            modelo = procesaVariable(modelo);
            estado = procesaVariable(estado);
            tienda = procesaVariable(tienda);

            generaMenu(type, clasificacion, aparato, marca, modelo, estado,tienda);
            return menu;
        }

        private string  procesaVariable(string value)
        {
            if (string.IsNullOrEmpty(value)) { return "0"; } else {return value;}
        }
        

        public void generaMenu(string type, string clasificacion, string aparato, string marca, string modelo, string estado, string tienda)
        {

            // vamos a generar un listado de anuncios
            menu.Clear();

            String _query = "execute sp_consultaContadoresMenus '" + type + "'," + clasificacion + "," + aparato + "," + marca + "," + modelo +
               "," + estado + "," + tienda;
            
            BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            int _id;
            while (DRConexion.Read())
            {

                 
                try { _id = DRConexion.GetInt32(0);}catch {_id = int.Parse(DRConexion.GetDecimal(0).ToString());}
                menu.Add(new Menu
                {
                    ID = _id.ToString(),
                    item = DRConexion.GetString(1),
                    cantidad = DRConexion.GetInt32(2).ToString(),

                });

            }
            conexion.Desconectar();

        }

        public void generaMenu(string type)
        {

            // vamos a generar un listado de anuncios
            menu.Clear();

            String _query = "execute sp_consultaContadoresMenus '" + type + "',0,0,0,0,0,0 ";
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
                menu.Add(new Menu
                {
                    ID = DRConexion.GetDecimal(0).ToString(),
                    item = DRConexion.GetString(1),
                    cantidad = DRConexion.GetInt32(2).ToString(),
                   
                });

            }
            conexion.Desconectar();
             
        }

        public void testMenu()
        {
            menu.Clear();
            menu.Add(new Menu
            {
                ID = "1",
                item = "television",
                cantidad = "1258",

            });

        }

    

    }
}