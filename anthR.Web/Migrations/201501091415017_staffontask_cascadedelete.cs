namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class staffontask_cascadedelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AnthRTask", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.StaffOnTask", "AnthRTaskId", "dbo.AnthRTask");
            DropForeignKey("dbo.AnthRTask", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Project", "MasterSiteId", "dbo.MasterSite");
            DropForeignKey("dbo.StaffOnProjects", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.TodoItem", "MasterSiteId", "dbo.MasterSite");
            DropForeignKey("dbo.TodoItem", "StatusId", "dbo.Status");
            DropForeignKey("dbo.StaffOnProjects", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.Note", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.StaffOnTask", "StaffId", "dbo.Staff");
            AddForeignKey("dbo.AnthRTask", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StaffOnTask", "AnthRTaskId", "dbo.AnthRTask", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AnthRTask", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Project", "MasterSiteId", "dbo.MasterSite", "MasterSiteId", cascadeDelete: true);
            AddForeignKey("dbo.StaffOnProjects", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TodoItem", "MasterSiteId", "dbo.MasterSite", "MasterSiteId", cascadeDelete: true);
            AddForeignKey("dbo.TodoItem", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StaffOnProjects", "StaffId", "dbo.Staff", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Note", "StaffId", "dbo.Staff", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StaffOnTask", "StaffId", "dbo.Staff", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StaffOnTask", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.Note", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.StaffOnProjects", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.TodoItem", "StatusId", "dbo.Status");
            DropForeignKey("dbo.TodoItem", "MasterSiteId", "dbo.MasterSite");
            DropForeignKey("dbo.StaffOnProjects", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "MasterSiteId", "dbo.MasterSite");
            DropForeignKey("dbo.AnthRTask", "StatusId", "dbo.Status");
            DropForeignKey("dbo.StaffOnTask", "AnthRTaskId", "dbo.AnthRTask");
            DropForeignKey("dbo.AnthRTask", "ProjectId", "dbo.Project");
            AddForeignKey("dbo.StaffOnTask", "StaffId", "dbo.Staff", "Id");
            AddForeignKey("dbo.Note", "StaffId", "dbo.Staff", "Id");
            AddForeignKey("dbo.StaffOnProjects", "StaffId", "dbo.Staff", "Id");
            AddForeignKey("dbo.TodoItem", "StatusId", "dbo.Status", "Id");
            AddForeignKey("dbo.TodoItem", "MasterSiteId", "dbo.MasterSite", "MasterSiteId");
            AddForeignKey("dbo.StaffOnProjects", "ProjectId", "dbo.Project", "Id");
            AddForeignKey("dbo.Project", "MasterSiteId", "dbo.MasterSite", "MasterSiteId");
            AddForeignKey("dbo.AnthRTask", "StatusId", "dbo.Status", "Id");
            AddForeignKey("dbo.StaffOnTask", "AnthRTaskId", "dbo.AnthRTask", "Id");
            AddForeignKey("dbo.AnthRTask", "ProjectId", "dbo.Project", "Id");
        }
    }
}
