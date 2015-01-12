namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class project_nullable_datecompleted : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Project", "DateCompleted", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Project", "DateCompleted", c => c.DateTime(nullable: false));
        }
    }
}
