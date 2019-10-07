using System.Linq;
using IBAMembersApp.API.Models.Response;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using IBA_Common;
using System.Collections.Generic;

namespace IBAMembersApp.API.Controllers
{
    public class RefreshController : BaseApiController
    {
        private RefreshDataResponseModel RefreshData(decimal userClass)
        {
            decimal[] V2ConfTypes = { 1, 2, 3, 4, 7};

            var types = new decimal[] { 1, 3, 7, 11, 12 };
            var sections = new decimal[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 18, 19, 20, 21 };
            var committees = Db.gen_committees.Where(r => types.Contains(r.type)).Where(p => sections.Contains(p.section)).Where(p => p.id != 143).Where(t => t.active).OrderBy(t => t.description).ToList();
            RefreshDataResponseModel response = new RefreshDataResponseModel()
            {
                AreasOfPracticeDict = Db.mem_areasofpractice.ToDictionary(r => r.id, t => t.description),
                CommitteesDict = committees.ToDictionary(g => g.id, r => r.description)

            };
            var access = UserRights.V2Login(Db, Request);
            //var confdelegate =
            //    Db.conf_delegate.FirstOrDefault(
            //        c =>
            //            c.record_id == access.Session.record_id && c.conference_id == 637 && c.status_id == 1 &&
            //            V2ConfTypes.Contains(c.type_id));

           var profile = GetProfile(access.Session.record_id);

            var ImAttending = Db.conf_delegate.Any(r => r.conference_id == AppSettings.ConferenceId && r.record_id == profile.Id);
            IEnumerable<string> headerValues;
            var correctAppVersion = false;
            if (Request.Headers.TryGetValues("AppVersion",out headerValues)) {
                var version = headerValues.FirstOrDefault();
                correctAppVersion = version != null;
                if (correctAppVersion)
                {
                    double doubleVersion;
                    double.TryParse(version, out doubleVersion);
                    if (doubleVersion != 0)
                    {
                        correctAppVersion = doubleVersion >= 3;
                    }
                    else
                    {
                        correctAppVersion = false;
                    }

                }
            }
            
            if (AppSettings.ShowConferenceDetails && ImAttending && correctAppVersion)
            {
                var conf = Db.conf_conference.SingleOrDefault(r => r.id == AppSettings.ConferenceId);
                
                response.Conference = new Conference();
                response.Conference.ShowDetails = true;
                response.Conference.Url = AppSettings.ConferenceUrl;
                response.Conference.StartDate = AppSettings.ConferenceStart; 
                response.Conference.FinishDate = AppSettings.ConferenceEnd; //conf.end_date;
                response.Conference.ConferenceId = AppSettings.ConferenceId;
            }
            
            return response;
        }

        [DateHeaderCheckWebApi]
        [XAuthHeaderCheckWebApi()]
        [BasicAuthenticationWebApi(BasicAuthenticationType.Device)]
        public RefreshDataResponseModel Get()
        {
            return RefreshData(0);
        }

        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/refresh")]
        public HttpResponseMessage GetV2()
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            return Request.CreateResponse(HttpStatusCode.OK, RefreshData(access.Session.C_records.@class));
        }
    }
}
