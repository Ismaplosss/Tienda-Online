using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Capa_Datos
{
    public class Conexion
    {
        public static string Conecctions = ConfigurationManager.ConnectionStrings["Mercadito"].ToString();
        
    }
}
