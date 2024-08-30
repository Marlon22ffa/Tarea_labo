using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    public class CustomerRepository
    {

        public List<Customers> ObtenerTodos()
        {
            // Aquí estamos abriendo una conexión con la base de datos.
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Vamos a construir la consulta SQL para obtener todos los clientes.
                String selectFrom = "";
                selectFrom += "SELECT [CustomerID] " + "\n";  // Seleccionamos el ID del cliente.
                selectFrom += "      ,[CompanyName] " + "\n"; // Seleccionamos el nombre de la empresa.
                selectFrom += "      ,[ContactName] " + "\n"; // Seleccionamos el nombre del contacto.
                selectFrom += "      ,[ContactTitle] " + "\n"; // Seleccionamos el título del contacto.
                selectFrom += "      ,[Address] " + "\n"; // Seleccionamos la dirección.
                selectFrom += "      ,[City] " + "\n"; // Seleccionamos la ciudad.
                selectFrom += "      ,[Region] " + "\n"; // Seleccionamos la región.
                selectFrom += "      ,[PostalCode] " + "\n"; // Seleccionamos el código postal.
                selectFrom += "      ,[Country] " + "\n"; // Seleccionamos el país.
                selectFrom += "      ,[Phone] " + "\n"; // Seleccionamos el teléfono.
                selectFrom += "      ,[Fax] " + "\n"; // Seleccionamos el fax.
                selectFrom += "  FROM [dbo].[Customers]"; // Todo esto lo estamos sacando de la tabla 'Customers'.

                // Ejecutamos el comando SQL y obtenemos los resultados.
                using (SqlCommand comando = new SqlCommand(selectFrom, conexion))
                {
                    SqlDataReader reader = comando.ExecuteReader(); // Aquí estamos leyendo los datos de la consulta.
                    List<Customers> Customers = new List<Customers>(); // Creamos una lista para guardar todos los clientes.

                    while (reader.Read())
                    {
                        // Vamos leyendo cada cliente y lo agregamos a la lista.
                        var customers = LeerDelDataReader(reader);
                        Customers.Add(customers);
                    }
                    // Finalmente, devolvemos la lista de clientes.
                    return Customers;
                }
            }
        }

        public Customers ObtenerPorID(string id)
        {
            // Aquí abrimos otra conexión con la base de datos.
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Construimos la consulta SQL para obtener un cliente específico por su ID.
                String selectForID = "";
                selectForID += "SELECT [CustomerID] " + "\n";
                selectForID += "      ,[CompanyName] " + "\n";
                selectForID += "      ,[ContactName] " + "\n";
                selectForID += "      ,[ContactTitle] " + "\n";
                selectForID += "      ,[Address] " + "\n";
                selectForID += "      ,[City] " + "\n";
                selectForID += "      ,[Region] " + "\n";
                selectForID += "      ,[PostalCode] " + "\n";
                selectForID += "      ,[Country] " + "\n";
                selectForID += "      ,[Phone] " + "\n";
                selectForID += "      ,[Fax] " + "\n";
                selectForID += "  FROM [dbo].[Customers] " + "\n";
                selectForID += $"  WHERE CustomerID = @customerId"; // Filtro por el ID del cliente que nos pasaron.

                // Ejecutamos la consulta.
                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                {
                    // Añadimos el parámetro del ID del cliente a la consulta.
                    comando.Parameters.AddWithValue("customerId", id);

                    var reader = comando.ExecuteReader(); // Leemos el resultado.
                    Customers customers = null;

                    // Si encontramos un cliente con ese ID, lo leemos.
                    if (reader.Read())
                    {
                        customers = LeerDelDataReader(reader);
                    }
                    // Devolvemos el cliente encontrado (o null si no lo encontramos).
                    return customers;
                }
            }
        }

        public Customers LeerDelDataReader(SqlDataReader reader)
        {
            // Aquí vamos a convertir lo que leemos del DataReader en un objeto Customers.
            Customers customers = new Customers();
            customers.CustomerID = reader["CustomerID"] == DBNull.Value ? " " : (String)reader["CustomerID"];
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];
            // Devolvemos el cliente ya mapeado.
            return customers;
        }

        // Aquí estamos insertando un nuevo cliente en la base de datos.
        public int InsertarCliente(Customers customer)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Construimos la consulta SQL para insertar un nuevo cliente.
                String insertInto = "";
                insertInto += "INSERT INTO [dbo].[Customers] " + "\n";
                insertInto += "           ([CustomerID] " + "\n";
                insertInto += "           ,[CompanyName] " + "\n";
                insertInto += "           ,[ContactName] " + "\n";
                insertInto += "           ,[ContactTitle] " + "\n";
                insertInto += "           ,[Address] " + "\n";
                insertInto += "           ,[City]) " + "\n";
                insertInto += "     VALUES " + "\n";
                insertInto += "           (@CustomerID " + "\n";
                insertInto += "           ,@CompanyName " + "\n";
                insertInto += "           ,@ContactName " + "\n";
                insertInto += "           ,@ContactTitle " + "\n";
                insertInto += "           ,@Address " + "\n";
                insertInto += "           ,@City)";

                // Ejecutamos la consulta para insertar el cliente.
                using (var comando = new SqlCommand(insertInto, conexion))
                {
                    int insertados = parametrosCliente(customer, comando); // Llamamos a un método para añadir los parámetros.
                    return insertados; // Devolvemos cuántos registros se insertaron (debería ser 1).
                }
            }
        }

        // Este método se encarga de actualizar la información de un cliente existente.
        public int ActualizarCliente(Customers customer)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Construimos la consulta SQL para actualizar un cliente por su ID.
                String ActualizarCustomerPorID = "";
                ActualizarCustomerPorID += "UPDATE [dbo].[Customers] " + "\n";
                ActualizarCustomerPorID += "   SET [CustomerID] = @CustomerID " + "\n";
                ActualizarCustomerPorID += "      ,[CompanyName] = @CompanyName " + "\n";
                ActualizarCustomerPorID += "      ,[ContactName] = @ContactName " + "\n";
                ActualizarCustomerPorID += "      ,[ContactTitle] = @ContactTitle " + "\n";
                ActualizarCustomerPorID += "      ,[Address] = @Address " + "\n";
                ActualizarCustomerPorID += "      ,[City] = @City " + "\n";
                ActualizarCustomerPorID += " WHERE CustomerID= @CustomerID"; // Filtramos por el ID del cliente.

                // Ejecutamos la consulta.
                using (var comando = new SqlCommand(ActualizarCustomerPorID, conexion))
                {
                    int actualizados = parametrosCliente(customer, comando); // Añadimos los parámetros.
                    return actualizados; // Devolvemos cuántos registros se actualizaron (debería ser 1).
                }
            }
        }

        // Método auxiliar que añade los parámetros del cliente a un comando SQL.
        public int parametrosCliente(Customers customer, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("CustomerID", customer.CustomerID);
            comando.Parameters.AddWithValue("CompanyName", customer.CompanyName);
            comando.Parameters.AddWithValue("ContactName", customer.ContactName);
            comando.Parameters.AddWithValue("ContactTitle", customer.ContactName);
            comando.Parameters.AddWithValue("Address", customer.Address);
            comando.Parameters.AddWithValue("City", customer.City);
            var insertados = comando.ExecuteNonQuery(); // Ejecuta la consulta.
            return insertados; // Devolvemos cuántos registros fueron afectados.
        }

        // Finalmente, aquí estamos eliminando un cliente por su ID.
        public int EliminarCliente(string id)
        {
            // Conecta a la base de datos usando la configuración definida
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Prepara el comando SQL para borrar un cliente
                String EliminarCliente = "";
                EliminarCliente = EliminarCliente + "DELETE FROM [dbo].[Customers] " + "\n"; // Dices que quieres borrar de la tabla Customers
                EliminarCliente = EliminarCliente + "      WHERE CustomerID = @CustomerID"; // Borras el cliente con el ID que pasaste

                // Configura el comando con la consulta y la conexión
                using (SqlCommand comando = new SqlCommand(EliminarCliente, conexion))
                {
                    // Añades el ID del cliente al comando
                    comando.Parameters.AddWithValue("@CustomerID", id);

                    // Ejecutas el comando y obtienes cuántas filas fueron borradas
                    int elimindos = comando.ExecuteNonQuery();

                    // Devuelves el número de filas eliminadas
                    return elimindos;
                }
            }
        }

    }
}
