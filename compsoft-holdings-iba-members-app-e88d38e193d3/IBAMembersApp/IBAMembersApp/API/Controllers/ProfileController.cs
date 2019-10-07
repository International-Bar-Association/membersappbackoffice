using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using IBAMembersApp.API.Models.Request;
using IBAMembersApp.API.Models.Response;
using IBA_Common;

namespace IBAMembersApp.API.Controllers
{
    public class ProfileController : BaseApiController
    {
        [DateHeaderCheckWebApi]
        [XAuthHeaderCheckWebApi]
        [BasicAuthenticationWebApi(BasicAuthenticationType.Device)]
        public ProfileResponseModel Get(decimal userId)
        {
            return GetProfile(userId);
        }

        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/profile")]
        public HttpResponseMessage GetV2(decimal userId)
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }

            return Request.CreateResponse(HttpStatusCode.OK, GetProfile(userId,access));
        }

        [HttpPut]
        [DateHeaderCheckWebApi]
        [XAuthHeaderCheckWebApi]
        [BasicAuthenticationWebApi(BasicAuthenticationType.Device)]
        public async Task<ProfileUpdateSuccessModel> Put(ProfileRequestModel profile)
        {
            var username = GetUserNameFromRequest(Request);
            var user = Db.C_records.SingleOrDefault(r => r.username == username);

            ProfileUpdateSuccessModel response = new ProfileUpdateSuccessModel();
            if (profile.Biography.Length > 1024)
            {
                response.SetError(400, "Biography must be below 1024 chars in length.");
                return response;
            }
            if (user != null) user.ProfileBiography = profile.Biography;
            await Db.SaveChangesAsync();
            return response;
        }

        [HttpPut]
        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/profile")]
        public HttpResponseMessage PutV2(ProfileRequestModel profile)
        {
            if (profile.Biography.Length > 1024)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Biography must be below 1024 chars in length.");
            }
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }

            access.Session.C_records.ProfileBiography = profile.Biography;
            Db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [DateHeaderCheckWebApi]
        [XAuthHeaderCheckWebApi]
        [BasicAuthenticationWebApi(BasicAuthenticationType.Device)]
        public List<ProfileSearchResponseModel> Search([FromUri]ProfileSearchRequestModel model)
        {
            List<ProfileSearchResponseModel> response = new List<ProfileSearchResponseModel>();
            var search = Db.New_Profile_WebsiteSearch(model.Country, model.AreaOfPractice,model.Committee, model.FirstName, model.LastName,model.FirmName,model.City).ToList();

            var results = Db.C_records.Where(r => search.Contains(r.id)).ToList();
            foreach (var record in results)
            {
                var profile = GetProfile(record.id);

                response.Add(new ProfileSearchResponseModel()
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    JobPosition = profile.JobPosition,
                    FirmName = profile.FirmName,
                    ProfilePicture = profile.ProfilePicture,
                    Address = profile.Address,
                    UserId = record.id

                });
            }
            return response;
        }

        [HttpGet]
        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/profile")]
        public HttpResponseMessage SearchV2([FromUri]ProfileSearchRequestModel model)
        {
            model = model ?? new ProfileSearchRequestModel();
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid model");

            var access = UserRights.SearchDirectory(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            List<ProfileSearchResponseModel> response = new List<ProfileSearchResponseModel>();
            var prev = Db.Database.CommandTimeout;

            Db.Database.CommandTimeout = (int)1000000000;
            
            var searchv2 = Db.New_Profile_Search_Conference_Compsoft(model.Country, model.AreaOfPractice, model.Committee, model.FirstName, model.LastName, model.FirmName, model.City, model.OnlyConferenceAttendees? AppSettings.ConferenceId:(int?)null).Where(r => r.HasValue)
                .Select(r => r.Value)
                .ToList();
            Db.Database.CommandTimeout = prev;

            var results = Db.C_records.Where(r => searchv2.Contains(r.id)).ToList();
            foreach (var record in results)
            {
                var profile = GetProfile(record.id);

                response.Add(new ProfileSearchResponseModel()
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    JobPosition = profile.JobPosition,
                    FirmName = profile.FirmName,
                    ProfilePicture = profile.ProfilePicture,
                    Address = profile.Address,
                    UserId = record.id,
                    CurrentlyAttendingConference = profile.CurrentlyAttendingConference
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, response); ;
        }
    }
}