namespace IBA_Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppP2PMessage",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Read = c.DateTime(),
                        Received = c.DateTime(),
                        Deleted = c.DateTime(),
                        DeviceOwner_Id = c.Int(),
                        Message_P2PMessageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceOwners", t => t.DeviceOwner_Id)
                .ForeignKey("dbo.P2PMessage", t => t.Message_P2PMessageId)
                .Index(t => t.DeviceOwner_Id)
                .Index(t => t.Message_P2PMessageId);
            
            CreateTable(
                "dbo.P2PMessage",
                c => new
                    {
                        P2PMessageId = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        SentTime = c.DateTime(),
                        DeliveredTime = c.DateTime(),
                        ReadTime = c.DateTime(),
                        SenderName = c.String(),
                        SenderId = c.Int(nullable: false),
                        Thread_P2PMessageThreadId = c.Int(),
                    })
                .PrimaryKey(t => t.P2PMessageId)
                .ForeignKey("dbo.P2PMessageThread", t => t.Thread_P2PMessageThreadId)
                .Index(t => t.Thread_P2PMessageThreadId);
            
            CreateTable(
                "dbo.P2PMessageThread",
                c => new
                    {
                        P2PMessageThreadId = c.Int(nullable: false, identity: true),
                        InitialSenderId = c.Int(nullable: false),
                        InitialSenderDeletionDate = c.DateTime(),
                        InitialRecipientId = c.Int(nullable: false),
                        InitialRecipientDeletionDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.P2PMessageThreadId);
            
            CreateTable(
                "dbo.ScheduleEventLocations",
                c => new
                    {
                        ScheduleEventLocationId = c.Int(nullable: false, identity: true),
                        ConferenceId = c.Int(nullable: false),
                        conferenceRoomId = c.Int(nullable: false),
                        CentreX = c.Int(nullable: false),
                        CentreY = c.Int(nullable: false),
                        Floor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleEventLocationId);
            
            AddColumn("dbo.CmsMessages", "SendDate", c => c.DateTime());
            AddColumn("dbo.PushMessageQueues", "AppP2PMessage_Id", c => c.Long());
            AddColumn("dbo.PushMessageQueues", "P2PMessage_P2PMessageId", c => c.Int());
            AddColumn("dbo.ContentLibraries", "AvailableFromDate", c => c.DateTime());
            CreateIndex("dbo.PushMessageQueues", "AppP2PMessage_Id");
            CreateIndex("dbo.PushMessageQueues", "P2PMessage_P2PMessageId");
            AddForeignKey("dbo.PushMessageQueues", "AppP2PMessage_Id", "dbo.AppP2PMessage", "Id");
            AddForeignKey("dbo.PushMessageQueues", "P2PMessage_P2PMessageId", "dbo.P2PMessage", "P2PMessageId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PushMessageQueues", "P2PMessage_P2PMessageId", "dbo.P2PMessage");
            DropForeignKey("dbo.PushMessageQueues", "AppP2PMessage_Id", "dbo.AppP2PMessage");
            DropForeignKey("dbo.AppP2PMessage", "Message_P2PMessageId", "dbo.P2PMessage");
            DropForeignKey("dbo.P2PMessage", "Thread_P2PMessageThreadId", "dbo.P2PMessageThread");
            DropForeignKey("dbo.AppP2PMessage", "DeviceOwner_Id", "dbo.DeviceOwners");
            DropIndex("dbo.PushMessageQueues", new[] { "P2PMessage_P2PMessageId" });
            DropIndex("dbo.PushMessageQueues", new[] { "AppP2PMessage_Id" });
            DropIndex("dbo.P2PMessage", new[] { "Thread_P2PMessageThreadId" });
            DropIndex("dbo.AppP2PMessage", new[] { "Message_P2PMessageId" });
            DropIndex("dbo.AppP2PMessage", new[] { "DeviceOwner_Id" });
            DropColumn("dbo.ContentLibraries", "AvailableFromDate");
            DropColumn("dbo.PushMessageQueues", "P2PMessage_P2PMessageId");
            DropColumn("dbo.PushMessageQueues", "AppP2PMessage_Id");
            DropColumn("dbo.CmsMessages", "SendDate");
            DropTable("dbo.ScheduleEventLocations");
            DropTable("dbo.P2PMessageThread");
            DropTable("dbo.P2PMessage");
            DropTable("dbo.AppP2PMessage");
        }
    }
}
