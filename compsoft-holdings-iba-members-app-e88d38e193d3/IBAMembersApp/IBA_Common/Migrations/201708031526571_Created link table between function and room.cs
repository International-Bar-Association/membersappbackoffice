namespace IBA_Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Createdlinktablebetweenfunctionandroom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventLocationFunctionLinks",
                c => new
                    {
                        EventLocationFunctionLinkId = c.Int(nullable: false, identity: true),
                        functionId = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Location_ScheduleEventLocationId = c.Int(),
                    })
                .PrimaryKey(t => t.EventLocationFunctionLinkId)
                .ForeignKey("dbo.ScheduleEventLocations", t => t.Location_ScheduleEventLocationId)
                .Index(t => t.Location_ScheduleEventLocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventLocationFunctionLinks", "Location_ScheduleEventLocationId", "dbo.ScheduleEventLocations");
            DropIndex("dbo.EventLocationFunctionLinks", new[] { "Location_ScheduleEventLocationId" });
            DropTable("dbo.EventLocationFunctionLinks");
        }
    }
}
