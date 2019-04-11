namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableMeasures : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO IngredientMeasures( Name ) VALUES ('komad')");
        }
        
        public override void Down()
        {
            Sql("Delete from IngredientMeasures");
        }
    }
}
