using log4net;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IBAMembersApp.API
{
    public class BasicAuthenticationMvcAttribute : ActionFilterAttribute
    {
        private BasicAuthenticationType type;

        public BasicAuthenticationMvcAttribute(BasicAuthenticationType type)
        {
            this.type = type;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            BasicAuthentication auth;
            switch (type)
            {
                default:
                    auth = new DeviceAuthentication();
                    break;
            }

            if (!auth.Authenticated(filterContext.HttpContext.Request))
                filterContext.Result = new HttpUnauthorizedResult();

            base.OnActionExecuting(filterContext);
        }
    }

    public class BasicAuthenticationWebApiAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private BasicAuthenticationType type;

        public BasicAuthenticationWebApiAttribute(BasicAuthenticationType type)
        {
            this.type = type;
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            BasicAuthentication auth;
            switch (type)
            {
               
                default:
                    auth = new DeviceAuthentication();
                    break;
            }

            if (!auth.Authenticated(actionContext.Request))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

                var log = LogManager.GetLogger(GetType());
                log.Info("HttpStatusCode.Unauthorized");
            }

            base.OnActionExecuting(actionContext);
        }
    }

    public enum BasicAuthenticationType
    {
        Device
    }

    class DeviceAuthentication : BasicAuthentication
    {

        protected override bool CheckPassword(string username, string password)
        {
            return CheckUser(username, password);
        }

        /// <summary>
        /// Is the given device Id and token combination correct?
        /// </summary>
        public static bool CheckUser(string userId, string token)
        {
            using (var db = new IBAEntities1())
            {
                var user = db.C_records.SingleOrDefault(r=>r.username == userId);
                return user?.password == token;
            }
        }
    }

    abstract class BasicAuthentication
    {
        public const string AuthUserId = "authUserId";
        private static readonly string AuthHeaderKey = "Authorization";
        private static readonly string BasicAuthKey = "Basic";
        private static readonly char[] AuthSchemeSeperator = { ' ' };
        private static readonly char[] Separator = { ':' };

        internal bool Authenticated(HttpRequestBase httpRequestBase)
        {
            try
            {
                var authHeader = httpRequestBase.Headers[AuthHeaderKey];
                if (!string.IsNullOrWhiteSpace(authHeader))
                {
                    var authParts = authHeader.Split(AuthSchemeSeperator);
                    if (authParts.Length == 2)
                    {
                        return IsAuthenticated(authParts[0], authParts[1]);
                    }
                }
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }

        internal bool Authenticated(HttpRequestMessage request)
        {
            try
            {
                var authHeader = request.Headers.Authorization;
                return IsAuthenticated(authHeader.Scheme, authHeader.Parameter);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool IsAuthenticated(string authHeaderScheme, string authHeaderValue)
        {
            bool authenticated = false;

            if (authHeaderScheme.StartsWith(BasicAuthKey, StringComparison.InvariantCultureIgnoreCase))
            {
                if (!string.IsNullOrEmpty(authHeaderValue))
                {
                    string[] credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderValue)).Split(Separator);

                    if (credentials.Length == 2 && CheckPassword(credentials[0], credentials[1]))
                    {
                        authenticated = true;
                        HttpContext.Current.Items[AuthUserId] = credentials[0];
                    }
                }
            }

            return authenticated;
        }

        protected abstract bool CheckPassword(string username, string password);
    }
}