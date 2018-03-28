namespace CivicdAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserActivityMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserActivities", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserActivities", new[] { "User_Id" });
            RenameColumn(table: "dbo.UserActivities", name: "User_Id", newName: "UserID");
            DropPrimaryKey("dbo.UserActivities");
            AddColumn("dbo.UserActivities", "UserActivityId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserActivities", "UserID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserActivities", new[] { "UserActivityId", "ActivityID", "UserID" });
            CreateIndex("dbo.UserActivities", "UserID");
            AddForeignKey("dbo.UserActivities", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.UserActivities", "ApplicationUserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserActivities", "ApplicationUserID", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserActivities", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.UserActivities", new[] { "UserID" });
            DropPrimaryKey("dbo.UserActivities");
            AlterColumn("dbo.UserActivities", "UserID", c => c.String(maxLength: 128));
            DropColumn("dbo.UserActivities", "UserActivityId");
            AddPrimaryKey("dbo.UserActivities", new[] { "ApplicationUserID", "ActivityID" });
            RenameColumn(table: "dbo.UserActivities", name: "UserID", newName: "User_Id");
            CreateIndex("dbo.UserActivities", "User_Id");
            AddForeignKey("dbo.UserActivities", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
