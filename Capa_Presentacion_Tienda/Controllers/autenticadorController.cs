using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capa_Presentacion_Admin.Controllers
{
    public class autenticadorController : Controller
    {
        // GET: autenticador
        public ActionResult Login()
        {
            return View();
        }
    }
}