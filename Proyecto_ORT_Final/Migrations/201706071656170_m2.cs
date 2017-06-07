namespace Proyecto_ORT_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Objetivoes", "Descripcion", c => c.String());
            AddColumn("dbo.Objetivoes", "Fecha", c => c.DateTime(nullable: false));
            AddColumn("dbo.Objetivoes", "Monto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Objetivoes", "Monto");
            DropColumn("dbo.Objetivoes", "Fecha");
            DropColumn("dbo.Objetivoes", "Descripcion");
        }
    }
}
