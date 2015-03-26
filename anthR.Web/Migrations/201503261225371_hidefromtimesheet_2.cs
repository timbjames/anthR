namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hidefromtimesheet_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AnthRTask", "HideFromTimesheet", c => c.Boolean());
            AlterColumn("dbo.Project", "HideFromTimesheet", c => c.Boolean());
            AlterColumn("dbo.Timesheet", "HideFromTimesheet", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Timesheet", "HideFromTimesheet", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Project", "HideFromTimesheet", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AnthRTask", "HideFromTimesheet", c => c.Boolean(nullable: false));
        }
    }
}
