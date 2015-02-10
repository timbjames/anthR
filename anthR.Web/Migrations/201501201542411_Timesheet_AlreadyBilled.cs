namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Timesheet_AlreadyBilled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Timesheet", "AlreadyBilled", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Timesheet", "AlreadyBilled");
        }
    }
}
