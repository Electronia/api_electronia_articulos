using api_electronia_articulos.Interfaces;
using api_electronia_articulos.Models;
using Base_Datos;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace api_electronia_articulos.Services
{
    public class categoryManagerService : categoryInterface
    {
        private List<Category> category = new List<Category>();
        BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
        public categoryManagerService() { }

        public IEnumerable<Category> Get() 
        {
            getCategoryService();
            return category;
        }

        public IEnumerable<Category> Get(string id)
        {
            getCategoryService(id);
            return category;
        }


        // construimos los servicios

        private void getCategoryService()
        {
            category.Clear();
            try
            {
                String _query = "exec sp_api_getCategory null ";
                conexion.Conectar();
                conexion.CrearComando(_query); //Comando indica que quiero encontrar
                DbDataReader DRConexion = conexion.EjecutarConsulta();
                while (DRConexion.Read())
                {

                    category.Add(new Category
                    {
                        category_id = DRConexion.GetString(0),
                        category_id_fad = DRConexion.GetString(1),
                        category_name = DRConexion.GetString(2)
                    });

                }
                conexion.Desconectar();
            }
            catch
            {
                category.Clear();
            }
        }

        private void getCategoryService(string id)
        {
            category.Clear();
            try
            {
                String _query = "exec sp_api_getCategory '" + id + "' ";
                conexion.Conectar();
                conexion.CrearComando(_query); //Comando indica que quiero encontrar
                DbDataReader DRConexion = conexion.EjecutarConsulta();
                while (DRConexion.Read())
                {

                    category.Add( new Category
                    {
                        category_id = DRConexion.GetString(0),
                        category_id_fad = DRConexion.GetString(1),
                        category_name = DRConexion.GetString(2)
                    });

                }
                conexion.Desconectar();
            }
            catch
            {
                category.Clear();
            }
        }

    }
}