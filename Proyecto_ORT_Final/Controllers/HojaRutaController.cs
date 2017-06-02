
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_ORT_Final.Models;

namespace Proyecto_ORT_Final.Controllers
{
    public class HojaRutaController : Controller
    {
        private ProyectoContext db = new ProyectoContext();
        Sistema sistema = new Sistema();
        
        // GET: HojaRuta
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
 var HojaRuta = sistema.getHojaDeRuta();
            return View(HojaRuta);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }

        public ActionResult Actualizar(int id)
        {
            HojaDeRuta hr = sistema.getHojaDeRuta();
            hr.actualizarGasto(id);
            return View(hr);
        }
   
    }
}