namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnotationRequiredRecipeTYpeName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RecipeTypes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RecipeTypes", "Name", c => c.String());
        }
    }
}
