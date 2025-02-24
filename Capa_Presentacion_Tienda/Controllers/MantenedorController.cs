using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capa_Presentacion_Tienda.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Marca()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }




        [HttpGet]
        public JsonResult Listar_Categoria()
        {
            try
            {
                List<categoria> categorias = new List<categoria>();


                categorias = new Cn_Categoria().Lista_Categoria();
                return Json(new { data = categorias }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = "Error al obtener las categorías: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
     
        public JsonResult Agregar_Categoria(categoria objeto)
        {
            object resultado = false;
            string mensaje = string.Empty;

            try
            {
                if (objeto == null)
                {
                    return Json(new { resultado = 0, mensaje = "Datos inválidos" });
                }

                Cn_Categoria Cater = new Cn_Categoria();

                if (objeto.IdCategoria == 0)
                {
                    
                    resultado =  Cater.Nuevo_Ingreso_Categorias(objeto, out mensaje);
                }
                else
                {
                    resultado = Cater.Actualizar_Categoria(objeto, out mensaje);
                     
                }

                return Json(new { resultado,  mensaje });
            }
            catch (Exception ex)
            {

                return Json(new { resultado = 0, mensaje = "Error en el servidor: " + ex.Message });
            }
        }





        [HttpGet]
        public  JsonResult Listar_Marcas()
        {

            try
            {
                List<Marca> Marcas = new List<Marca>();
                Marcas = new Cn_Marca().Listar_Marca();

                return Json(new { data = Marcas }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { error = "Error al obtener las Marcas: " + ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }
    }
}