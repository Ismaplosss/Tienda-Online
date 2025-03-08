using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Negocio;
using ClosedXML.Excel;

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


        [HttpGet]
        public JsonResult Listar_Consulta()
        {
            Consulta objeto = new Cn_Consulta().Listar_panel();
            return Json(new { data = objeto }, JsonRequestBehavior.AllowGet); // Retornar data como clave
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


        [HttpGet]
        public JsonResult Listar_Reportes(string Fecha_Inicio, string Fecha_Final, string Id)
        {
            Cn_Consulta cn = new Cn_Consulta();
            var reportes = cn.Lista_Reportes(Fecha_Inicio, Fecha_Final, Id);
            if (reportes == null || !reportes.Any())
            {
                return Json(new { data = new List<object>() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data= reportes }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public FileResult Exportar_Reportes(string Fecha_Inicio, string Fecha_Final, string Id)
        {

            List<Reportes> reporte = new List<Reportes>();
            reporte = new Cn_Consulta().Lista_Reportes(Fecha_Inicio, Fecha_Final, Id);

            DataTable dt = new DataTable();
            dt.Locale = new System.Globalization.CultureInfo("es-CR");

            dt.Columns.Add("Fecha venta", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("Id_Transaccion", typeof(string));


            foreach (Reportes repor in reporte)
            {
                dt.Rows.Add(new object[]
                {
                    repor.Fecha_venta,
                    repor.Cliente,
                    repor.Producto,
                    repor.Precio,
                    repor.Cantidad,
                    repor.Total,
                    repor.Id_Transaccion
                });

            }

            dt.TableName = "Datos";

            using (XLWorkbook wb = new XLWorkbook())
            {

                wb.Worksheets.Add(dt);

                using (MemoryStream Stree = new MemoryStream())
                {
                    wb.SaveAs(Stree);
                    return File(Stree.ToArray(), "aplication/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Reprtes_Venta" + DateTime.Now.ToString() + ".xlsx");
                }
            }
           
        }


    }
}