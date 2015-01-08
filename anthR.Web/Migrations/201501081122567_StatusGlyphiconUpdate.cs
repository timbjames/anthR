namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusGlyphiconUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Status", "ShowIcon", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Status", "ShowIcon");
        }
    }
}
