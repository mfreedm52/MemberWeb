namespace MemberDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class July12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "BusinessType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "BusinessType");
        }
    }
}
