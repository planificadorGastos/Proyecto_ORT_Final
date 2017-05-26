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
    public class IngresoController : Controller
    {
        private ProyectoContext db = new ProyectoContext();

        // GET: Ingreso
        public ActionResult Index()
        {
            return View(db.Ingresos.ToList());
        }

        // GET: Ingreso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingreso ingreso = db.Ingresos.Find(id);
            if (ingreso == null)
            {
                return HttpNotFound();
            }
            return View(ingreso);
        }

        // GET: Ingreso/Create
        public ActionResult Create()
        {

            var userLogueado = (Usuario)Session["user"];

            var cuentas = from c in db.Cuentas
                          where c.Usuario.Mail == userLogueado.Mail
                          select c;

            var list = new SelectList(cuentas, "Id", "Nombre");
            ViewData["cuentas"] = list;

            return View();
        }

        // POST: Ingreso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ingreso ingreso, int cuentas)
        {
            if (ModelState.IsValid)
            {
                var userLogueado = (Usuario)Session["user"];

                var usuario = from u in db.Usuarios
                              where u.Mail == userLogueado.Mail
                              select u;

                var cuenta = from c in db.Cuentas
                              where c.Id == cuentas
                             select c;

                ingreso.cuenta = cuenta.First();
                ingreso.Usuario = usuario.First();
                ingreso.cuenta.Id = cuentas;
                db.Ingresos.Add(ingreso);
                db.SaveChanges();
                return RedirectToAction("Index","HojaRuta");
            }

            return View(ingreso);
        }

        // GET: Ingreso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingreso ingreso = db.Ingresos.Find(id);
            if (ingreso == null)
            {
                return HttpNotFound();
            }
            return View(ingreso);
        }

        // POST: Ingreso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] Ingreso ingreso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingreso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ingreso);
        }

        // GET: Ingreso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingreso ingreso = db.Ingresos.Find(id);
            if (ingreso == null)
            {
                return HttpNotFound();
            }
            return View(ingreso);
        }

        // POST: Ingreso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingreso ingreso = db.Ingresos.Find(id);
            db.Ingresos.Remove(ingreso);
            db.SaveChanges();
            return RedirectToAction("Index");
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
