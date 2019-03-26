namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateIngrediantMeasure : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO IngredientMeasures( Name) VALUES ( 'kg')");
            Sql("INSERT INTO IngredientMeasures( Name) VALUES ( 'g')");
            Sql("INSERT INTO IngredientMeasures( Name) VALUES ( 'l')");
            Sql("INSERT INTO IngredientMeasures( Name) VALUES ( 'dl')");
            Sql("INSERT INTO IngredientMeasures( Name) VALUES ( 'ml')");
            Sql("INSERT INTO IngredientMeasures( Name) VALUES ( N'supena kašičica')");
            Sql("INSERT INTO IngredientMeasures( Name) VALUES ( N'kafena kašičica')");
            Sql("INSERT INTO IngredientMeasures( Name) VALUES ( N'šolja(2dl)')");

        }

        public override void Down()
        {
            Sql("Delete from IngredientMeasures");
        }
    }
}
