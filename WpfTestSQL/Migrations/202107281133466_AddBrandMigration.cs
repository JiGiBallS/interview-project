namespace WpfTestSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrandMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Phones", "Brand_name", c => c.String());
            DropColumn("dbo.Phones", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Phones", "Title", c => c.String());
            DropColumn("dbo.Phones", "Brand_name");
        }
    }
}
