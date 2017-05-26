using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class HojaDeRuta
    {
        public int Id { get; set; }
        public List<Objetivo> objetivos { get; set; }
        public Usuario usuario { get; set; }
        public List<Ingreso> ingresos { get; set; }
        public List<Gasto> gastos { get; set; }

        public Usuario Usuario
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Ingreso Ingreso
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Gasto Gasto
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Objetivo Objetivo
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Cuenta Cuenta
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        private ProyectoContext db = new ProyectoContext();


        public decimal getTotalPago()
    {
            decimal retorno=0;
            foreach(Gasto g in gastos)
            {
                if (g.pago)
                {
                    retorno += g.monto;
                }        
            }
              return retorno;
    }

        public decimal getTotalPendiente()
        {
            decimal retorno = 0;
            foreach (Gasto g in gastos)
            {
                if (!g.pago)
                {
                    retorno += g.monto;
                }
            }
            return retorno;
        }

        public List<Gasto>  getGastosPagos()
        {
            List<Gasto> retorno = new List<Gasto>();
            foreach (Gasto g in gastos)
            {
                if (g.pago)
                {
                    retorno.Add(g);
                }
            }
            return retorno;
        }

        public List<Gasto> getGastosNoPagos()
        {
            List<Gasto> retorno = new List<Gasto>();
            foreach (Gasto g in gastos)
            {
                if (!g.pago)
                {
                    retorno.Add(g);
                }
            }
            return retorno;
        }

        public void actualizarGasto(int id)
        {
            foreach (Gasto g in gastos)
            {
                if (!g.pago && g.Id== id)
                {
                    g.pago = true;
                }
            }
        }

        public List<Cuenta> getCuentas()
        {
            var userLogueado = (Usuario)HttpContext.Current.Session["user"];
            var cuentas = from c in db.Cuentas
                           where c.Usuario.Mail == userLogueado.Mail
                           select c;
            return cuentas.ToList();
        }

    }

}