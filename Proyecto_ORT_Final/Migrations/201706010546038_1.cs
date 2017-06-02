namespace Proyecto_ORT_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Gastoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        fecha = c.DateTime(nullable: false),
                        descripcion = c.String(),
                        monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        pago = c.Boolean(nullable: false),
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
                "dbo.Ingresoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descripcion = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        cuenta_Id = c.Int(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cuentas", t => t.cuenta_Id)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id)
                .Index(t => t.cuenta_Id)
                .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingresoes", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Ingresoes", "cuenta_Id", "dbo.Cuentas");
            DropForeignKey("dbo.Gastoes", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Gastoes", "mapa_Id", "dbo.Mapas");
            DropForeignKey("dbo.Gastoes", "cuenta_Id", "dbo.Cuentas");
            DropForeignKey("dbo.Cuentas", "Usuario_Id", "dbo.Usuarios");
            DropIndex("dbo.Ingresoes", new[] { "Usuario_Id" });
            DropIndex("dbo.Ingresoes", new[] { "cuenta_Id" });
            DropIndex("dbo.Gastoes", new[] { "Usuario_Id" });
            DropIndex("dbo.Gastoes", new[] { "mapa_Id" });
            DropIndex("dbo.Gastoes", new[] { "cuenta_Id" });
            DropIndex("dbo.Cuentas", new[] { "Usuario_Id" });
            DropTable("dbo.Ingresoes");
            DropTable("dbo.Mapas");
            DropTable("dbo.Gastoes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Cuentas");
        }
    }
}
