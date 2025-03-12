using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.WebSockets;

namespace Capa_Presentacion_Admin.Controllers
{
    public class autenticadorController : Controller
    {
        // GET: autenticador
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult New_Pass(string token)
        {
            if (string.IsNullOrEmpty(token) )//|| !_Login.VerificarToken(token))
            {
                ViewBag.Mensaje = "El enlace ha expirado o es inválido.";
                return View("TokenExpirado"); // Aquí podrías crear una vista de error.
            }

            ViewBag.Token = token; // Pasamos el token para que el usuario pueda enviarlo con la nueva contraseña.
            System.Diagnostics.Debug.WriteLine("Token recibido: " + token);

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

               
                CN_Login login = new CN_Login();
                resultado = login.Acceso_al_panel(correo, hash, out mensaje);

                
                if (resultado.Equals("Login"))
                {
                    return Json(new { resultado = 1, mensaje = "Ingreso exitoso" });
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

        


















    }
}