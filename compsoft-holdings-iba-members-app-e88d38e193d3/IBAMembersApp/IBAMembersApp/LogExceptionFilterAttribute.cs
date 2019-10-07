using log4net;
using System.Web.Http.Filters;

namespace IBAMembersApp
{
    public class LogExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var log = LogManager.GetLogger(GetType());
            log.Fatal("An error has occurred", context.Exception);
        }
    }
}
