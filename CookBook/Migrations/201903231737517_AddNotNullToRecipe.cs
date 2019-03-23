namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotNullToRecipe : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipes", "AverageDifficulty", c => c.Double());
            AlterColumn("dbo.Recipes", "AverageRating", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "AverageRating", c => c.Double(nullable: false));
            AlterColumn("dbo.Recipes", "AverageDifficulty", c => c.Double(nullable: false));
        }
    }
}
