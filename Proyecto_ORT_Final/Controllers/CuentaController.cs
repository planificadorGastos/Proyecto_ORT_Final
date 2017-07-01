using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_ORT_Final.Models;

namespace Proyecto_ORT_Final.Controllers
{
    public class CuentaController : Controller
    {
        private ProyectoContext db = new ProyectoContext();
        private Sistema sistema = new Sistema();
        // GET: Cuenta mod
        public ActionResult Index()
        {
            return View(db.Cuentas.ToList());
        }


        // GET: Cuenta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuenta cuenta = db.Cuentas.Find(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            return View(cuenta);
        }

        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            
            var userLogueado = (Usuario)Session["user"];
            
            var list = new SelectList(sistema.getCuentas(), "Id", "Nombre");
            ViewData["cuentas"] = list;


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
                              where u.Id == userLogueado.Id
                              select u;

                cuenta.Usuario = usuario.First();
                cuenta.SaldoRestante = cuenta.SaldoInicial;
                db.Cuentas.Add(cuenta);
                db.SaveChanges();
                return RedirectToAction("Index", "HojaRuta");
            }

            return View(cuenta);
        }

        // GET: Cuenta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuenta cuenta = db.Cuentas.Find(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            return View(cuenta);
        }

        // POST: Cuenta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,SaldoInicial,SaldoRestante,TipoMoneda")] Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuenta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","HojaRuta");
            }
            return View(cuenta);
        }

        // GET: Cuenta/Delete/5
        // GET: Cuenta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuenta cuenta = db.Cuentas.Find(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }

            return View(cuenta);
        }

        // POST: Cuenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuenta cuenta = db.Cuentas.Find(id);

            var gastos = from g in db.Gastos
                         where g.cuenta.Id == id
                         select g;

            var ingresos = from i in db.Ingresos
                         where i.cuenta.Id == id
                         select i;

            foreach (var gasto in gastos.ToList())
            {
                db.Gastos.Remove(gasto);
                
            }

            foreach (var ingreso in ingresos.ToList())
            {
                db.Ingresos.Remove(ingreso);

            }



            db.Cuentas.Remove(cuenta);
            db.SaveChanges();
            return RedirectToAction("Index","HojaRuta");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
