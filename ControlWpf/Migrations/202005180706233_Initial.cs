namespace ControlWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IdentificationNumber = c.Int(nullable: false),
                        SpeciesId = c.Int(nullable: false),
                        Race = c.String(),
                        AnimalSex = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Species", t => t.SpeciesId, cascadeDelete: true)
                .Index(t => t.SpeciesId);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NbRegisteredAnimals = c.Int(nullable: false),
                        NbMaxAuthorizedKills = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animals", "SpeciesId", "dbo.Species");
            DropIndex("dbo.Animals", new[] { "SpeciesId" });
            DropTable("dbo.Species");
            DropTable("dbo.Animals");
        }
    }
}
