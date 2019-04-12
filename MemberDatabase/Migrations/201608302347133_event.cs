namespace MemberDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _event : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserGroup", newName: "UserGroups");
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        startDate = c.DateTime(nullable: false),
                        endDate = c.DateTime(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Events");
            RenameTable(name: "dbo.UserGroups", newName: "UserGroup");
        }
    }
}
