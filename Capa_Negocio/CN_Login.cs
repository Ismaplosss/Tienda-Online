using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

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
                string temp= _Login.AccesoAdministrador(satinar.SatinarInput(correo), cifrado.Cifrado_Password(hash), out Resumen);

                if (Resumen.Length>70)
                {
                    GenerarToken(correo);
                }
                    return temp;
            }
            else
            {
                return $"Error encontrado: ,{Resumen}";
            }



           
        }

        public bool VerificarTokens(string token)
        {
            return _Login.VerificarTokens(token);
        }

        public bool CambiarContra(string token, string conttra, out string mensaje) 
        
        { 
            string temp= cifrado.Cifrado_Password(conttra);
            return _Login.CambiarContrasena(token, temp, out mensaje);
        
        
        }

        public string GenerarToken(string correo)
        {
            string tokenFull = "", saltRandom;

            if (VerificarCorreo(correo))
            {
                saltRandom = Guid.NewGuid().ToString().Replace("-", "").Substring(8);
                tokenFull = cifrado.Cifrado_Password(correo + saltRandom + DateTime.Now.ToString());

                // Almacenamos el token con una expiración de 5 minutos
                DateTime fechaExpiracion = DateTime.Now.AddMinutes(5);
                AlmacenarTokens(correo, tokenFull, fechaExpiracion);
                EnviarTocken(correo, tokenFull);
            }

            return tokenFull;
        }

        public bool VerificarCorreo(string correo)
        {
            return _Login.VerificarCorreo(correo);
        }

        private bool AlmacenarTokens(string correo, string token, DateTime fechaExpiracion)
        {
            return _Login.AlmacenarTokens(correo, token, fechaExpiracion);
        }

  

        public void EnviarTocken(string Correo, string token)
        {



            string asunto = "Restablecimiento de contraseña";
            string enlaceRecuperacion = $"https://localhost:44369/autenticador/New_Pass?token={token}";

            string mensajeHtml = $@"
            <!DOCTYPE html>
             <html lang='es'>
          <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Restablecimiento de contraseña</title>
            <link href='https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/css/bootstrap.min.css' rel='stylesheet'>
            <style>
                body {{
                    background-color: #f4f7fa;
                    padding: 20px;
                }}
                .email-container {{
                    background-color: white;
                    border-radius: 8px;
                    padding: 30px;
                    max-width: 600px;
                    margin: 0 auto;
                    box-shadow: 0 0 15px rgba(0,0,0,0.1);
                }}
                .btn-primary {{
                    background-color: #007bff;
                    border-color: #007bff;
                }}
                .footer {{
                    text-align: center;
                    font-size: 12px;
                    color: #aaa;
                    margin-top: 30px;
                }}
                .footer a {{
                    color: #007bff;
                    text-decoration: none;
                }}
            </style>
        </head>
        <body>
            <div class='email-container'>
                <h2 class='text-center'>Restablecer tu contraseña</h2>
                <br>
                <p>Recibimos una solicitud para restablecer la contraseña de tu cuenta. Haz clic en el botón de abajo para restablecerla.</p>
                <div class='text-center'>
                    <a href='{enlaceRecuperacion}' class='btn btn-primary btn-lg' role='button'>Restablecer contraseña</a>
                </div>
                <br>
                <p>Si no solicitaste un cambio de contraseña, por favor ignora este mensaje.</p>
                <p>Saludos,<br>El equipo de soporte</p>
                <div class='footer'>
                    <p>Si tienes problemas, contacta con nosotros en <a href='#'>soporte@desarrollo.com</a></p>
                </div>
            </div>
          </body>
          </html>";
            bool respuesta = EnviarCorreo(Correo, asunto, mensajeHtml);


        }


        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool confirmacion = Correo.Enviar_Correo(correo, asunto, mensaje);
            return confirmacion;
        }
    }
}
