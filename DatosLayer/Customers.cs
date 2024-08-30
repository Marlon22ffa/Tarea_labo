using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    public class Customers
    {
        // Esta es la clase que representa a un cliente. 
        // Cada cliente tiene varios atributos que vamos a definir aquí como propiedades.

        public string CustomerID { get; set; }  // Aquí guardamos el ID del cliente.
        public string CompanyName { get; set; } // Este es el nombre de la empresa.
        public string ContactName { get; set; } // Nombre de la persona de contacto en la empresa.
        public string ContactTitle { get; set; } // El título del contacto, por ejemplo, 'Gerente'.
        public string Address { get; set; } // Dirección física de la empresa.
        public string City { get; set; } // Ciudad donde se encuentra la empresa.
        public string Region { get; set; } // Región o estado donde está la empresa.
        public string PostalCode { get; set; } // Código postal de la dirección.
        public string Country { get; set; } // País de la empresa.
        public string Phone { get; set; } // Número de teléfono del contacto.
        public string Fax { get; set; } // Número de fax, si es que tienen uno.
    }
}
