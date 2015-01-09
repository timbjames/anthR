namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Timesheet_DateRecorded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Timesheet", "DateRecorded", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Timesheet", "DateRecorded");
        }
    }
}
