using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace IBAMembersApp.API.Controllers
{
    public class MakeProfilePublicController : BaseApiController
    {
        [HttpPut]
        [DateHeaderCheckWebApi]
        [XAuthHeaderCheckWebApi]
        [BasicAuthenticationWebApi(BasicAuthenticationType.Device)]
        public async Task<HttpResponseMessage> Put()
        {
            var requestMsg = ActionContext.Request;
            var authHeader = requestMsg.Headers.Authorization;
            string[] credentials = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
            string username = credentials[0];
            var user = Db.C_records.SingleOrDefault(r => r.username == username);
            if (user != null)
            {
                bool makePublic = !user.ProfileMakePublic;
                user.ProfileMakePublic = makePublic;
                await Db.SaveChangesAsync();
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/makeprofilepublic")]
        public HttpResponseMessage PutV2()
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            access.Session.C_records.ProfileMakePublic = !access.Session.C_records.ProfileMakePublic;
            Db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}