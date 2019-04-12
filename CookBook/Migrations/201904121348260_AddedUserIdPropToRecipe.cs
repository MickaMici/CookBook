namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserIdPropToRecipe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Recipes", "User_Id");
            AddForeignKey("dbo.Recipes", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Recipes", new[] { "User_Id" });
            DropColumn("dbo.Recipes", "User_Id");
        }
    }
}
