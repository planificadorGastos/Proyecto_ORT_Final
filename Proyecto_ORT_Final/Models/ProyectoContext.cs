using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class ProyectoContext: DbContext
    {
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Gasto> Gastos { get; set; }

        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

       // public DbSet<HojaDeRuta> HojaDeRutas { get; set; }

        public ProyectoContext()
            : base("con"){

        }
    }
}