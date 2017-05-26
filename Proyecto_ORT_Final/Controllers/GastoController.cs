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
    public class GastoController : Controller
    {
        private ProyectoContext db = new ProyectoContext();

        // GET: Gasto
        public ActionResult Index()
        {
            return View(db.Gastos.ToList());
        }

        // GET: Gasto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gasto gasto = db.Gastos.Find(id);
            if (gasto == null)
            {
                return HttpNotFound();
            }
            return View(gasto);
        }

        // GET: Gasto/Create
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

        // POST: Gasto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha,descripcion,monto,pago")] Gasto gasto,int cuentas)
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

                gasto.cuenta = cuenta.First();
                gasto.Usuario = usuario.First();
                db.Gastos.Add(gasto);
                db.SaveChanges();
                return RedirectToAction("Index","HojaRuta");
            }

            return View(gasto);
        }

        // GET: Gasto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gasto gasto = db.Gastos.Find(id);
            if (gasto == null)
            {
                return HttpNotFound();
            }
            return View(gasto);
        }

        // POST: Gasto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fecha,descripcion,monto,pago")] Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gasto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gasto);
        }

        // GET: Gasto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gasto gasto = db.Gastos.Find(id);
            if (gasto == null)
            {
                return HttpNotFound();
            }
            return View(gasto);
        }

        // POST: Gasto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gasto gasto = db.Gastos.Find(id);
            db.Gastos.Remove(gasto);
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
