namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hidefromtimesheet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnthRTask", "HideFromTimesheet", c => c.Boolean(nullable: true));
            AddColumn("dbo.Project", "HideFromTimesheet", c => c.Boolean(nullable: true));
            AddColumn("dbo.Timesheet", "HideFromTimesheet", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Timesheet", "HideFromTimesheet");
            DropColumn("dbo.Project", "HideFromTimesheet");
            DropColumn("dbo.AnthRTask", "HideFromTimesheet");
        }
    }
}
