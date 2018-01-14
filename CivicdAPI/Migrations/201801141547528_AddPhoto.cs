namespace CivicdAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "Photo", c => c.String());
            AddColumn("dbo.AspNetUsers", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Photo");
            DropColumn("dbo.Activities", "Photo");
        }
    }
}
