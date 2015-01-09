namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timesheet_initialise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Timesheet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hours = c.Int(nullable: false),
                        Mins = c.Int(nullable: false),
                        HourlyRate = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        AnthRTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnthRTask", t => t.AnthRTaskId, cascadeDelete: true)
                .ForeignKey("dbo.Staff", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId)
                .Index(t => t.AnthRTaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Timesheet", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.Timesheet", "AnthRTaskId", "dbo.AnthRTask");
            DropIndex("dbo.Timesheet", new[] { "AnthRTaskId" });
            DropIndex("dbo.Timesheet", new[] { "StaffId" });
            DropTable("dbo.Timesheet");
        }
    }
}
