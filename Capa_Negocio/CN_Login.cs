using Capa_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
   public class CN_Login
    {
        CD_Login _Login = new CD_Login();
        CN_Satinar satinar = new CN_Satinar();
        cifrado cifrado = new cifrado();

        public string Acceso_al_panel(string correo, string hash , out string Resumen )
        {
            Resumen = string.Empty;

            if(string.IsNullOrEmpty(correo) || string.IsNullOrWhiteSpace(correo))
            {
                Resumen = "Error Falta el correo";
            }
            if(string.IsNullOrEmpty(hash) || string.IsNullOrWhiteSpace(hash))
            {
                Resumen = "Error falta la contraseña";
            }

            if (string.IsNullOrEmpty(Resumen))
            {
                return _Login.AccesoAdministrador(satinar.SatinarInput(correo),cifrado.Cifrado_Password(hash)  , out Resumen);
            }
            else
            {
                return $"Error encontrado: ,{Resumen}";
            }



           
        }


    }
}
