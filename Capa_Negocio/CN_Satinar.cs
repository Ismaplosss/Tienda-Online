using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Capa_Negocio
{
   public class CN_Satinar
    {

        public string SatinarInput(string texto)
        {

            return HttpUtility.HtmlEncode(texto);
        }



    }
}
