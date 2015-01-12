namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullabledatecompleted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AnthRTask", "DateCompleted");
            AddColumn("dbo.AnthRTask", "DateCompleted", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnthRTask", "DateCompleted");
            AddColumn("dbo.AnthRTask", "DateCompleted", c => c.DateTime(nullable: false));
        }
    }
}
