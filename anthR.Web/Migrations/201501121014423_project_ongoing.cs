namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class project_ongoing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "OnGoing", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "OnGoing");
        }
    }
}
