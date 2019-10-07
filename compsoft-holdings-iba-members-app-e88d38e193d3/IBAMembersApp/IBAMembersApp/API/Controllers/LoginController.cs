using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using IBAMembersApp.API.Models.Response;
using IBAMembersApp.Utilities;

namespace IBAMembersApp.API.Controllers
{
    public class LoginController : BaseApiController
    {
        [DateHeaderCheckWebApi]
        [XAuthHeaderForUserLogin]
        [BasicAuthenticationWebApi(BasicAuthenticationType.Device)]
        [Route("api/v2/login")]
        [HttpPut]
        public LoginResponseModel PutV2()
        {
            LoginResponseModel model = new LoginResponseModel();
            try
            {
                var username = GetUserNameFromRequest(Request);

                var record = Db.C_records.FirstOrDefault(r => r.username == username);

                if (record == null || record.status != 1 || !UserRights.V2LoginClasses.Contains(record.@class))
                {
                    model.SetError(403, "User is not permitted to log in through the app.");
                    return model;
                }

                //If the user logs in and they already have an active session, update the session
                //expiry. Otherwise create a new session.
                var session = RecordSessionUtilities.ActiveSessionByUserId(Db, record.id);

                if (session == null)
                {
                    session = new record_sessions
                    {
                        session_token = GenerateTokenForUser(),
                        C_records = record,
                        session_expiry = DateTime.UtcNow.Add(RecordSessionUtilities.SlidingSessionExpiryInDays),
                        client_platform = string.Join(" ", Request.Headers.UserAgent),
                        initial_logon = DateTime.UtcNow
                    };
                    Db.record_sessions.Add(session);
                    Db.SaveChanges();
                }
                else
                {
                    var newExpiry = session.session_expiry.Add(RecordSessionUtilities.SlidingSessionExpiryInDays);
                    session.session_expiry = newExpiry;
                    Db.SaveChanges();
                }

                model.SessionToken = session.session_token;
                model.Profile = GetProfile(record.id);
            }
            catch (Exception e)
            {
                model.SetError(500,$"Exception {e} was thrown.");
            }
            return model;
        }

        [DateHeaderCheckWebApi]
        [XAuthHeaderCheckWebApi(false)]
        [BasicAuthenticationWebApi(BasicAuthenticationType.Device)]
        public async Task<LoginResponseModel> Put()
        {
            LoginResponseModel model = new LoginResponseModel();
            try
            {
                var username = GetUserNameFromRequest(Request);
                decimal[] usersAllowedToLogin = new decimal[] { 1, 4, 13, 27 };

                var filteredRecords = Db.C_records.Where(r => usersAllowedToLogin.Contains(r.@class));

                var record = filteredRecords.FirstOrDefault(r => r.username == username);
                if (record == null || record.status != 1)
                {
                    model.ResponseError.Code = 403;
                    model.ResponseError.Message = "User is not permitted to log in through the app.";
                    return model;
                }

                //If the user logs in and they already have an active session, update the session
                //expiry. Otherwise create a new session.
                var session = RecordSessionUtilities.ActiveSessionByUserId(Db, record.id);

                if (session == null)
                {
                    session = new record_sessions
                    {
                        session_token = GenerateTokenForUser(),
                        C_records = record,
                        session_expiry = DateTime.UtcNow.Add(RecordSessionUtilities.SlidingSessionExpiryInDays),
                        client_platform = String.Join(" ", Request.Headers.UserAgent),
                        initial_logon = DateTime.UtcNow
                    };
                    Db.record_sessions.Add(session);
                    await Db.SaveChangesAsync();
                }
                else
                {
                    var newExpiry = session.session_expiry.Add(RecordSessionUtilities.SlidingSessionExpiryInDays);
                    session.session_expiry = newExpiry;
                    await Db.SaveChangesAsync();
                }

                model.SessionToken = session.session_token;
                model.Profile = GetProfile(record.id);
                return model;
            }
            catch (Exception e)
            {
                model.ResponseError.Code = 500;
                model.ResponseError.Message = $"Exception {e} was thrown.";
                return model;
            }
        }

        [DateHeaderCheckWebApi]
        [XAuthHeaderCheckWebApi(false)]
        [BasicAuthenticationWebApi(BasicAuthenticationType.Device)]
        [HttpDelete]
        public async Task<bool> Delete()
        {
            var username = GetUserNameFromRequest(Request);
            var record = Db.C_records.FirstOrDefault(r => r.username == username);
            if (record != null)
            {
                var session = RecordSessionUtilities.ActiveSessionByUserId(Db, record.id);
                if (session != null)
                {
                    session.logout_time = DateTime.UtcNow;
                    await Db.SaveChangesAsync();
                }
            }

            return true;
        }

        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/login")]
        [HttpDelete]
        public bool DeleteV2([FromUri]string uuid)
        {
            RightsResponseModel rights = UserRights.GetUserSession(Db, Request);
            if (rights.Session != null)
            {
                rights.Session.logout_time = DateTime.UtcNow;
                var deviceForThisUuid =
                    CmsDb.Devices.FirstOrDefault(
                        d => d.DeviceUUID == uuid && d.DeviceOwner.IbaId == rights.Session.record_id);
                if (deviceForThisUuid != null)
                {
                    deviceForThisUuid.PushToken = "";
                    CmsDb.SaveChanges();
                }
                Db.SaveChanges();
            }
            return true;
        }

        private Guid GenerateTokenForUser()
        {
            var token = Guid.NewGuid();
            return token;
        }
    }
}