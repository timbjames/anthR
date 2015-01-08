namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Name_Status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Status", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Status", "Name");
        }
    }
}
