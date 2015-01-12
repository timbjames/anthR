namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class project_nullable_deadline : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Project", "Deadline", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Project", "Deadline", c => c.DateTime(nullable: false));
        }
    }
}
