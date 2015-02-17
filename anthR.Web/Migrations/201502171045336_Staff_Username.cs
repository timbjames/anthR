namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Staff_Username : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staff", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staff", "Username");
        }
    }
}
