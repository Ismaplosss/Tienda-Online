using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentasion_Tienda.Controllers
{
    public class AccesoController : Controller
    {
        CN_Cliente_Login login = new CN_Cliente_Login();
        // GET: autenticador
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult CorreoRestablecimiento()
        {
            return View();
        }

        public ActionResult Registrar_Cliente()
        {
            return View();
        }

        public ActionResult New_Pass(string token)
        {
            System.Diagnostics.Debug.WriteLine("Token recibido: " + token);

            if (string.IsNullOrEmpty(token) || !login.VerificarTokens(token))
            {
                System.Diagnostics.Debug.WriteLine("Intento de acceso con token inválido o expirado: " + token);
                ViewBag.Mensaje = "El enlace ha expirado o es inválido.";
                return View("TokenExpirado"); // Vista de error
            }

            // Guardar en sesión en lugar de pasarlo directamente a la vista
            Session["ResetToken"] = token;
            return View();
        }


        [HttpPost]
        public JsonResult Acceso_Sistema(string correo, string hash)
        {

            object resultado = 0;
            string mensaje = string.Empty;

            try
            {

                if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(hash))
                {
                    return Json(new { resultado = 0, mensaje = "Correo y contraseña son obligatorios." });
                }


               
                resultado = login.AccesoCliente(correo, hash, out mensaje);


                if (resultado.Equals("Login"))
                {
                    return Json(new { resultado = 1, mensaje = "Ingreso exitoso" });
                }
                else if (resultado.Equals("Bloquear")   )
                {
                    return Json(new { resultado = 0, mensaje= "Correo electronico o contraseña incorrectos" });
                }
                else
                {
                    return Json(new { resultado = 0, mensaje });
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones y respuesta al cliente
                return Json(new { resultado = 0, mensaje = $"Error en el servidor, intente más tarde: {ex.Message}" });
            }
        }


        [HttpPost]
        public JsonResult CambiarContra(string token, string contra, string contra2)
        {
            // Validación de datos
            if (string.IsNullOrEmpty(contra) || string.IsNullOrEmpty(contra2) || !contra.Equals(contra2))
            {
                return Json(new { resultado = 0, mensaje = "Las contraseñas deben ser iguales y no estar vacías." }, JsonRequestBehavior.AllowGet);
            }

            // Intentar cambiar la contraseña
            string mensaje;
            bool resultado = login.CambiarContra(token, contra, out mensaje);

            // Devolver la respuesta según el resultado
            return Json(new { resultado = resultado ? 1 : 0, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EnviarCorreo(string Correo)
        {
            try
            {
                if (string.IsNullOrEmpty(Correo))
                {
                    return Json(new { resultado = 0, Mensaje = "Es necesario escribir un correo" }, JsonRequestBehavior.AllowGet);
                }

                if (!login.VerificarCorreo(Correo)) // Si el correo NO existe
                {
                    return Json(new { resultado = 0, Mensaje = "El correo no existe en los registros" }, JsonRequestBehavior.AllowGet);
                }

                // Si el correo existe, generamos el token y enviamos el mensaje de éxito
                login.GenerarToken(Correo);
                return Json(new { resultado = 1, Mensaje = "Correo enviado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { resultado = 0, Mensaje = "Ocurrió un error al procesar la solicitud: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Registrar_Cliente_DB(Cliente cliente )
        {
            object resultado = false;
            string Mensaje = string.Empty;

            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(cliente.Correo))
                {
                    Mensaje = "Ingrese un correo correcto.";
                    return Json(new { resultado = 0, Mensaje });
                }

                if (string.IsNullOrWhiteSpace(cliente.Nombre))
                {
                    Mensaje = "Ingrese un nombre correcto.";
                    return Json(new { resultado = 0, Mensaje });
                }

                if (string.IsNullOrWhiteSpace(cliente.Apellidos))
                {
                    Mensaje = "Ingrese sus apellidos correctos.";
                    return Json(new { resultado = 0, Mensaje });
                }

               resultado = login.RegistrarCliente(cliente, out Mensaje);

                if ((bool)resultado)
                {
                    return Json(new { resultado=1 , Mensaje = "Cliente registrado correctamente." });
                }
                else
                {
                    return Json(new { resultado = 0, Mensaje = string.IsNullOrEmpty(Mensaje) ? "Error al registrar el cliente." : Mensaje });
                }
            }
            catch (Exception ex)
            {
                // Log de la excepción (si se usa algún sistema de logs)
                // Logger.LogError(ex, "Error al registrar cliente.");

                return Json(new { resultado = 0, Mensaje = "Ocurrió un error en el servidor. Por favor, inténtalo nuevamente." });
            }
        }




    }
}