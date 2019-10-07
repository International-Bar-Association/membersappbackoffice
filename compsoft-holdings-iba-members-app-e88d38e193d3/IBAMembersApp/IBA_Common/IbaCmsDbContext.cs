using System.Data.Entity;
using IBA_Common.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IBA_Common
{
    public class IbaCmsDbContext : IdentityDbContext<IbaCmsUser>
    {
        private const string ConnectionStringName = "IBACMSConnection";

        public DbSet<ContentLibrary> ContentLibraries { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<DeviceOwner> DeviceOwners { get; set; }

        public DbSet<CmsMessage>  CmsMessages { get; set; }

        public DbSet<AppUserMessage> AppUserMessages { get; set; }
        public DbSet<P2PMessageThread> UserMessageThreads { get; set; }
        public DbSet<P2PMessage> UserMessages { get; set; }
        public DbSet<AppP2PMessage> AppP2PMessages { get; set; }
        public DbSet<ScheduleEventLocation> EventLocations { get; set; }
        public DbSet<ScheduleEventBuilding> EventBuildings { get; set; }

        public DbSet<ScheduleEventFloorName> FloorNames { get; set; }
        public IbaCmsDbContext() : base(ConnectionStringName)
        {
               
        }
    }
}
