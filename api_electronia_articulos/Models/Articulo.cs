using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductStore.Models
{
    public class Articulo
    {
        /*public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }*/


        public int articuloID { get; set; }
        public string identificador { get; set; }
        public int tipoAnuncioID { get; set; }
        public string tipoAnuncio { get; set; }
        public decimal marcaID { get; set; }
        public decimal modeloID { get; set; }
        public int aparatoID { get; set; }
        public int clasificacionID { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string aparato { get; set; }
        public string clasificacion { get; set; }
        public string descripcion { get; set; }
        public int estadoID { get; set; }
        public string estado { get; set; }
        public int numFotos { get; set; }
        public string fotoPrincipal { get; set; }
        public decimal precio { get; set; }
        public string tipoMoneda { get; set; }
        public string color { get; set; }
        public string archivoVideo { get; set; }
        public string caracteristicas { get; set; }
        public decimal personaID { get; set; }
        public decimal tiendaID { get; set; }
        public string persona { get; set; }
        public string tienda { get; set; }
        public string telefonos { get; set; }
        public string direccion { get; set; }
        public int statusAnuncioID { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaModificacion { get; set; }
        public string financiamiento { get; set; }

    }
}