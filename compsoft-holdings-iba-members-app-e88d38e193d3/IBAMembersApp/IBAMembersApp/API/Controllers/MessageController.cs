using System.Net;
using System.Net.Http;
using System.Web.Http;
using IBAMembersApp.BusinessLayer;
using IBAMembersApp.BusinessLayer.Models;

namespace IBAMembersApp.API.Controllers
{
    public class MessageController : BaseApiController
    {
        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/message")]
        public HttpResponseMessage Get([FromUri]MessageRequestModel model)
        {
            if (model == null)
            {
                model = new MessageRequestModel();
            }
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            var result = MessageLayer.GetMessagesFromData(CmsDb, access.Session.record_id, model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/message")]
        public HttpResponseMessage Put(UpdateMessageStatusModel model)
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            var ok = MessageLayer.LogMessageStatus(CmsDb, model,access.Session.record_id);
            return Request.CreateResponse(ok?HttpStatusCode.OK:HttpStatusCode.InternalServerError);
        }

    }
}
