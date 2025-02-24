using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public Cliente Ocliente { get; set; }
        public int Total_Producto { get; set; }
        public decimal Monto_Total { get; set; }
        public string Contacto { get; set; }
        public string IdDistrito { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Id_Transaccion { get; set; }
        public string Fecha_Registro { get; set;  }


    }
}
