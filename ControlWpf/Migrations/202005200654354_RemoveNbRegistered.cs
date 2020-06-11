namespace ControlWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNbRegistered : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Species", "NbRegisteredAnimals");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Species", "NbRegisteredAnimals", c => c.Int(nullable: false));
        }
    }
}
