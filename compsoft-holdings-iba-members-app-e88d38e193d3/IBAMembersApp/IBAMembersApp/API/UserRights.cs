using IBAMembersApp.Utilities;
using System.Linq;
using System.Net;
using System.Net.Http;
using IBAMembersApp.BusinessLayer.Models;

namespace IBAMembersApp.API
{
    public class UserRights
    {
        public static decimal[] V2LoginClasses = {1, 3, 4, 6, 7, 9, 12, 13, 15, 16, 23, 24, 25, 26, 27, 28};

        private static readonly decimal[] V2ViewContentClasses = V2LoginClasses;

        private static readonly decimal[] V2DirectorySearchClasses = { 1, 4, 13, 27 };

        public static RightsResponseModel GetUserSession(IBAEntities1 db, HttpRequestMessage request)
        {
            var result = new RightsResponseModel {Session = RecordSessionUtilities.SessionByToken(db, request)};
            if (result.Session == null)
            {
                // should never happen after xauth but just incase !
                result.SetError(HttpStatusCode.Forbidden, "you do not have an active Session");
                return result;
            }
            if (result.Session.C_records.status != 1)
            {
                result.SetError(HttpStatusCode.Forbidden, "User is Disabled");
            }
            return result;
        }

        private static RightsResponseModel ValidateUser(IBAEntities1 db, HttpRequestMessage request, decimal[] usersAllowedToLogin)
        {
            var response = GetUserSession(db, request);
            if (response.HasErrors)
            {
                return response;
            }

            if (!usersAllowedToLogin.Contains(response.Session.C_records.@class))
            {
                response.SetError(HttpStatusCode.Forbidden, "User is not permitted to access this feature");
            }
            return response;
        }

        public static bool CanSearchDirectory(decimal userClass)
        {
            return V2DirectorySearchClasses.Contains(userClass);
        }

        public static RightsResponseModel V2Login(IBAEntities1 db, HttpRequestMessage request)
        {
            return ValidateUser(db, request, V2LoginClasses);
        }

        public static RightsResponseModel ViewContentLibrary(IBAEntities1 db, HttpRequestMessage request)
        {
            return ValidateUser(db, request, V2ViewContentClasses);
        }

        public static RightsResponseModel SearchDirectory(IBAEntities1 db, HttpRequestMessage request)
        {
            return ValidateUser(db, request, V2DirectorySearchClasses);
        }

    }

    public class RightsResponseModel : BaseV2ResponseModel
    {
        public record_sessions Session { get; set; }
    }
}