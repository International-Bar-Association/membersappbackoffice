namespace IBAMembersApp.API.Models.Response
{
    /// <summary>
    /// Model that represents an error
    /// </summary>
    public class ResponseErrorModel
    {
        /// <summary>
        /// The error code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        public string Message { get; set; }
    }
}
