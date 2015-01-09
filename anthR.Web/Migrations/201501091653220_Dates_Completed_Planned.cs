namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dates_Completed_Planned : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnthRTask", "Deadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.AnthRTask", "PlannedStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.AnthRTask", "DateCompleted", c => c.DateTime(nullable: false));
            AddColumn("dbo.Project", "Completed", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Project", "Deadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.Project", "PlannedStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.Project", "DateCompleted", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "DateCompleted");
            DropColumn("dbo.Project", "PlannedStart");
            DropColumn("dbo.Project", "Deadline");
            DropColumn("dbo.Project", "Completed");
            DropColumn("dbo.AnthRTask", "DateCompleted");
            DropColumn("dbo.AnthRTask", "PlannedStart");
            DropColumn("dbo.AnthRTask", "Deadline");
        }
    }
}
