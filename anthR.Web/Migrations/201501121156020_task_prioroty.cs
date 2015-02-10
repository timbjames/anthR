namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class task_prioroty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnthRTask", "Priority", c => c.Int(nullable: false, defaultValue: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnthRTask", "Priority");
        }
    }
}
