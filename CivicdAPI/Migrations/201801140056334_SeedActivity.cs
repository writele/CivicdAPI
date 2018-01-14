namespace CivicdAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedActivity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "DisplayTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "DisplayTitle");
        }
    }
}
