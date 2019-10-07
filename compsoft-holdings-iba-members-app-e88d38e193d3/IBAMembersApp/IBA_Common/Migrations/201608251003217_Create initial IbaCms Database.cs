namespace IBA_Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateinitialIbaCmsDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUserMessages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Read = c.DateTime(),
                        Received = c.DateTime(),
                        Deleted = c.DateTime(),
                        DeviceOwner_Id = c.Int(),
                        Message_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceOwners", t => t.DeviceOwner_Id)
                .ForeignKey("dbo.CmsMessages", t => t.Message_Id)
                .Index(t => t.DeviceOwner_Id)
                .Index(t => t.Message_Id);
            
            CreateTable(
                "dbo.DeviceOwners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IbaId = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LastDeviceUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceUUID = c.String(),
                        DeviceType = c.Int(nullable: false),
                        PushToken = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        ApiVersion = c.String(),
                        DeviceOwner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceOwners", t => t.DeviceOwner_Id)
                .Index(t => t.DeviceOwner_Id);
            
            CreateTable(
                "dbo.CmsMessages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MessageType = c.Int(nullable: false),
                        Title = c.String(),
                        Text = c.String(),
                        Url = c.String(),
                        Recipients = c.String(),
                        TotalRecipients = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        UrlOnly = c.Boolean(nullable: false),
                        Sender_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Sender_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MustResetPassword = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PushMessageQueues",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        PushStatus = c.Int(nullable: false),
                        StatusMessage = c.String(),
                        StatusDate = c.DateTime(),
                        FailedRetryCount = c.Int(nullable: false),
                        AppUserMessage_Id = c.Long(),
                        Device_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUserMessages", t => t.AppUserMessage_Id)
                .ForeignKey("dbo.Devices", t => t.Device_Id)
                .Index(t => t.AppUserMessage_Id)
                .Index(t => t.Device_Id);
            
            CreateTable(
                "dbo.ContentLibraries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        Title = c.String(),
                        Precis = c.String(),
                        ContentType = c.Int(nullable: false),
                        Url = c.String(),
                        Featured = c.Boolean(nullable: false),
                        Created = c.DateTime(),
                        Status = c.Int(nullable: false),
                        UploadedToDevice = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PushMessageQueues", "Device_Id", "dbo.Devices");
            DropForeignKey("dbo.PushMessageQueues", "AppUserMessage_Id", "dbo.AppUserMessages");
            DropForeignKey("dbo.CmsMessages", "Sender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AppUserMessages", "Message_Id", "dbo.CmsMessages");
            DropForeignKey("dbo.AppUserMessages", "DeviceOwner_Id", "dbo.DeviceOwners");
            DropForeignKey("dbo.Devices", "DeviceOwner_Id", "dbo.DeviceOwners");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PushMessageQueues", new[] { "Device_Id" });
            DropIndex("dbo.PushMessageQueues", new[] { "AppUserMessage_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CmsMessages", new[] { "Sender_Id" });
            DropIndex("dbo.Devices", new[] { "DeviceOwner_Id" });
            DropIndex("dbo.AppUserMessages", new[] { "Message_Id" });
            DropIndex("dbo.AppUserMessages", new[] { "DeviceOwner_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ContentLibraries");
            DropTable("dbo.PushMessageQueues");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CmsMessages");
            DropTable("dbo.Devices");
            DropTable("dbo.DeviceOwners");
            DropTable("dbo.AppUserMessages");
        }
    }
}
