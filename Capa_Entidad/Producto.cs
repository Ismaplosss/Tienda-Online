using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{


   public class Producto
    {
        public  int IdProducto { get; set; }
        public string  Nombre { get; set; }
        public string Descripcion { get; set;  }
        public Marca OMarca { get; set; }
        public categoria OCategoria{ get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Ruta_Imagen { get; set; }
        public string Nombre_Imagen { get; set; }
        public bool Activo { get; set; }
        public string Fecha_Registro { get; set; }


    }















}
