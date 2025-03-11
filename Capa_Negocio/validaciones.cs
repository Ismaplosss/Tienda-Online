using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Capa_Negocio
{
    public class Validaciones
    {
        // Método que verifica si el usuario está autenticado
        public bool VerificarAcceso(HttpRequestBase request, HttpSessionStateBase session)
        {
            HttpCookie authCookie = request.Cookies["AuthCookie"];
            if (authCookie != null)
            {   
                string cookieValue = session["Correo"].ToString() + DateTime.Today.ToString("MM/dd/yyyy");
                string hashedCookieValue = SHA256Hash(cookieValue);

                // Comparar el hash de la cookie con el valor esperado
                if (hashedCookieValue == ObtenerHashGuardado(request))
                {
                    if (session["Authenticated"] != null && (bool)session["Authenticated"])
                    {
                        return true; 
                    }
                }
            }

            return false;  
        }
        private string ObtenerHashGuardado(HttpRequestBase request)
        {
            HttpCookie authCookie = request.Cookies["AuthCookie"];
            if (authCookie != null)
            {
                return authCookie.Value; 
            }
            return null;
        }
        public string SHA256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                foreach (byte byteValue in bytes)
                {
                    builder.Append(byteValue.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }



}
