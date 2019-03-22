namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnotationToRecipeName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "Name", c => c.String());
        }
    }
}
