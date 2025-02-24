using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
   public  class Detalle_Venta
    {
        public int IdDetalleVenta { get; set;  }
        public Venta OVenta { get; set;  }
        public Producto OProducto { get; set;  }
        public int Cantidad { get; set; }
        public decimal Total { get; set;  }
    }
}
