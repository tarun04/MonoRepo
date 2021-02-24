using MonoRepo.Framework.Core.Domain;
using System;

namespace MonoRepo.Microservice.Application.Domain.Entities
{
    public class Admin : Entity
    {
        /// <summary>
        /// Admin unique identifier
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// IdentityUser Unique identifier
        /// </summary>
        public Guid IdentityUserId { get; set; }

        /// <summary>
        /// Admin's First Name
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Admin's Last Name
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Admin's Email
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Admin's Phone Number
        /// </summary>
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// Type of phone number (mobile/landline) 
        /// </summary>
        public int? PhoneNumberTypeId { get; private set; }

        /// <summary>
        /// Admin's Other Phone Number
        /// </summary>
        public string OtherPhoneNumber { get; private set; }

        /// <summary>
        /// Admin's Address
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Admin's Joining Date
        /// </summary>
        public DateTime? JoiningDate { get; private set; }

        public Admin(Guid identityUserId, string firstName, string lastName, string email, string phoneNumber, string address)
        {
            if (identityUserId == default) throw new ArgumentException(null, nameof(identityUserId));
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException(null, nameof(firstName));
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentException(null, nameof(lastName));
            if (string.IsNullOrEmpty(email)) throw new ArgumentException(null, nameof(email));
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentException(null, nameof(phoneNumber));
            if (string.IsNullOrEmpty(address)) throw new ArgumentException(null, nameof(address));

            IdentityUserId = identityUserId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public void UpdateRegistrationInformation(string phoneNumber, string address)
        {
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentException(null, nameof(phoneNumber));
            if (string.IsNullOrEmpty(address)) throw new ArgumentException(null, nameof(address));

            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}
