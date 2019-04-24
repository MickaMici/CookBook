namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedIngredientMeasures : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.IngredientMeasures");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IngredientMeasures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
