namespace Proyecto_ORT_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        email = c.String(nullable: false, maxLength: 100),
                        Telefono = c.String(nullable: false, maxLength: 100),
                        Mensaje = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cuentas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        SaldoInicial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaldoRestante = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoMoneda = c.String(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mail = c.String(),
                        HojaRuta_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HojaDeRutas", t => t.HojaRuta_Id)
                .Index(t => t.HojaRuta_Id);
            
            CreateTable(
                "dbo.Gastoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        fecha = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        descripcion = c.String(),
                        monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        pago = c.Boolean(nullable: false),
                        TipoMoneda = c.String(),
                        Imagen = c.Binary(),
                        cuenta_Id = c.Int(),
                        mapa_Id = c.Int(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cuentas", t => t.cuenta_Id)
                .ForeignKey("dbo.Mapas", t => t.mapa_Id)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id)
                .Index(t => t.cuenta_Id)
                .Index(t => t.mapa_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Mapas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HojaDeRutas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notificacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HojaDeRuta_Id = c.Int(),
                        Objetivo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HojaDeRutas", t => t.HojaDeRuta_Id)
                .ForeignKey("dbo.Objetivoes", t => t.Objetivo_Id)
                .Index(t => t.HojaDeRuta_Id)
                .Index(t => t.Objetivo_Id);
            
            CreateTable(
                "dbo.Ingresoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descripcion = c.String(),
                        Fecha = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        cuenta_Id = c.Int(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cuentas", t => t.cuenta_Id)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id)
                .Index(t => t.cuenta_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Objetivoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Fecha = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontoMensual = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoMoneda = c.String(),
                        Pago = c.Boolean(nullable: false),
                        CuotaActual = c.Int(nullable: false),
                        CuotasTotales = c.Int(nullable: false),
                        FechaUltimaCuotaPaga = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DebitoAutomatico = c.Boolean(nullable: false),
                        Cuenta_Id = c.Int(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cuentas", t => t.Cuenta_Id)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id)
                .Index(t => t.Cuenta_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Qr = c.String(),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Objetivoes", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Notificacions", "Objetivo_Id", "dbo.Objetivoes");
            DropForeignKey("dbo.Objetivoes", "Cuenta_Id", "dbo.Cuentas");
            DropForeignKey("dbo.Ingresoes", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Ingresoes", "cuenta_Id", "dbo.Cuentas");
            DropForeignKey("dbo.Usuarios", "HojaRuta_Id", "dbo.HojaDeRutas");
            DropForeignKey("dbo.Notificacions", "HojaDeRuta_Id", "dbo.HojaDeRutas");
            DropForeignKey("dbo.Gastoes", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Gastoes", "mapa_Id", "dbo.Mapas");
            DropForeignKey("dbo.Gastoes", "cuenta_Id", "dbo.Cuentas");
            DropForeignKey("dbo.Cuentas", "Usuario_Id", "dbo.Usuarios");
            DropIndex("dbo.Objetivoes", new[] { "Usuario_Id" });
            DropIndex("dbo.Objetivoes", new[] { "Cuenta_Id" });
            DropIndex("dbo.Ingresoes", new[] { "Usuario_Id" });
            DropIndex("dbo.Ingresoes", new[] { "cuenta_Id" });
            DropIndex("dbo.Notificacions", new[] { "Objetivo_Id" });
            DropIndex("dbo.Notificacions", new[] { "HojaDeRuta_Id" });
            DropIndex("dbo.Gastoes", new[] { "Usuario_Id" });
            DropIndex("dbo.Gastoes", new[] { "mapa_Id" });
            DropIndex("dbo.Gastoes", new[] { "cuenta_Id" });
            DropIndex("dbo.Usuarios", new[] { "HojaRuta_Id" });
            DropIndex("dbo.Cuentas", new[] { "Usuario_Id" });
            DropTable("dbo.Facturas");
            DropTable("dbo.Objetivoes");
            DropTable("dbo.Ingresoes");
            DropTable("dbo.Notificacions");
            DropTable("dbo.HojaDeRutas");
            DropTable("dbo.Mapas");
            DropTable("dbo.Gastoes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Cuentas");
            DropTable("dbo.Contacts");
        }
    }
}
