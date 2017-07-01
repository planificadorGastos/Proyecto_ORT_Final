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
    public class ObjetivoController : Controller
    {
        private ProyectoContext db = new ProyectoContext();
        private Sistema sistema = new Sistema();
        
        
        // GET: Objetivo
        public ActionResult Index()
        {
            return View(db.Objetivos.ToList());

        }

        // GET: Objetivo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivos.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // GET: Objetivo/Create
        public ActionResult Create()
        {
            var list = new SelectList(sistema.getCuentas(), "Id", "Nombre");
            ViewData["cuentas"] = list;
            return View();
        }

        // POST: Objetivo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,Fecha,Monto,TipoMoneda,DebitoAutomatico")] Objetivo objetivo, int cuentas)
        {
            if (ModelState.IsValid)
            {
                var userLogueado = (Usuario)Session["user"];
                var usuario = from u in db.Usuarios
                              where u.Mail == userLogueado.Mail
                              select u;

                var cuenta = from u in db.Cuentas
                             where u.Id == cuentas
                             select u;
                objetivo.Cuenta = cuenta.First();
                objetivo.Usuario = usuario.First();
                objetivo.MontoMensual= sistema.calcularPromedioMensual(objetivo.Monto, objetivo.Fecha);
                objetivo.CuotasTotales = sistema.cantidadCuotas(objetivo.Fecha);
                objetivo.CuotaActual = 0;
                db.Objetivos.Add(objetivo);
                db.SaveChanges();
                return RedirectToAction("Index","HojaRuta");
            }

            return View(objetivo);
        }

        // GET: Objetivo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivos.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // POST: Objetivo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,Fecha,Monto")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objetivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "HojaRuta");
            }
            return View(objetivo);
        }

        // GET: Objetivo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivos.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // POST: Objetivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Objetivo objetivo = db.Objetivos.Find(id);
            db.Objetivos.Remove(objetivo);
            db.SaveChanges();
            return RedirectToAction("Index", "HojaRuta");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       

        public ActionResult Pagar(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = new SelectList(sistema.getCuentas(), "Id", "Nombre");
            ViewData["cuentas"] = list;
            Objetivo objetivo = db.Objetivos.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // POST: Objetivo/Delete/5
        [HttpPost, ActionName("Pagar")]
        [ValidateAntiForgeryToken]
        public ActionResult PagarConfirmed(int id, int cuentas)
        {
            var cta = from u in db.Cuentas
                        where u.Id == cuentas
                          select u;
            
            Objetivo objetivo = db.Objetivos.Find(id);
            if (sistema.verificarSaldo(cuentas, objetivo.MontoMensual))
            {
                cta.First().SaldoRestante = cta.First().SaldoRestante - objetivo.MontoMensual;
                objetivo.CuotaActual = objetivo.CuotaActual + 1;
                if (objetivo.CuotaActual >= objetivo.CuotasTotales)
                {
                    objetivo.Pago = true;
                }
                db.SaveChanges();
                return RedirectToAction("Index","HojaRuta");
            }

            var list = new SelectList(sistema.getCuentas(), "Id", "Nombre");
            ViewData["cuentas"] = list;
            ViewBag.ErrorSaldo = "No tiene saldo suficiente";
                return View(objetivo);
            
        }





    }
}
