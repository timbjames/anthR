namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnthRTask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.ProjectId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MasterSiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MasterSite", t => t.MasterSiteId)
                .Index(t => t.MasterSiteId);
            
            CreateTable(
                "dbo.MasterSite",
                c => new
                    {
                        MasterSiteId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MasterSiteId);
            
            CreateTable(
                "dbo.TodoItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsDone = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                        Description = c.String(),
                        Deadline = c.DateTime(),
                        MasterSiteId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MasterSite", t => t.MasterSiteId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.MasterSiteId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        HexColor = c.String(),
                        Glyphicon = c.String(),
                        ShowIcon = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StaffOnProjects",
                c => new
                    {
                        StaffId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StaffId, t.ProjectId })
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .ForeignKey("dbo.Staff", t => t.StaffId)
                .Index(t => t.StaffId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Note",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        StaffId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Staff", t => t.StaffId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.StaffOnTask",
                c => new
                    {
                        StaffId = c.Int(nullable: false),
                        AnthRTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StaffId, t.AnthRTaskId })
                .ForeignKey("dbo.AnthRTask", t => t.AnthRTaskId)
                .ForeignKey("dbo.Staff", t => t.StaffId)
                .Index(t => t.StaffId)
                .Index(t => t.AnthRTaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnthRTask", "StatusId", "dbo.Status");
            DropForeignKey("dbo.AnthRTask", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.StaffOnTask", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.StaffOnTask", "AnthRTaskId", "dbo.AnthRTask");
            DropForeignKey("dbo.StaffOnProjects", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.Note", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.StaffOnProjects", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.TodoItem", "StatusId", "dbo.Status");
            DropForeignKey("dbo.TodoItem", "MasterSiteId", "dbo.MasterSite");
            DropForeignKey("dbo.Project", "MasterSiteId", "dbo.MasterSite");
            DropIndex("dbo.StaffOnTask", new[] { "AnthRTaskId" });
            DropIndex("dbo.StaffOnTask", new[] { "StaffId" });
            DropIndex("dbo.Note", new[] { "StaffId" });
            DropIndex("dbo.StaffOnProjects", new[] { "ProjectId" });
            DropIndex("dbo.StaffOnProjects", new[] { "StaffId" });
            DropIndex("dbo.TodoItem", new[] { "StatusId" });
            DropIndex("dbo.TodoItem", new[] { "MasterSiteId" });
            DropIndex("dbo.Project", new[] { "MasterSiteId" });
            DropIndex("dbo.AnthRTask", new[] { "StatusId" });
            DropIndex("dbo.AnthRTask", new[] { "ProjectId" });
            DropTable("dbo.StaffOnTask");
            DropTable("dbo.Note");
            DropTable("dbo.Staff");
            DropTable("dbo.StaffOnProjects");
            DropTable("dbo.Status");
            DropTable("dbo.TodoItem");
            DropTable("dbo.MasterSite");
            DropTable("dbo.Project");
            DropTable("dbo.AnthRTask");
        }
    }
}
