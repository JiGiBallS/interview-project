namespace WpfTestSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogoMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Phones", "Brand_logo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Phones", "Brand_logo");
        }
    }
}
