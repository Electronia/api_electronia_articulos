using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.Common;
using System.Globalization;
/// <summary>
/// Summary description for BaseDatos
/// </summary>
namespace Base_Datos
{
    public class BaseDatos
    {

        private DbConnection conexion = null;
        private DbCommand comando = null;
        private DbTransaction transaccion = null;
        private string cadenaConexion;
        private string provider;
        /// <summary>
        /// Cadena de conexi�n
        /// </summary>
        /// <param name="cadena">Cadena de conexi�n</param>
        public string cadena
        {
            set
            {
                this.cadenaConexion = value;
            }
        }
        public string commando {
            get {return this.comando.CommandText; }
        }
        private static DbProviderFactory factory = null;

        /// <summary>
        /// Crea una instancia del acceso a la base de datos.
        /// </summary>
        /// <param name="proveedor">Cadena del nombre del proveedor p.e. System.Data.Odbc</param>
      
      /*  public BaseDatos(string proveedor)
        {
            this.provider = proveedor;
            Configurar();

        }
       * */
        /// <summary>
        /// Crea una instancia del acceso a la base de datos.
        /// </summary>
        /// <param name="proveedor">Cadena del nombre del proveedor p.e. System.Data.Odbc</param>
        /// <param name="cadena">Cadena de la conexi�n a la base de datos</param>
        public BaseDatos(string proveedor, string cadena)
        {
            this.provider = proveedor;
            Configurar();
            this.cadena = cadena;
        }
        /// <summary>
        /// Crea una instancia del acceso a base de datos, tomando la cadena del web.config y el proveedor por default es SqlClient
        /// </summary>
        public BaseDatos() 
        {
            this.cadena = ConfigurationSettings.AppSettings.Get("SQL").ToString();
            this.provider = ConfigurationSettings.AppSettings.Get("prov").ToString();
            Configurar();
        }
        public BaseDatos(string Conn)
        {
            this.cadena = ConfigurationSettings.AppSettings.Get(Conn).ToString();
            this.provider = ConfigurationSettings.AppSettings.Get("prov").ToString();
            Configurar();
        }
        public DataTable getschema()
        {
            string[] restrictions = new string[4];
            restrictions[3] = "Table";
            try
            {
                if (this.provider == "System.Data.SqlClient")
                    return conexion.GetSchema("Tables");
                else
                    return conexion.GetSchema("Tables", restrictions);
            }
            catch { return null; }
        }
        /// <summary>
        /// Configura el acceso a la base de datos para su utilizaci�n.
        /// </summary>
        /// <exception cref="BaseDatosException">Si existe un error al cargar la configuraci�n.</exception>
        private void Configurar()
        {
            try
            {

                BaseDatos.factory = DbProviderFactories.GetFactory(this.provider);
            }
            catch (ConfigurationException ex)
            {
                throw new BaseDatosException("Error al cargar la configuraci�n del acceso a datos.", ex);
            }
        }

        /// <summary>
        /// Permite desconectarse de la base de datos.
        /// </summary>/
        public void Desconectar()
        {
            if (this.conexion.State.Equals(ConnectionState.Open))
            {
                this.conexion.Close();
            }
        }

        /// <summary>
        /// Se concecta con la base de datos.
        /// </summary>
        /// <exception cref="BaseDatosException">Si existe un error al conectarse.</exception>*/
        public bool Conectar()
        {
            if (this.conexion != null && !this.conexion.State.Equals(ConnectionState.Closed))
            {
                throw new BaseDatosException("La conexi�n ya se encuentra abierta.");
            }
            try
            {
                if (this.conexion == null)
                {
                    this.conexion = factory.CreateConnection();
                    this.conexion.ConnectionString = cadenaConexion;
                }
                this.conexion.Open();
                return true;
            }
            catch (DataException ex)
            {
                throw new BaseDatosException("Error al conectarse a la base de datos.", ex);
                return false;
            }
        }

        /// <summary>
        /// Crea un comando en base a una sentencia SQL.
        /// Ejemplo:
        /// <code>SELECT * FROM Tabla WHERE campo1=@campo1, campo2=@campo2</code>
        /// Guarda el comando para el seteo de par�metros y la posterior ejecuci�n.
        /// </summary>
        /// <param name="sentenciaSQL">La sentencia SQL con el formato: SENTENCIA [param = @param,]</param>*/
        public void CrearComando(string sentenciaSQL)
        {
            this.comando = factory.CreateCommand();
            this.comando.Connection = this.conexion;
            this.comando.CommandType = CommandType.Text;
            this.comando.CommandText = sentenciaSQL;
            if (this.transaccion != null)
            {
                this.comando.Transaction = this.transaccion;
            }
        }

        /// <summary>
        /// Setea un par�metro como nulo del comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par�metro cuyo valor ser� nulo.</param>*/
        public void AsignarParametroNulo(string nombre)
        {
            AsignarParametro(nombre, "", "NULL");
        }

        /// <summary>
        /// Asigna un par�metro de tipo cadena al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par�metro.</param>
        /// <param name="valor">El valor del par�metro.</param>*/
        public void AsignarParametroCadena(string nombre, string valor)
        {
            AsignarParametro(nombre, "'", valor);
        }

        /// <summary>
        /// Asigna un par�metro de tipo entero al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par�metro.</param>
        /// <param name="valor">El valor del par�metro.</param*>*/
        public void AsignarParametroEntero(string nombre, int valor)
        {
            AsignarParametro(nombre, "", valor.ToString());
        }

        /// <summary>
        /// Asigna un par�metro al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par�metro.</param>
        /// <param name="separador">El separador que ser� agregado al valor del par�metro.</param>
        /// <param name="valor">El valor del par�metro.</param>*/
        private void AsignarParametro(string nombre, string separador, string valor)
        {
            int indice = this.comando.CommandText.IndexOf(nombre);
            string prefijo = this.comando.CommandText.Substring(0, indice);
            string sufijo = this.comando.CommandText.Substring(indice + nombre.Length);
            this.comando.CommandText = prefijo + separador + valor + separador + sufijo;
        }

        /// <summary>
        /// Asigna un par�metro de tipo fecha al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par�metro.</param>
        /// <param name="valor">El valor del par�metro.</param>*/
        public void AsignarParametroFecha(string nombre, DateTime valor)
        {
            IFormatProvider culture = new CultureInfo(ConfigurationSettings.AppSettings.Get("LangServer").ToString(), true);

       //     IFormatProvider culture = new CultureInfo("en-US", true);
     
            AsignarParametro(nombre, "'", valor.ToString(culture));
        }

        //// <summary>
        /// Ejecuta el comando creado y retorna el resultado de la consulta.
        /// </summary>
        /// <returns>El resultado de la consulta.</returns>
        /// <exception cref="BaseDatosException">Si ocurre un error al ejecutar el comando.</exception>*/
        public DbDataReader EjecutarConsulta()
        {
            return this.comando.ExecuteReader();
        }

        /// <summary>
        /// Ejecuta el comando creado y retorna un escalar.
        /// </summary>
        /// <returns>El escalar que es el resultado del comando.</returns>
        /// <exception cref="BaseDatosException">Si ocurre un error al ejecutar el comando.</exception>*/
        public int EjecutarEscalar()
        {
             int escalar=0;
           /* int escalar = 0;
            try
            {
                escalar = int.Parse(this.comando.ExecuteScalar().ToString());
            }
            catch (InvalidCastException ex)
            {
                throw new BaseDatosException("Error al ejecutar un escalar.", ex);
            }*/
         
            return escalar;
        }

        /// <summary>
        /// Ejecuta el comando creado.
        /// </summary>*/
        public void EjecutarComando()
        {
            this.comando.ExecuteNonQuery();
        }

        /// <summary>
        /// Comienza una transacci�n en base a la conexion abierta.
        /// Todo lo que se ejecute luego de esta ionvocaci�n estar� 
        /// dentro de una tranasacci�n.
        /// </summary>*/
        public void ComenzarTransaccion()
        {
            if (this.transaccion == null)
            {
                this.transaccion = this.conexion.BeginTransaction();
            }
        }

        /// <summary>
        /// Cancela la ejecuci�n de una transacci�n.
        /// Todo lo ejecutado entre �sta invocaci�n y su 
        /// correspondiente <c>ComenzarTransaccion</c> ser� perdido.
        /// </summary>*/
        public void CancelarTransaccion()
        {
            if (this.transaccion != null)
            {
                this.transaccion.Rollback();
            }
        }

        /// <summary>
        /// Confirma todo los comandos ejecutados entre el <c>ComanzarTransaccion</c>
        /// y �sta invocaci�n.
        /// </summary>*/
        public void ConfirmarTransaccion()
        {
            if (this.transaccion != null)
            {
                this.transaccion.Commit();
            }
        }

    }
    public class BaseDatosException : ApplicationException
    {

        /// <summary>
        /// Construye una instancia en base a un mensaje de error y la una excepci�n original.
        /// </summary>
        /// <param name="mensaje">El mensaje de error.</param>
        /// <param name="original">La excepci�n original.</param>
        public BaseDatosException(string mensaje, Exception original) : base(mensaje, original) { }

        /// <summary>
        /// Construye una instancia en base a un mensaje de error.
        /// </summary>
        /// <param name="mensaje">El mensaje de error.</param>
        public BaseDatosException(string mensaje) : base(mensaje) { }
    }
}
