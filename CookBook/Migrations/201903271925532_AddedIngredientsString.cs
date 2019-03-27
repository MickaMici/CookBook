namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIngredientsString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "Ingredients", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "Ingredients");
        }
    }
}
