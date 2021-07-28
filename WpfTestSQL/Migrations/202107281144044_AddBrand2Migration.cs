namespace WpfTestSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrand2Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Phones", "Brand_description", c => c.String());
            DropColumn("dbo.Phones", "Company");
            DropColumn("dbo.Phones", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Phones", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Phones", "Company", c => c.String());
            DropColumn("dbo.Phones", "Brand_description");
        }
    }
}
