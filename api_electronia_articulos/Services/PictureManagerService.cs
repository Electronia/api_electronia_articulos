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
    public class PictureManagerService: PictureInterface
    {
        public Picture picture = new Picture();
        public List<Picture> pictures = new List<Picture>();
        BaseDatos conexion = new BaseDatos("SQL");
        public PictureManagerService() { }

        private void getPicture(string id)
        {


            String _query = "exec [sp_api_getPicture] " + id;
            //BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            while (DRConexion.Read())
            {

                picture = new Picture
                {
                    id = int.Parse(DRConexion.GetDecimal(0).ToString()),
                    item_id = DRConexion.GetString(1),
                    num_photo = DRConexion.GetInt32(2),
                    url_general = DRConexion.GetString(3),
                    path_general = DRConexion.GetString(4)
                };

            }
            conexion.Desconectar();
        }

        private void getPictures(string item_id)
        {

            pictures.Clear();
            String _query = "exec [sp_api_getPictures]  '" + item_id + "'";
            //BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            while (DRConexion.Read())
            {

                pictures.Add(new Picture
                {
                    id = int.Parse(DRConexion.GetDecimal(0).ToString()),
                    item_id = DRConexion.GetString(1),
                    num_photo = DRConexion.GetInt32(2),
                    url_general = DRConexion.GetString(3),
                    path_general = DRConexion.GetString(4)
                });

            }
            conexion.Desconectar();
        }

        private void addPicture(string item_id, Picture newPicture)
        {

            //pictures.Clear();
            String _query = "exec [sp_api_newPicture]  '" + newPicture.item_id + "'," + newPicture.num_photo + ",'" + newPicture.url_general + "','"+newPicture.path_general+"'";
            //BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            while (DRConexion.Read())
            {

                picture = new Picture
                {
                    id = int.Parse(DRConexion.GetDecimal(0).ToString()),
                    item_id = DRConexion.GetString(1),
                    num_photo = DRConexion.GetInt32(2),
                    url_general = DRConexion.GetString(3)
                };

            }
            conexion.Desconectar();
        }

        private bool deletePicture(string item_id, int num_phot)
        {
            bool result =  false;
            String _query = "exec [sp_api_delPicture] '" + item_id + "'," + num_phot;
            //BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            try
            {
                DbDataReader DRConexion = conexion.EjecutarConsulta();
                result = true;
            }
            catch
            {
                result = false;
            }
            conexion.Desconectar();

            return result;
        }

        public Picture Get(string id)
        {
            getPicture(id);
            return picture;
        }
        public Picture Add(string id, Picture newPicture)
        {
            addPicture(id, newPicture);
            return picture;
        }

        public IEnumerable<Picture> GetAll(string item_id)
        {
            getPictures(item_id);
            return pictures;
        }

        public bool Remove(string id, int num_photo)
        {
            if (deletePicture(id, num_photo))
            {
                return true;
            }
            else{
                return false;
            }
        }
    }
}