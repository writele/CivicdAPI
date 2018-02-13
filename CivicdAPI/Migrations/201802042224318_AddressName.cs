namespace CivicdAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "Name");
        }
    }
}
