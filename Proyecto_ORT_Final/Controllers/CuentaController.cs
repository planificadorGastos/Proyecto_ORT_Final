using Proyecto_ORT_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_ORT_Final.Controllers
{
    public class CuentaController : Controller
    {
        Sistema sistema = new Sistema();
        private ProyectoContext db = new ProyectoContext();
        // GET: Cuenta
        public ActionResult Index()

        {


            return View(db.Cuentas.ToList());
        }

 

        
        public ActionResult Create()
        {

            var list = new SelectList(new[] { "Pesos", "Dolares" });
            ViewData["monedas"] = list;

            return View();
        }

        //
        // POST: /Libro/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                var userLogueado = (Usuario)Session["user"];
                var usuario = from u in db.Usuarios
                              where u.Mail == userLogueado.Mail
                              select u;

                cuenta.Usuario = usuario.First();
                db.Cuentas.Add(cuenta);
                db.SaveChanges();
                return RedirectToAction("Index","HojaRuta");
            }

            return View(cuenta);
        }
    }
}