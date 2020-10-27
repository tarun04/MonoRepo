namespace MonoRepo.Microservice.IdentityServer.B2B.Models
{
    public class ErrorViewModel
    {
        /// <summary>
        /// Id of the Request
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Flag representing for ShowRequestId
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
