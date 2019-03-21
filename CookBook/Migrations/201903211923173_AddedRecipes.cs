namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRecipes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RecipeTypeId = c.Int(nullable: false),
                        AverageDifficulty = c.Double(nullable: false),
                        AverageRating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RecipeTypes", t => t.RecipeTypeId, cascadeDelete: true)
                .Index(t => t.RecipeTypeId);
            
            CreateTable(
                "dbo.RecipeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "RecipeTypeId", "dbo.RecipeTypes");
            DropIndex("dbo.Recipes", new[] { "RecipeTypeId" });
            DropTable("dbo.RecipeTypes");
            DropTable("dbo.Recipes");
        }
    }
}
