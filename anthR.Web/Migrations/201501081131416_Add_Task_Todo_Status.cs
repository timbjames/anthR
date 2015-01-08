namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Task_Todo_Status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnthRTask", "StatusId", c => c.Int(nullable: false, defaultValue: 1));
            AddColumn("dbo.TodoItem", "StatusId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.AnthRTask", "StatusId");
            CreateIndex("dbo.TodoItem", "StatusId");
            AddForeignKey("dbo.TodoItem", "StatusId", "dbo.Status", "Id");
            AddForeignKey("dbo.AnthRTask", "StatusId", "dbo.Status", "Id");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnthRTask", "StatusId", "dbo.Status");
            DropForeignKey("dbo.TodoItem", "StatusId", "dbo.Status");
            DropIndex("dbo.TodoItem", new[] { "StatusId" });
            DropIndex("dbo.AnthRTask", new[] { "StatusId" });
            DropColumn("dbo.TodoItem", "StatusId");
            DropColumn("dbo.AnthRTask", "StatusId");
        }
    }
}
