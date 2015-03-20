namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class project_quoted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "Quoted", c => c.Double(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "Quoted");
        }
    }
}
