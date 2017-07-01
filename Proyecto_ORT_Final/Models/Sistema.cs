using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Sistema
    {

      
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

        internal void VerificarCuotasPagasObjetivos()
        {
            List<Objetivo> objetivos = this.getObjetivos();
            foreach(Objetivo o in objetivos)
            {
                if(o.FechaUltimaCuotaPaga.Month < DateTime.Today.Month && o.DebitoAutomatico)
                {
                    this.debitarMontoCuotaObjetivo(o);
                }
            }
        }

        private void debitarMontoCuotaObjetivo(Objetivo o)
        {

            var objetivo = from c in db.Objetivos
                         where c.Id == o.Id
                         select c.Cuenta.Id;

            var cuenta = from c in db.Cuentas
                          where c.Id == objetivo.FirstOrDefault()
                         select c;

            cuenta.First().SaldoRestante = cuenta.First().SaldoRestante - o.MontoMensual;
            o.CuotaActual++;
            o.FechaUltimaCuotaPaga = DateTime.Today;
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();


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
            DateTime fechaActual = DateTime.Today;
            var mesActual = fechaActual.Month;
            var añoActual = fechaActual.Year;
            var user = (Usuario)HttpContext.Current.Session["user"];
            var Gastos = from c in db.Gastos
                           where c.Usuario.Id == user.Id
                            where c.fecha.Month == mesActual
                         where c.fecha.Year == añoActual

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

        internal decimal calcularPromedioMensual(decimal monto, DateTime fecha)
        {
            DateTime fechaActual = DateTime.Today;
            
            int cantidadMeses = Math.Abs((fechaActual.Month - fecha.Month) + 12 * (fechaActual.Year - fecha.Year));
            if (cantidadMeses == 0)
            {
                return monto;
            }
            return monto / cantidadMeses;
        }

        internal int cantidadCuotas(DateTime fecha)
        {
            DateTime fechaActual = DateTime.Today;

            int cantidadMeses = Math.Abs((fechaActual.Month - fecha.Month) + 12 * (fechaActual.Year - fecha.Year));

            if (cantidadMeses == 0)
            {
                return 1;
            }

            return cantidadMeses;
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
                            where c.Pago == false
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

        internal bool verificarSaldo(int cuentas, decimal montoMensual)
        {
            var cuenta = from c in db.Cuentas
                         where c.Id == cuentas
                         select c;
            return cuenta.First().SaldoRestante >= montoMensual;

        }

        internal double getCotizacion(string tipoMoneda1, string tipoMoneda2)
        {
            net.webservicex.www.CurrencyConvertor objWS = new net.webservicex.www.CurrencyConvertor(); 
            double cotizacion = 0;
            
            if (tipoMoneda1 == "Dólares" && tipoMoneda2 == "Pesos")
            {
                cotizacion = objWS.ConversionRate(net.webservicex.www.Currency.USD, net.webservicex.www.Currency.INR);
                return cotizacion;

            }
            else if (tipoMoneda1 == "Pesos" && tipoMoneda2 == "Dólares")
            {
                
                cotizacion = objWS.ConversionRate(net.webservicex.www.Currency.USD, net.webservicex.www.Currency.INR);
                return cotizacion;
            }

            return cotizacion;
        }

     
    }
}