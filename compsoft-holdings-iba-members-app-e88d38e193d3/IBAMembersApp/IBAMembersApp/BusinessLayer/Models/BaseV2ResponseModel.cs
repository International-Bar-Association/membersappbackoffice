using System.Net;

namespace IBAMembersApp.BusinessLayer.Models
{
    public class BaseV2ResponseModel
    {
        public HttpStatusCode ErrorCode { get; private set; }

        public string ErrorMessage { get; private set; }

        public bool HasErrors { get; private set; }

        public void SetError(HttpStatusCode code, string message)
        {
            ErrorCode = code;
            ErrorMessage = message;
            HasErrors = true;
        }

    }
}