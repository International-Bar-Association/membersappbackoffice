using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;
using IBAMembersApp.API.Models.Response;
using IBAMembersApp.Models;
using IBA_Common;

namespace IBAMembersApp.API.Controllers
{
    public class BaseApiController : ApiController
    {
        private IbaCmsDbContext _cmsDb;

        public IbaCmsDbContext CmsDb
        {
            get
            {
                _cmsDb = _cmsDb ?? new IbaCmsDbContext();
                return _cmsDb;
            }
        }


        private IBAEntities1 _db;

        public IBAEntities1 Db
        {
            get
            {
                _db = _db ?? new IBAEntities1();
                return _db;
            }
        }
        private static string _dataDirectory;
        protected static string DataDirectory
        {
            get
            {
                if (_dataDirectory == null)
                {
                    _dataDirectory = AppSettings.DataDirectory;
                    if (_dataDirectory.Contains("~"))
                        _dataDirectory = HostingEnvironment.MapPath(_dataDirectory);
                    if (_dataDirectory != null && !_dataDirectory.EndsWith("/"))
                        _dataDirectory += "/";
                }
                return _dataDirectory;
            }
        }

        protected internal const string RegisterFileExt = ".txt";
        protected internal const string RegisterFile = "register" + RegisterFileExt;

        private static string RegisterDirectory
        {
            get { return DataDirectory + "register/"; }
        }

        /// <summary>
        /// Returns the path to the directory for the registration of the given device.
        /// </summary>
        public static DirectoryInfo GetRegisterFolder(string deviceId, bool create = true)
        {
            return GetDeviceFolder(RegisterDirectory, deviceId, create);
        }

        private static DirectoryInfo GetDeviceFolder(string folder, string deviceId, bool create)
        {
            var dir = new DirectoryInfo(Path.Combine(folder, deviceId));
            if (create && !dir.Exists)
            {
                dir.Create();
            }
            return dir;
        }

        protected static string GetUserNameFromRequest(HttpRequestMessage request)
        {
            var authHeader = request.Headers.Authorization;
            string[] credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
            return credentials[0];
        }

        protected ProfileResponseModel GetProfile(decimal id)
        {
            ProfileResponseModel model = new ProfileResponseModel();
            var user = Db.C_records.SingleOrDefault(r => r.id == id);
            if (user == null)
            {
                model.HasError();
                model.SetError(404, "User not found");
                return model;
            }
            var userAddress = Db.gen_addresses.Where(r => r.type == 4).SingleOrDefault(p => p.record == user.id);
            var userAreaOfPractice = Db.mem_members_areasofpractice.Where(r => r.record == user.id);

            var types = new decimal[] { 1, 3, 7, 11, 12 };
            var sections = new decimal[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 19, 20, 21 };
            var userCommitees = Db.mem_members_committees.Where(r => types.Contains(r.committee_type)).Where(p => sections.Contains(p.section)).Where(p => p.committee != 143).Where(t => t.record == user.id).ToList();


            model.FirstName = user.given_name;
            model.Id = user.id;
            model.Access.Class = user.@class;
            model.Access.CanSearchDirectory = UserRights.CanSearchDirectory(model.Access.Class);
            model.LastName = user.family_name;
            model.JobPosition = user.ProfileJobTitle;
            model.FirmName = user.lawfirm;
            model.Public = user.ProfileMakePublic;
            model.Address = new ProfileAddress();
            if (userAddress != null)
            {
                model.Address.AddressLines.Insert(0, userAddress.address1);
                model.Address.AddressLines.Insert(1, userAddress.address2);
                model.Address.AddressLines.Insert(2, userAddress.address3);
                model.Address.County = userAddress.county;
                model.Address.PcZip = userAddress.pc_zip;
                model.Email = userAddress.email;
                if (!string.IsNullOrEmpty(userAddress.telephone))
                {
                    var dbcountry = Db.gen_countries.FirstOrDefault(r => r.id == userAddress.country);
                    model.Phone = dbcountry != null ? $"+{dbcountry.dial_code}{userAddress.telephone}" : userAddress.telephone;
                }
            }
            var city = Db.gen_cities.SingleOrDefault(a => a.id == userAddress.city);
            if (city != null)
            {
                model.Address.City = city.description;
            }
            var country = Db.gen_countries.SingleOrDefault(a => a.id == userAddress.country);
            if (country != null)
            {
                model.Address.Country = country.description;
            }

            var state = Db.gen_states.SingleOrDefault(a => a.id == userAddress.state);
            if (state != null)
            {
                model.Address.State = state.description;
            }

            model.Biography = user.ProfileBiography;
            var imageLocations = AppSettings.ProfileImageAddress;
            model.ProfilePicture = user.ProfileImageName != null ? String.Format("{0}/{1}", imageLocations, user.ProfileImageName) : "N/A";
            model.Committees = userCommitees.Select(c => c.committee).ToList();
            model.AreasOfPractice = userAreaOfPractice.Select(aop => aop.areasofpractice).ToList();
            var confdelegate =
           Db.conf_delegate.FirstOrDefault(
               c =>
                   c.record_id == user.id && c.conference_id == AppSettings.ConferenceId && c.status_id == 1);

            model.CurrentlyAttendingConference = confdelegate != null && AppSettings.ShowConferenceDetails ? AppSettings.ConferenceId : (decimal?)null;

            return model;
        }

        protected ProfileResponseModel GetProfile(decimal id, RightsResponseModel access)
        {
            ProfileResponseModel model = new ProfileResponseModel();
            var user = Db.C_records.SingleOrDefault(r => r.id == id);
            if (user == null)
            {
                model.HasError();
                model.SetError(404, "User not found");
                return model;
            }
            var userAddress = Db.gen_addresses.Where(r => r.type == 4).SingleOrDefault(p => p.record == user.id);
            var userAreaOfPractice = Db.mem_members_areasofpractice.Where(r => r.record == user.id);

            var types = new decimal[] { 1, 3, 7, 11, 12 };
            var sections = new decimal[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 19, 20, 21 };
            var userCommitees = Db.mem_members_committees.Where(r => types.Contains(r.committee_type)).Where(p => sections.Contains(p.section)).Where(p => p.committee != 143).Where(t => t.record == user.id).ToList();


            model.FirstName = user.given_name;
            model.Id = user.id;
            model.Access.Class = user.@class;
            model.Access.CanSearchDirectory = UserRights.CanSearchDirectory(model.Access.Class);
            model.LastName = user.family_name;
            model.JobPosition = user.ProfileJobTitle;
            model.FirmName = user.lawfirm;
            model.Public = user.ProfileMakePublic;
            model.Address = new ProfileAddress();
            if (userAddress != null)
            {
                model.Address.AddressLines.Insert(0, userAddress.address1);
                model.Address.AddressLines.Insert(1, userAddress.address2);
                model.Address.AddressLines.Insert(2, userAddress.address3);
                model.Address.County = userAddress.county;
                model.Address.PcZip = userAddress.pc_zip;
                model.Email = userAddress.email;
                if (!string.IsNullOrEmpty(userAddress.telephone))
                {
                    var dbcountry = Db.gen_countries.FirstOrDefault(r => r.id == userAddress.country);
                    model.Phone = dbcountry != null ? $"+{dbcountry.dial_code}{userAddress.telephone}" : userAddress.telephone;
                }
            }
            var city = Db.gen_cities.SingleOrDefault(a => a.id == userAddress.city);
            if (city != null)
            {
                model.Address.City = city.description;
            }
            var country = Db.gen_countries.SingleOrDefault(a => a.id == userAddress.country);
            if (country != null)
            {
                model.Address.Country = country.description;
            }

            var state = Db.gen_states.SingleOrDefault(a => a.id == userAddress.state);
            if (state != null)
            {
                model.Address.State = state.description;
            }

            model.Biography = user.ProfileBiography;
            var imageLocations = AppSettings.ProfileImageAddress;
            model.ProfilePicture = user.ProfileImageName != null ? String.Format("{0}/{1}", imageLocations, user.ProfileImageName) : "N/A";
            model.Committees = userCommitees.Select(c => c.committee).ToList();
            model.AreasOfPractice = userAreaOfPractice.Select(aop => aop.areasofpractice).ToList();
            var confdelegate =
           Db.conf_delegate.FirstOrDefault(
               c =>
                   c.record_id == user.id && c.conference_id == AppSettings.ConferenceId && c.status_id == 1);

            var myDelegate = Db.conf_delegate.FirstOrDefault(
                c =>
                    c.record_id == access.Session.record_id && c.conference_id == AppSettings.ConferenceId && c.status_id == 1);


            model.CurrentlyAttendingConference = confdelegate != null && myDelegate != null ? AppSettings.ConferenceId : (decimal?)null;

            return model;
        }
    }
}