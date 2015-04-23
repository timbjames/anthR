namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_password : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staff", "Password", c => c.String(defaultValue: ""));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staff", "Password");
        }
    }
}
