using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Base_Datos;
using System.Data.Common;

namespace api_electronia_articulos.Models
{
    public class ListaRepository:IListaRepositorio
    {
        private List<Lista> listado = new List<Lista>();

       


        public IEnumerable<Lista> GetAll(string type)
        {

            if (string.IsNullOrEmpty(type)) { type = "estados"; }

            generaCategoria(type);
            return listado;
        }

        private void generaCategoria(string type)
        {
            listado.Clear();

            String _query = "exec CAT_sp_listados '" + type + "' ";

            BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            string _id;
            while (DRConexion.Read())
            {


                try { _id = DRConexion.GetInt32(0).ToString(); }
                catch { _id = DRConexion.GetDecimal(0).ToString(); }
                listado.Add(new Lista
                {
                    id = _id,
                    valor = DRConexion.GetString(1),

                });

            }
            conexion.Desconectar();
        }

  
    }
}