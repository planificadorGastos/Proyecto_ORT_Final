using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class HojaDeRuta
    {
        public int Id { get; set; }
        
        
        private ProyectoContext db = new ProyectoContext();
        public List<Notificacion> Notificaciones { get; set; }

        public decimal getTotalPago()
    {
            decimal retorno=0;
            foreach(Gasto g in Sistema.instancia.getGastos())
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
            foreach (Gasto g in Sistema.instancia.getGastos())
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
            foreach (Gasto g in Sistema.instancia.getGastos())
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
            foreach (Gasto g in Sistema.instancia.getGastos())
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
            foreach (Gasto g in Sistema.instancia.getGastos())
            {
                if (!g.pago && g.Id== id)
                {
                    g.pago = true;
                }
            }
        }


       


      public List<Objetivo> getObjetivos()
        {
            return Sistema.instancia.getObjetivos();
        }


        public List<Cuenta> getCuentas()
        {
            return Sistema.instancia.getCuentas();
        }



    }

}