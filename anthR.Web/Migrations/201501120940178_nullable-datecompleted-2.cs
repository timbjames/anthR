namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullabledatecompleted2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AnthRTask", "DateCompleted", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AnthRTask", "DateCompleted", c => c.DateTime(nullable: false));
        }
    }
}
