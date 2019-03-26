namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixLatinExtended : DbMigration
    {
        public override void Up()
        {
            Sql("Update RecipeTypes set Name=N'Kolači' where Name='Kolaci'");
            Sql("Update RecipeTypes set Name=N'Supe i čorbe' where Name='Supe i corbe'");
        }
        
        public override void Down()
        {
            Sql("Update RecipeTypes set Name='Kolaci' where Name=  N'Kolači'");
            Sql("Update RecipeTypes set Name='Supe i corbe' where Name=N'Supe i čorbe'");
        }
    }
}
