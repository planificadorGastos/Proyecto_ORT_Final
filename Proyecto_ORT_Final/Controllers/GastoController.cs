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
        private Sistema sistema = new Sistema();

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
            //var userLogueado = (Usuario)Session["user"];

            //var cuentas = from c in db.Cuentas
            //              where c.Usuario.Mail == userLogueado.Mail
            //              select c;


            var list = new SelectList(sistema.getCuentas(), "Id", "Nombre");
            ViewData["cuentas"] = list;
            return View();
        }

        // POST: Gasto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha,descripcion,monto,pago,TipoMoneda")] Gasto gasto,int cuentas,HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid)

                if(gasto.pago &&gasto.monto <= sistema.getCuenta(cuentas).SaldoRestante || !gasto.pago)
            {
               
                var userLogueado = (Usuario)Session["user"];
                    
              
                if (imagen != null)
                {
                  gasto.Imagen = new byte[imagen.ContentLength];
                
                imagen.InputStream.Read(gasto.Imagen, 0, imagen.ContentLength);

                }
                    var usuario = from u in db.Usuarios
                                  where u.Id == userLogueado.Id
                                  select u;

                    var cuenta = from u in db.Cuentas
                                  where u.Id == cuentas
                                  select u;
                    if (gasto.pago)
                    {
                        cuenta.First().SaldoRestante = cuenta.First().SaldoRestante - gasto.monto;
                    }

                gasto.cuenta = cuenta.First();
                gasto.Usuario = usuario.First();
                db.Gastos.Add(gasto);
                db.SaveChanges();
                return RedirectToAction("Index","HojaRuta");
            }

            ViewBag.ErrorSaldo = "Hay errores en los datos ingresados";
            var list = new SelectList(sistema.getCuentas(), "Id", "Nombre");
            ViewData["cuentas"] = list;
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
                return RedirectToAction("Index","HojaRuta");
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



        public ActionResult Pagar(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = new SelectList(sistema.getCuentas(), "Id", "Nombre");
            ViewData["cuentas"] = list;
            Gasto objetivo = db.Gastos.Find(id);
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

            Gasto gasto = db.Gastos.Find(id);

            double cotizacion = sistema.getCotizacion(gasto.TipoMoneda, cta.First().TipoMoneda);

            if (sistema.verificarSaldo(cuentas, gasto.monto))
            {
                
                cta.First().SaldoRestante = cta.First().SaldoRestante - (gasto.monto * (Decimal)cotizacion);
                gasto.pago = true;
                db.SaveChanges();
                return RedirectToAction("Index", "HojaRuta");
            }

            var list = new SelectList(sistema.getCuentas(), "Id", "Nombre");
            ViewData["cuentas"] = list;
            ViewBag.ErrorSaldo = "No tiene saldo suficiente";
            return View(gasto);

        }






    }
}
