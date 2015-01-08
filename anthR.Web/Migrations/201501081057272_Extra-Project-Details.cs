namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtraProjectDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnthRTask", "Name", c => c.String());
            AddColumn("dbo.AnthRTask", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnthRTask", "Description");
            DropColumn("dbo.AnthRTask", "Name");
        }
    }
}
