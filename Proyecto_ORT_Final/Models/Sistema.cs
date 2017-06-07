using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Sistema
    {

        public static Sistema instancia = new Sistema();
        public List<Reporte> reportes { get; set; }
        public List<HojaDeRuta> HojaRutas { get; set; }

        //  public List<HojaDeRuta> HojasDeRuta { get; set; }
        private ProyectoContext db = new ProyectoContext();
        
        public Sistema()
        {
            //HojasDeRuta = new List<HojaDeRuta>();
            //cargarHojasDeRutaPrueba();
        }

        public HojaDeRuta getHojaDeRuta()
        {
            return new  HojaDeRuta();
           
        }

        public List<Ingreso> getIngresos()
        {
            var user = (Usuario)HttpContext.Current.Session["user"];
            if (verificarLogin(user))
            {
                var Ingresos = from c in db.Ingresos
                             where c.Usuario.Id == user.Id
                              select c;
                
                return Ingresos.ToList();

            }
            else
            {
                return new List<Ingreso>();
            }
        }

        public List<Gasto> getGastos()
        {
            var user = (Usuario)HttpContext.Current.Session["user"];
            var Gastos = from c in db.Gastos
                           where c.Usuario.Id == user.Id
                           select c;

            return Gastos.ToList();
        }

      

        public List<Cuenta> getCuentas()
        {
            var user = (Usuario)HttpContext.Current.Session["user"];
            var Cuentas = from c in db.Cuentas
                         where c.Usuario.Id == user.Id
                         select c;

            return Cuentas.ToList();
        }

        public bool verificarLogin(Object user)
        {
            if (user != null) return true;
            else return false;
        }

        public Cuenta getCuenta(int id)
        {
                 var cuenta = from c in db.Cuentas
                     where c.Id == id
                     select c;
            return cuenta.First();
        }

        internal List<Objetivo> getObjetivos()
        {
            var user = (Usuario)HttpContext.Current.Session["user"];
            var Objetivos = from c in db.Objetivos
                          where c.Usuario.Id == user.Id
                          select c;

            return Objetivos.ToList();
        }

        public Usuario getUsuarioLogueado(String id)
        {

            var usr = from u in db.Usuarios
                      where u.Mail == id
                      select u;

            return usr.First();
        }



    }
}