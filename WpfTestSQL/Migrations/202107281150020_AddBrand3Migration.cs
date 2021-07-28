namespace WpfTestSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrand3Migration : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Phones", new[] { "Id" });
            DropColumn("dbo.Phones", "Id");
            AddColumn("dbo.Phones", "Brand_id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Phones", "Brand_id");
        }
        
    }
}
