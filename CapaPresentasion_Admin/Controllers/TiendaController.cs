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
    }
}