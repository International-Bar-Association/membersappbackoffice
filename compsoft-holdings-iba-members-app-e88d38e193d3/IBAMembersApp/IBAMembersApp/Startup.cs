using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IBAMembersApp.Startup))]

namespace IBAMembersApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
