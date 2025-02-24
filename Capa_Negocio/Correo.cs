using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace Capa_Negocio
{
   public  class Correo
    {



        public static bool Enviar_Correo(string correo,string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {

                MailMessage mail = new MailMessage();

                mail.To.Add(correo);
                mail.From = new MailAddress("proyectos.mails.31@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;


                var smtp = new SmtpClient()
                {

                    Credentials = new NetworkCredential("proyectos.mails.31@gmail.com", "jpnzvpzoffddmwug"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl =true



                };

                smtp.Send(mail);
                resultado = true;
              






            }
            catch (Exception)
            {
                resultado = false;

            }
            return resultado;
        }
             

    }
}
