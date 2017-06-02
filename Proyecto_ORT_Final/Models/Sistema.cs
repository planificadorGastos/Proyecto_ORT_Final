using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Sistema
    {

        public List<Reporte> reportes { get; set; }

        public HojaDeRuta HojaDeRuta
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Reporte Reporte
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        //  public List<HojaDeRuta> HojasDeRuta { get; set; }
        private ProyectoContext db = new ProyectoContext();
        
        public Sistema()
        {
            //HojasDeRuta = new List<HojaDeRuta>();
            //cargarHojasDeRutaPrueba();
        }

        public HojaDeRuta getHojaDeRuta()
        {

            HojaDeRuta retorno = new HojaDeRuta();
            retorno.ingresos = this.getIngresos();
            retorno.gastos = this.getGastos();

            //var hojaRuta = from p in db.HojaDeRutas
            //               where p.usuario.Mail == u
            //               select p;


            //foreach (HojaDeRuta h in this.HojasDeRuta)
            //{
            //    if (h.usuario.Mail == u)
            //    {
            //        return h;
            //    }
            //}
            return retorno;
        }

        private List<Ingreso> getIngresos()
        {
            if (verificarLogin(HttpContext.Current.Session["user"]))
            {

            

            var userLogueado = (Usuario)HttpContext.Current.Session["user"];
            var ingresos = from i in db.Ingresos
                           where i.Usuario.Id == userLogueado.Id
                           select i;
            return ingresos.ToList();
            }
            else
            {
                return new List<Ingreso>();
            }
        }

        private List<Gasto> getGastos()
        {
            var userLogueado = (Usuario)HttpContext.Current.Session["user"];
            var gastos = from g in db.Gastos
                         where g.Usuario.Id == userLogueado.Id
                         select g;
            return gastos.ToList();
        }

      

        public List<Cuenta> getCuentas()
        {
            var userLogueado = (Usuario)HttpContext.Current.Session["user"];
            List<Cuenta> retorno = new List<Cuenta>();
            var query = from b in db.Cuentas
                        where b.Usuario.Id == userLogueado.Id
                        orderby b.SaldoInicial
                        select b;

            foreach (var Cuenta in query)
            {
                retorno.Add(Cuenta);
            }
            return retorno;
        }

        public bool verificarLogin(Object user)
        {
            if (user != null) return true;
            else return false;
        }

      

    }
}