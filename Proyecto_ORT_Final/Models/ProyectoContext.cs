using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class ProyectoContext : DbContext
    {
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Gasto> Gastos { get; set; }

        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Objetivo> Objetivos { get; set; }

        public DbSet<Contact> Contactos { get; set; }
        public ProyectoContext()
            : base("con") {

        }

        public System.Data.Entity.DbSet<Proyecto_ORT_Final.Models.Factura> Facturas { get; set; }
    }
    }