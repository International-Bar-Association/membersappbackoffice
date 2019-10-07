using System;
using System.Net;
using System.Net.Http;
using IBA_Common;

namespace IBAMembersApp.API
{
    public class DateHeaderCheckWebApiAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!CheckDate(actionContext.Request))
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);

            base.OnActionExecuting(actionContext);
        }

        private bool CheckDate(HttpRequestMessage request)
        {
            var date = request.Headers.Date;
            if (!date.HasValue)
                return false;

            var d = date.Value.ToUniversalTime();
            var dif = d - DateTime.UtcNow;

            if (Math.Abs(dif.TotalSeconds) > RequestDifferenceMaxSeconds)
                return false;

            return true;
        }

        private static double? _requestDifferenceMaxSeconds;
        private static double RequestDifferenceMaxSeconds
        {
            get
            {
                if (_requestDifferenceMaxSeconds.HasValue) return _requestDifferenceMaxSeconds.Value;
                var s = AppSettings.RequestDifferenceMaxSeconds;
                double requestDifferenceMaxSeconds;
                if (!double.TryParse(s, out requestDifferenceMaxSeconds))
                {
                    requestDifferenceMaxSeconds = 1800;
                }
#if DEBUG
                requestDifferenceMaxSeconds = 10000000000000000;
#endif
                _requestDifferenceMaxSeconds = requestDifferenceMaxSeconds;
                return _requestDifferenceMaxSeconds.Value;
            }
        }
    }
}