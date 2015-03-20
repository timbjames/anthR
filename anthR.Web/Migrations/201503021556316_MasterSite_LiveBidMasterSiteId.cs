namespace anthR.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MasterSite_LiveBidMasterSiteId : DbMigration
    {
       
        public override void Up()
        {
            AddColumn("dbo.MasterSite", "LiveBidMasterSiteId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasterSite", "LiveBidMasterSiteId");
        }
   
    }
}
