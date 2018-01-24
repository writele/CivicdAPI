namespace CivicdAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagApplicationUsers",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.ApplicationUser_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Tag_ID)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TagApplicationUsers", "Tag_ID", "dbo.Tags");
            DropIndex("dbo.TagApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TagApplicationUsers", new[] { "Tag_ID" });
            DropTable("dbo.TagApplicationUsers");
        }
    }
}
