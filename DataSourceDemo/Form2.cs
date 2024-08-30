using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSourceDemo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        // Cuando haces clic en el botón para guardar los cambios
        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Checa si todo está bien en el formulario
            this.Validate();
            // Termina cualquier edición que esté pendiente
            this.customersBindingSource.EndEdit();
            // Guarda todos los cambios en la base de datos
            this.tableAdapterManager.UpdateAll(this.northwindDataSet);
        }

        // Cuando se abre el Form2
        private void Form2_Load(object sender, EventArgs e)
        {
            // Aquí se carga la info de los clientes en el DataSet
            // Es como un "Cargar ahora" para los datos
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);
        }

        // Cuando haces clic en el TextBox del ID del cliente
        private void cajaTextoID_Click(object sender, EventArgs e)
        {
            // Este método está vacío por ahora, podrías añadir algo aquí si quieres
        }

        // Cuando presionas una tecla en el TextBox del ID del cliente
        private void cajaTextoID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si presionas la tecla Enter (código 13)
            if (e.KeyChar == (char)13)
            {
                // Busca el cliente en la lista con el ID que escribiste
                var index = customersBindingSource.Find("customerID", cajaTextoID.Text);
                // Si encuentra el cliente
                if (index > -1)
                {
                    // Mueve la posición en la lista al cliente encontrado
                    customersBindingSource.Position = index;
                    return;
                }
                else
                {
                    // Si no encuentra el cliente, muestra un mensaje
                    MessageBox.Show("Elemento no encontrado");
                }
            };
        }

    }
}
