namespace MemberDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comments5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ContactId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "ContactId");
            AddForeignKey("dbo.Comments", "ContactId", "dbo.Contacts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ContactId", "dbo.Contacts");
            DropIndex("dbo.Comments", new[] { "ContactId" });
            DropColumn("dbo.Comments", "ContactId");
        }
    }
}
