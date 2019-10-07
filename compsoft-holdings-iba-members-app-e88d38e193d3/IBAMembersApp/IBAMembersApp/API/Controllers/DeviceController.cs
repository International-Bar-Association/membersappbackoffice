using System.Net;
using System.Net.Http;
using System.Web.Http;
using IBAMembersApp.BusinessLayer;
using IBAMembersApp.BusinessLayer.Models;

namespace IBAMembersApp.API.Controllers
{
    public class DeviceController : BaseApiController
    {
        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/device")]

        public HttpResponseMessage Put(DeviceUpdateRequestModel model)
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }

            var update=DeviceLayer.UpdateDevice(CmsDb, model, access.Session.record_id);
            if (update.HasErrors)
            {
                return Request.CreateErrorResponse(update.ErrorCode, update.ErrorMessage);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}