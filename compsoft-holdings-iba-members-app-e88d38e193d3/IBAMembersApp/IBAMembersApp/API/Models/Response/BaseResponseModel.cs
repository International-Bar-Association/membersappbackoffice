using System;
using Newtonsoft.Json;

namespace IBAMembersApp.API.Models.Response
{
    /// <summary>
    /// Base class for all response models
    /// </summary>
    public class BaseResponseModel
    {
        /// <summary>
        /// Error that is populated if something went wrong
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ResponseErrorModel ResponseError { get; set; }

        public void SetError<T>(T errorCode) where T : struct
        {
            if (typeof(T).IsEnum)
                SetError(Convert.ToInt32(errorCode), "");
        }

        public void SetError(int code, string message)
        {
            var responseError = new ResponseErrorModel();
            responseError.Code = code;
            responseError.Message = message;

            ResponseError = responseError;
        }

        public bool HasError()
        {
            return (ResponseError != null);
        }
    }
}