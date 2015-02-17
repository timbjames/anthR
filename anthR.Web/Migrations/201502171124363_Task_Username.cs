namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task_Username : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnthRTask", "Username", c => c.String(nullable: false, defaultValue: "KFS\\tim"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnthRTask", "Username");
        }
    }
}
