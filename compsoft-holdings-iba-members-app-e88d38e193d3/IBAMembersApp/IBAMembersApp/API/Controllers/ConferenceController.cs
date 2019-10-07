using IBAMembersApp.BusinessLayer;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IBAMembersApp.API.Controllers
{
    public class ConferenceController : BaseApiController
    {
        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/Conference")]
        //Returns the conference the user is attending next.
        public HttpResponseMessage Get(int id)
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            var result = ConferenceLayer.GetConferenceById(Db, id);
            if (result.Start.HasValue && result.End.HasValue)
            {
                var start = result.Start.Value;
                var end = result.End.Value;
                if(start < DateTime.UtcNow && end > DateTime.UtcNow)
                {
                    //Conference is Live
                    return Request.CreateResponse(HttpStatusCode.OK, result);

                }
            }
            return Request.CreateResponse(HttpStatusCode.NoContent, result);

        }

        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/Conference/{id}/events")]
        //Returns the conference the user is attending next.
        public HttpResponseMessage GetEvents(int id, int take = 20, int skip = 0)
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            var result = ConferenceLayer.GetEventsForConferenceById(CmsDb, Db, id, access.Session.record_id, take, skip);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/Conference/{id}/buildingEvents")]
        public HttpResponseMessage GetConferenceBuildingEvent(int id, int take = 20, int skip = 0)
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            var result = ConferenceLayer.GetConferenceWithBuildingEventsById(CmsDb, Db, id, access.Session.record_id, take, skip);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
