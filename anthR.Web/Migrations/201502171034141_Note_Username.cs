namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Note_Username : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "Username", c => c.String(nullable: false, defaultValue: "KFS\\tim"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Note", "Username");
        }
    }
}
