using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public String Mail { get; set; }

        private Ranking Ranking { get; set; }
        public List<Cuenta> Cuentas { get; set; }
        public List<Ingreso> Ingresos { get; set; }
        public List<Gasto> Gastos { get; set; }
       // public Rol Rol { get; set; }
        public HojaDeRuta HojaRuta { get; set; }
        public List<Objetivo> Objetivos { get; set; }
    }



    }
