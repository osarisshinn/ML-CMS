namespace MaerskLine.CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderTrackingID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderTrackingId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderTrackingId");
        }
    }
}
