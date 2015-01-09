namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task_RequestedBy_AgreedWith : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnthRTask", "RequestedBy", c => c.String());
            AddColumn("dbo.AnthRTask", "AgreedWith", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnthRTask", "AgreedWith");
            DropColumn("dbo.AnthRTask", "RequestedBy");
        }
    }
}
