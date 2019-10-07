using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using IBAMembersApp.Utilities;
using IBA_Common;
using Microsoft.Ajax.Utilities;
using Compsoft.ApiLib;
using log4net;

namespace IBAMembersApp.API
{
    public class XAuthHeaderForUserLoginAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private XAuthHeaderCheckAttribute _headerCheck;

        public XAuthHeaderForUserLoginAttribute()
        {
            _headerCheck = new XAuthHeaderCheckAttribute(AppSettings.AuthApiKey);
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            _headerCheck.OnActionExecuting(actionContext);
            if (HttpStatusCode.Forbidden.Equals(actionContext?.Response?.StatusCode))
            {
                var log = LogManager.GetLogger(GetType());
                log.Info("HttpStatusCode.Forbidden");
            }
        }
    }

    public class XAuthHeaderUsingTokenAttribute : BaseV2XauthHeader
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!CheckAuth(actionContext.Request, actionContext.RequestContext.VirtualPathRoot, GetKey(actionContext.Request)))
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);

            base.OnActionExecuting(actionContext);
        }

        private string GetKey(HttpRequestMessage requestMsg)
        {
            var sessionToken = requestMsg.Headers.GetValues("UserKey").FirstOrDefault();
            if (!sessionToken.IsNullOrWhiteSpace())
            {
                var loggedIn = CheckUserIsLoggedInSession(sessionToken);
                if (loggedIn)
                    return "UserKey" + " " + sessionToken;
            }
            return "";
        }

        private static bool CheckUserIsLoggedInSession(string userKey)
        {
            using (var db = new IBAEntities1())
            {
                var session = RecordSessionUtilities.ActiveSessionByToken(db, userKey);
                if (session == null)
                {
                    return false;
                }
                TimeSpan timeDiff = session.session_expiry - DateTime.UtcNow;
                var timeToAdd = RecordSessionUtilities.SlidingSessionExpiryInDays - timeDiff;
                var newExpiry = session.session_expiry.Add(timeToAdd);
                session.session_expiry = newExpiry;
                db.SaveChanges();
            }
            return true;
        }
    }

    public class BaseV2XauthHeader : System.Web.Http.Filters.ActionFilterAttribute
    {
        protected string GetDate(HttpRequestMessage requestMsg)
        {
            return requestMsg.Headers.First(k => k.Key == "Date").Value.First();
        }

        protected string GetPath(HttpRequestMessage requestMsg, string url)
        {
            var path = requestMsg.RequestUri.AbsolutePath;
            if ("/".Equals(url))
                return path;
            else
                return path.Replace(url, "");
        }

        protected string SignRequest(HttpRequestMessage requestMsg, string url, string key)
        {
            StringBuilder sbSignature = new StringBuilder();
            sbSignature.Append(GetPath(requestMsg, url));
            sbSignature.Append(" ");
            sbSignature.Append(GetDate(requestMsg));
            sbSignature.Append(" ");
            sbSignature.Append(key);

            byte[] digest = new HMACSHA256(Encoding.UTF8.GetBytes(AppSettings.AuthApiKey)).ComputeHash(Encoding.UTF8.GetBytes(sbSignature.ToString()));
            return Convert.ToBase64String(digest);
        }

        protected bool CheckAuth(HttpRequestMessage request, string url,string key)
        {
            var xAuth = request.Headers.Where(h => h.Key == "X-Auth").Select(h => h.Value).SingleOrDefault();
            if (xAuth == null)
                return false;

            string auth = xAuth.First();
            if (string.IsNullOrWhiteSpace(auth))
                return false;

            string signiture = SignRequest(request, url, key);

            return auth == signiture;
        }
    }
}
