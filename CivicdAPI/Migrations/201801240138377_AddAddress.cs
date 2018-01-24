namespace CivicdAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StreetAddressOne = c.String(),
                        StreetAddressTwo = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Activities", "StartTime", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Activities", "EndTime", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.Activities", "Address_ID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "DisplayName", c => c.String());
            AddColumn("dbo.AspNetUsers", "ProfileDescription", c => c.String());
            AddColumn("dbo.AspNetUsers", "Verified", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "LegalStatus", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Address_ID", c => c.Int());
            CreateIndex("dbo.Activities", "Address_ID");
            CreateIndex("dbo.AspNetUsers", "Address_ID");
            AddForeignKey("dbo.Activities", "Address_ID", "dbo.Addresses", "ID");
            AddForeignKey("dbo.AspNetUsers", "Address_ID", "dbo.Addresses", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Address_ID", "dbo.Addresses");
            DropForeignKey("dbo.Activities", "Address_ID", "dbo.Addresses");
            DropIndex("dbo.AspNetUsers", new[] { "Address_ID" });
            DropIndex("dbo.Activities", new[] { "Address_ID" });
            DropColumn("dbo.AspNetUsers", "Address_ID");
            DropColumn("dbo.AspNetUsers", "LegalStatus");
            DropColumn("dbo.AspNetUsers", "Verified");
            DropColumn("dbo.AspNetUsers", "ProfileDescription");
            DropColumn("dbo.AspNetUsers", "DisplayName");
            DropColumn("dbo.Activities", "Address_ID");
            DropColumn("dbo.Activities", "EndTime");
            DropColumn("dbo.Activities", "StartTime");
            DropTable("dbo.Addresses");
        }
    }
}
