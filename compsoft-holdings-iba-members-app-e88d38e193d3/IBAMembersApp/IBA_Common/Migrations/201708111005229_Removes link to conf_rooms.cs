namespace IBA_Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removeslinktoconf_rooms : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ScheduleEventLocations", "conferenceRoomId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScheduleEventLocations", "conferenceRoomId", c => c.Int(nullable: false));
        }
    }
}
