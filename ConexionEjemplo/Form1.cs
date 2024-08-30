using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DatosLayer;
using System.Net;
using System.Reflection;


namespace ConexionEjemplo
{
    public partial class Form1 : Form
    {
        // Aquí creamos una instancia del repositorio de clientes
        CustomerRepository customerRepository = new CustomerRepository();

        // Constructor del formulario
        public Form1()
        {
            InitializeComponent(); // Inicia el formulario y sus componentes
        }

        // Cuando le damos click al botón "Cargar"
        private void btnCargar_Click(object sender, EventArgs e)
        {
            // Obtenemos la lista de clientes desde el repositorio
            var Customers = customerRepository.ObtenerTodos();
            // Mostramos esos clientes en el DataGrid
            dataGrid.DataSource = Customers;
        }

        // Este método se activa cuando se escribe algo en el TextBox1
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Aquí podrías filtrar la lista de clientes mientras escribes en el TextBox, pero está comentado
            // var filtro = Customers.FindAll( X => X.CompanyName.StartsWith(tbFiltro.Text));
            // dataGrid.DataSource = filtro;
        }

        // Este método se ejecuta cuando se carga el formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            // Código comentado que parece que estaba configurando la conexión a la base de datos
            /*  
            DatosLayer.DataBase.ApplicationName = "Programacion 2 ejemplo";
            DatosLayer.DataBase.ConnectionTimeout = 30;

            string cadenaConexion = DatosLayer.DataBase.ConnectionString;
            var conxion = DatosLayer.DataBase.GetSqlConnection();
            */
        }

        // Cuando se hace clic en el botón "Buscar"
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtenemos un cliente por su ID
            var cliente = customerRepository.ObtenerPorID(txtBuscar.Text);
            // Llenamos los TextBoxes con los datos del cliente
            tboxCustomerID.Text = cliente.CustomerID;
            tboxCompanyName.Text = cliente.CompanyName;
            tboxContacName.Text = cliente.ContactName;
            tboxContactTitle.Text = cliente.ContactTitle;
            tboxAddress.Text = cliente.Address;
            tboxCity.Text = cliente.City;
        }

        // Este evento está vacío, pero podría usarse para hacer algo cuando se hace clic en la etiqueta 4
        private void label4_Click(object sender, EventArgs e)
        {

        }

        // Cuando se hace clic en el botón "Insertar"
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            var resultado = 0; // Inicializamos el resultado

            // Creamos un nuevo cliente con los datos de los TextBoxes
            var nuevoCliente = ObtenerNuevoCliente();

            // Validamos que no haya campos nulos
            if (validarCampoNull(nuevoCliente) == false)
            {
                // Si no hay campos nulos, insertamos el cliente y mostramos un mensaje de confirmación
                resultado = customerRepository.InsertarCliente(nuevoCliente);
                MessageBox.Show("Guardado" + " Filas modificadas = " + resultado);
            }
            else
            {
                // Si hay campos nulos, mostramos un mensaje pidiendo que se completen
                MessageBox.Show("Debe completar los campos por favor");
            }
        }

        // Este método revisa si algún campo del objeto está vacío
        public Boolean validarCampoNull(Object objeto)
        {
            foreach (PropertyInfo property in objeto.GetType().GetProperties())
            {
                object value = property.GetValue(objeto, null);
                if ((string)value == "")
                {
                    return true; // Si encuentra un campo vacío, devuelve true
                }
            }
            return false; // Si todos los campos están llenos, devuelve false
        }

        // Este evento está vacío también, pero podría hacer algo al hacer clic en la etiqueta 5
        private void label5_Click(object sender, EventArgs e)
        {

        }

        // Cuando se hace clic en el botón "Modificar"
        private void btModificar_Click(object sender, EventArgs e)
        {
            // Obtenemos los datos del cliente que queremos actualizar
            var actualizarCliente = ObtenerNuevoCliente();
            // Llamamos al método para actualizar el cliente en el repositorio
            int actualizadas = customerRepository.ActualizarCliente(actualizarCliente);
            // Mostramos cuántas filas fueron actualizadas
            MessageBox.Show($"Filas actualizadas = {actualizadas}");
        }

        // Este método crea un nuevo objeto de cliente con los datos de los TextBoxes
        private Customers ObtenerNuevoCliente()
        {
            var nuevoCliente = new Customers
            {
                CustomerID = tboxCustomerID.Text,
                CompanyName = tboxCompanyName.Text,
                ContactName = tboxContacName.Text,
                ContactTitle = tboxContactTitle.Text,
                Address = tboxAddress.Text,
                City = tboxCity.Text
            };

            return nuevoCliente; // Retorna el nuevo cliente
        }

        // Cuando se hace clic en el botón "Eliminar"
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Eliminamos el cliente con el ID dado y mostramos cuántas filas fueron eliminadas
            int eliminadas = customerRepository.EliminarCliente(tboxCustomerID.Text);
            MessageBox.Show("Filas eliminadas = " + eliminadas);
        }
    }


    /*private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

        
    }*/
}
