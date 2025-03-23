using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentasion_Tienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Productos()
        {
            return View();
        }


        [HttpGet]
        public JsonResult Listar_Categorias()
        {

            List<categoria> Lista_categoria = new List<categoria>();

            Cn_Categoria categoria = new Cn_Categoria();


            Lista_categoria = categoria.Lista_Categoria();

            return Json(new { data=Lista_categoria}, JsonRequestBehavior.AllowGet);


        }





    }
}