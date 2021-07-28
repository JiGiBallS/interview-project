namespace WpfTestSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrand4Migration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Phones", newName: "Brands");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Brands", newName: "Phones");
        }
    }
}
