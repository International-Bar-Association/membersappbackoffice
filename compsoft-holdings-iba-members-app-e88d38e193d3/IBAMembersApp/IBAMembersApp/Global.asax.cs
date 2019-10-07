using log4net;
using log4net.Config;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IBAMembersApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Error(Object sender, EventArgs e)
        {
            var log = LogManager.GetLogger(GetType());
            var exception = Server.GetLastError();
            if (exception is HttpException)
            {
                var httpException = exception as HttpException;
                if (httpException.GetHttpCode() == 404)
                {
                    log.Info("Application level 404", httpException);
                    return;
                }
            }
            log.Fatal("An error has occurred", exception);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            XmlConfigurator.Configure();
        }

        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }
}
