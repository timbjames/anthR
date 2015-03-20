namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mastersite_novat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasterSite", "HasVAT", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasterSite", "HasVAT");
        }
    }
}
