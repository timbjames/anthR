namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Timesheet_Quoted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Timesheet", "Quoted", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Timesheet", "Quoted");
        }
    }
}
