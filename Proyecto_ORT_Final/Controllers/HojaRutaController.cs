
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
        Sistema instancia = Sistema.instancia;
        
        // GET: HojaRuta
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
            var HojaRuta = instancia.getHojaDeRuta();
            return View(HojaRuta);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }

      
   
    }
}