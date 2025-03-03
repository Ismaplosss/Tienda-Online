using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Negocio;

namespace Capa_Presentacion_Tienda.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }
        public ActionResult Resumen()
        {
            return View();
        }


        [HttpGet]
        public JsonResult Listar_Usuarios()
        {
            List<Usuario> OLista = new List<Usuario>();

            OLista = new Cn_Usuario().Listar();
            return Json( new { data = OLista }, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public JsonResult Guardar_Usuarios(Usuario objeto)
        {
            object resultado = null;
            string mensaje = string.Empty;

            try
            {
                if (objeto == null)
                {
                    return Json(new { resultado = 0, mensaje = "Datos inválidos" });
                }

                Cn_Usuario usuarioService = new Cn_Usuario();

                if (objeto.IdUsuario == 0)
                    resultado = usuarioService.Registrar_User(objeto, out mensaje);
                else
                    resultado = usuarioService.Actualizar_User(objeto, out mensaje);

                return Json(new { resultado, mensaje });
            }
            catch (Exception ex)
            {
                return Json(new { resultado = 0, mensaje = "Error en el servidor: " + ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Desactivar_User(Usuario objeto)
        {
            object resultado = null;
            string mensaje = string.Empty;


            try
            {

                if (objeto == null)
                {
                    return Json(new { resultado = 0, mensaje = "Datos inválidos" });

                }
                else
                {
                    Cn_Usuario usuarioService = new Cn_Usuario();

                    resultado = usuarioService.Desactivar_User(objeto, out mensaje);

                }

                return Json(new { resultado, mensaje });



            }
            catch (Exception ex)
            {

                return Json(new { resultado = 0, mensaje = "Error en el servidor: " + ex.Message });

            }
        }

    }
}