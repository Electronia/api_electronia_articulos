using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Base_Datos;
using System.Data.Common;

namespace api_electronia_articulos.Models
{
    public class CategoriaRepository:ICategoriaRepositorio
    {
        private List<Categoria> categoria = new List<Categoria>();

        public IEnumerable<Categoria> GetAll()
        {
            generaCategoria("articulos", "0", "0");
            return categoria;
        }


        public IEnumerable<Categoria> GetAll(string type, string marcaID, string aparatoID)
        {

            if (string.IsNullOrEmpty(type)) { type = "articulos"; }
            marcaID = procesaVariable(marcaID);
            aparatoID = procesaVariable(aparatoID);

            generaCategoria(type, marcaID, aparatoID);
            return categoria;
        }

        private void generaCategoria(string type, string marcaID, string aparatoID)
        {
            categoria.Clear();

            String _query = "exec CAT_sp_categorias '" + type + "' ," + marcaID + "," + aparatoID;

            BaseDatos conexion = new BaseDatos("SQL");   //Creando objetos de la base de datos
            conexion.Conectar();
            conexion.CrearComando(_query); //Comando indica que quiero encontrar
            DbDataReader DRConexion = conexion.EjecutarConsulta();
            int _id;
            while (DRConexion.Read())
            {


                try { _id = DRConexion.GetInt32(0); }
                catch { _id = int.Parse(DRConexion.GetDecimal(0).ToString()); }
                categoria.Add(new Categoria
                {
                    id = _id,
                    nombre = DRConexion.GetString(1),

                });

            }
            conexion.Desconectar();
        }

        private string procesaVariable(string value)
        {
            if (string.IsNullOrEmpty(value)) { return "0"; } else { return value; }
        }
    }
}