namespace CivicdAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "Category", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Category", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Category");
            DropColumn("dbo.Activities", "Category");
        }
    }
}
