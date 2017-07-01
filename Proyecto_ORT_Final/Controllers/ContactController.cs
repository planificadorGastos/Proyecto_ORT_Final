using Proyecto_ORT_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_ORT_Final.Controllers
{
    public class ContactController : Controller
    {
        ProyectoContext context;


        public ContactController()
        {
            context = new ProyectoContext();
        }

        // GET: Contact
        public ActionResult Index()
        {
            return View(context.Contactos.ToList());
        }

        public ActionResult Create()
        {
            return View(new Contact());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contact model)
        {
            if (ModelState.IsValid)
            {
                context.Contactos.Add(model);
               await context.SaveChangesAsync();
                return RedirectToAction("Index", "HojaRuta");
            }


            return View();
        }
    }
}