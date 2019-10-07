using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using IBAMembersApp.API.Models.Response;

namespace IBAMembersApp.API.Controllers
{
    public class ProfileImageController : BaseApiController
    {
        [HttpPut]
        [DateHeaderCheckWebApi]
        [XAuthHeaderCheckWebApi]
        [BasicAuthenticationWebApi(BasicAuthenticationType.Device)]
        public async Task<ProfileUpdateSuccessModel> Put()
        {
            ProfileUpdateSuccessModel response = new ProfileUpdateSuccessModel();
            var username = GetUserNameFromRequest(Request);
            var user = Db.C_records.SingleOrDefault(r => r.username == username);
            if (user == null)
            {
                response.SetError(404, "User Not Found");
                return response;
            }

            var readTask = await Request.Content.ReadAsMultipartAsync();
            foreach (var content in readTask.Contents)
            {
                string fieldName = (content.Headers.ContentDisposition.Name ?? "").Trim('"');

                if ("ProfileImage".Equals(fieldName))
                {
                    var currentFileData = await content.ReadAsByteArrayAsync();
                    var fileName = (content.Headers.ContentDisposition.FileName ?? "").Trim('"');
                    var fileFormat = fileName.Split('.')[1];
                    MemoryStream ms = new MemoryStream(currentFileData);
                    Image returnImage = Image.FromStream(ms);
                    if (returnImage.Size.Height != 100 || returnImage.Size.Width != 75)
                    {
                        response.SetError(400, "Image dimensions must be 75x100");
                        return response;
                    }
                    fileName = $"{user.id}.{fileFormat}";
                    try
                    {
                        var fileToSave = new FileInfo(Path.Combine(DataDirectory, fileName));
                        File.WriteAllBytes(fileToSave.FullName, currentFileData);
                    }
                    catch (Exception e)
                    {
                        response.SetError(500,
                            $"{"Error when trying to write the bytes"}-{e.InnerException}-{e.Message}");
                    }
                    user.ProfileImageName = fileName;
                    await Db.SaveChangesAsync();

                }
                else
                {
                    response.SetError(404, "Photo not found");
                }

            }

            return response;
        }

        [HttpPut]
        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/profileimage")]
        public async Task<HttpResponseMessage> PutV2()
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }

            var readTask = await Request.Content.ReadAsMultipartAsync();
            foreach (var content in readTask.Contents)
            {
                string fieldName = (content.Headers.ContentDisposition.Name ?? "").Trim('"');

                if ("ProfileImage".Equals(fieldName))
                {
                    var currentFileData = await content.ReadAsByteArrayAsync();
                    var fileName = (content.Headers.ContentDisposition.FileName ?? "").Trim('"');
                    var fileFormat = fileName.Split('.')[1];
                    MemoryStream ms = new MemoryStream(currentFileData);
                    Image returnImage = Image.FromStream(ms);
                    if (returnImage.Size.Height != 100 || returnImage.Size.Width != 75)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Image dimensions must be 75x100");
                    }
                    fileName = $"{access.Session.C_records.id}.{fileFormat}";
                    try
                    {
                        var fileToSave = new FileInfo(Path.Combine(DataDirectory, fileName));
                        File.WriteAllBytes(fileToSave.FullName, currentFileData);
                    }
                    catch (Exception e)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            $"{"Error when trying to write the bytes"}-{e.InnerException}-{e.Message}");
                    }
                    access.Session.C_records.ProfileImageName = fileName;
                    await Db.SaveChangesAsync();

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Photo not found");
                }

            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}