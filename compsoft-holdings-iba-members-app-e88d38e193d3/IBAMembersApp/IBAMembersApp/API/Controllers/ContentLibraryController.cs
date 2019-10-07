using System.Net;
using System.Net.Http;
using System.Web.Http;
using IBAMembersApp.BusinessLayer;
using IBAMembersApp.BusinessLayer.Models;

namespace IBAMembersApp.API.Controllers
{
    public class ContentLibraryController : BaseApiController
    {
        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/contentlibrary")]

        public HttpResponseMessage Get([FromUri]ContentLibraryRequestModel model)
        {
            if (model == null)
            {
                model = new ContentLibraryRequestModel();
            }
            var access = UserRights.ViewContentLibrary(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            var result = ContentLibraryLayer.RequestContent(CmsDb, model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}