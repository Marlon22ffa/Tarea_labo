using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DatosLayer
{
    public class DataBase
    {
        // Propiedad que devuelve la cadena de conexión que usaremos para conectar con la base de datos.
        public static string ConnectionString
        {
            get
            {
                // Agarramos la cadena de conexión desde la configuración del proyecto.
                string CadenaConexion = ConfigurationManager
                    .ConnectionStrings["NWConnection"]
                    .ConnectionString;

                // Usamos un ayudante para construir la cadena de conexión de una manera más flexible.
                SqlConnectionStringBuilder conexionBuilder =
                    new SqlConnectionStringBuilder(CadenaConexion);

                // Si tenemos un nombre de aplicación definido, lo ponemos en la cadena.
                conexionBuilder.ApplicationName =
                    ApplicationName ?? conexionBuilder.ApplicationName;

                // Configuramos el tiempo de espera si se ha definido; si no, dejamos el valor que ya tiene.
                conexionBuilder.ConnectTimeout = (ConnectionTimeout > 0)
                    ? ConnectionTimeout : conexionBuilder.ConnectTimeout;

                // Devolvemos la cadena de conexión ya configurada y lista para usarse.
                return conexionBuilder.ToString();
            }
        }

        // Propiedad para definir el tiempo de espera (timeout) de la conexión. Lo podemos ajustar si necesitamos más o menos tiempo.
        public static int ConnectionTimeout { get; set; }

        // Aquí podemos establecer el nombre de la aplicación que va a usar la base de datos.
        public static string ApplicationName { get; set; }

        // Este método abre una conexión a la base de datos y la devuelve para que la usemos.
        public static SqlConnection GetSqlConnection()
        {
            // Creamos una nueva conexión usando la cadena de conexión que configuramos arriba.
            SqlConnection conexion = new SqlConnection(ConnectionString);

            // Abrimos la conexión. ¡Importante no olvidarse de cerrarla después!
            conexion.Open();

            // Devolvemos la conexión ya abierta y lista para ser usada en nuestras consultas.
            return conexion;
        }
    }
}
