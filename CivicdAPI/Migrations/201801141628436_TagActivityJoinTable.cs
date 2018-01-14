namespace CivicdAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagActivityJoinTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TagActivities",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Activity_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Activity_ID })
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .ForeignKey("dbo.Activities", t => t.Activity_ID, cascadeDelete: true)
                .Index(t => t.Tag_ID)
                .Index(t => t.Activity_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagActivities", "Activity_ID", "dbo.Activities");
            DropForeignKey("dbo.TagActivities", "Tag_ID", "dbo.Tags");
            DropIndex("dbo.TagActivities", new[] { "Activity_ID" });
            DropIndex("dbo.TagActivities", new[] { "Tag_ID" });
            DropTable("dbo.TagActivities");
            DropTable("dbo.Tags");
        }
    }
}
