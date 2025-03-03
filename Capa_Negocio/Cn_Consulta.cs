using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
   public   class Cn_Consulta
    {
        CD_Consulta Consulta = new CD_Consulta();
        public Consulta Listar_panel()
        {
            return Consulta.ListarConsulta();
        }



    }
}
