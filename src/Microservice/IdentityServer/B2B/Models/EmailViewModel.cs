namespace MonoRepo.Microservice.IdentityServer.B2B.Models
{
    public class EmailViewModel
    {
        /// <summary>
        /// Recipient emails ; delimited.
        /// </summary>
        public string Recipients { get; set; }

        /// <summary>
        /// Subject of the email.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// HTML Body text of the email.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Weak foreign key pointing to object that is related to this email.
        /// This is used for integration events.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The product that this email request came from.
        /// This is populated by the gateway.
        /// </summary>
        public string ProductName { get; set; }
    }
}
