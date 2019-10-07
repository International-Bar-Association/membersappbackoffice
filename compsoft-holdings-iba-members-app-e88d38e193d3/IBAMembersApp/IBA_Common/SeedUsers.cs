using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IBA_Common
{
    public class SeedUsers
    {
        private static void CheckUser(IbaCmsDbContext db, UserManager<IbaCmsUser> userManager, string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                return;
            }
            if (db.Users.Any(u => u.UserName == name))
            {
                return;
            }
            var user = new IbaCmsUser { UserName = name };
            userManager.Create(user, password);
        }

        public static void CheckAndAddUsers(IbaCmsDbContext db)
        {
            var userStore = new UserStore<IbaCmsUser>(db);
            var userManager = new UserManager<IbaCmsUser>(userStore);
            CheckUser(db, userManager, AppSettings.SuperAdminName, AppSettings.SuperAdminPassword);
        }

    }
}
