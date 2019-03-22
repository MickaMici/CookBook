namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateRecipeType : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO RecipeTypes( Name) VALUES ( 'Predjela')");
            Sql("INSERT INTO RecipeTypes( Name) VALUES ( 'Supe i corbe ')");
            Sql("INSERT INTO RecipeTypes( Name) VALUES ( 'Glavna jela')");
            Sql("INSERT INTO RecipeTypes( Name) VALUES ( 'Morski plodovi')");
            Sql("INSERT INTO RecipeTypes( Name) VALUES ( 'Torte')");
            Sql("INSERT INTO RecipeTypes( Name) VALUES ( 'Kolaci')");
            Sql("INSERT INTO RecipeTypes( Name) VALUES ( 'Sosovi')");
            Sql("INSERT INTO RecipeTypes( Name) VALUES ( 'Pite i testa')");
            Sql("INSERT INTO RecipeTypes( Name) VALUES ( 'Salate')");
            Sql("INSERT INTO RecipeTypes(Name) VALUES ( 'Zimnica')");

        }

        public override void Down()
        {
        }
    }
}
