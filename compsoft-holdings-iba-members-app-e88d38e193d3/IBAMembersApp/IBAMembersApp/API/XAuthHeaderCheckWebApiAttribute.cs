using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IBAMembersApp.Utilities;
using IBA_Common;

namespace IBAMembersApp.API
{
    public class XAuthHeaderCheckWebApiAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private readonly bool _requiresKey;

        public XAuthHeaderCheckWebApiAttribute(bool requiresKey = true)
        {
            _requiresKey = requiresKey;
        }

        public override async void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!await CheckAuth(actionContext.Request, actionContext.RequestContext.VirtualPathRoot))
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);

            base.OnActionExecuting(actionContext);
        }

        private async Task<bool> CheckAuth(HttpRequestMessage request, string url)
        {
            var xAuth = request.Headers.Where(h => h.Key == "X-Auth").Select(h => h.Value).SingleOrDefault();
            if (xAuth == null)
                return false;

            string auth = xAuth.First();
            if (string.IsNullOrWhiteSpace(auth))
                return false;

            // Check signiture
            string signiture = await SignRequest(request, url);

            return auth == signiture;

        }

        private async Task<string> SignRequest(HttpRequestMessage requestMsg, string url)
        {
            StringBuilder sbSignature = new StringBuilder();
            sbSignature.Append(GetPath(requestMsg, url));
            sbSignature.Append(" ");
            sbSignature.Append(GetDate(requestMsg));
            sbSignature.Append(" ");
            if (_requiresKey)
            {
                sbSignature.Append(await GetKey(requestMsg));
            }
            else
            {
                sbSignature.Append(GetAuth(requestMsg));
            }
            
            
            byte[] digest = new HMACSHA256(Encoding.UTF8.GetBytes(AppSettings.AuthApiKey)).ComputeHash(Encoding.UTF8.GetBytes(sbSignature.ToString()));
            return Convert.ToBase64String(digest);
        }

        private string GetPath(HttpRequestMessage requestMsg, string url)
        {
            var path = requestMsg.RequestUri.AbsolutePath;
            if ("/".Equals(url))
                return path;
            else
                return path.Replace(url, "");
        }

        private string GetDate(HttpRequestMessage requestMsg)
        {
            return requestMsg.Headers.First(k => k.Key == "Date").Value.First();
        }

        private string GetAuth(HttpRequestMessage requestMsg)
        {
            var authHeader = requestMsg.Headers.Authorization;
            if (authHeader != null)
                return authHeader.Scheme + " " + authHeader.Parameter;
            else
                return "";
        }

        private async Task<string> GetKey(HttpRequestMessage requestMsg)
        {
            var sessionToken = requestMsg.Headers.GetValues("UserKey").FirstOrDefault();
            var authHeader = requestMsg.Headers.Authorization;
            string[] credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');

            if (sessionToken != null)
            {
                var loggedIn = await CheckUserIsLoggedInSession(sessionToken, credentials[0]);
                if (loggedIn)
                    return "UserKey" + " " + sessionToken;
            }

            return "";
        }

        private async Task<bool> CheckUserIsLoggedInSession(string userKey, string username)
        {

            var db = new IBAEntities1();
           
            var record = db.C_records.FirstOrDefault(r => r.username == username);
            var session = RecordSessionUtilities.ActiveSessionByUserId(db, record.id);
            if (session == null)
            {
                return false;
            }
            else if(userKey != session.session_token.ToString())
            {
                return false;
            }
            else
            {
                TimeSpan timeDiff = session.session_expiry - DateTime.UtcNow;
                var timeToAdd = RecordSessionUtilities.SlidingSessionExpiryInDays - timeDiff;
                var newExpiry = session.session_expiry.Add(timeToAdd);
                session.session_expiry = newExpiry;
                db.SaveChanges();
            }

            return true;            
        }
    }
}
