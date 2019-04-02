namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProcedureToRecipeClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "Procedure", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "Procedure");
        }
    }
}
