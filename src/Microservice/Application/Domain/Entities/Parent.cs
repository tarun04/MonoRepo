using MonoRepo.Framework.Core.Domain;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.Application.Domain.Entities
{
    public class Parent : Entity
    {
        /// <summary>
        /// Parent unique identifier
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// IdentityUser Unique identifier
        /// </summary>
        public Guid IdentityUserId { get; set; }

        /// <summary>
        /// Parent's First Name
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Parent's Last Name
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Parent's Gender
        /// </summary>
        public string Gender { get; private set; }

        /// <summary>
        /// Parent's Email
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Parent's Phone Number
        /// </summary>
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// Type of phone number (mobile/landline) 
        /// </summary>
        public int? PhoneNumberTypeId { get; private set; }

        /// <summary>
        /// Parent's Other Phone Number
        /// </summary>
        public string OtherPhoneNumber { get; private set; }

        /// <summary>
        /// Parent's Address
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Type of relation (father/mother/guardian) 
        /// </summary>
        public int? RelationTypeId { get; private set; }

        public ICollection<Student> Children { get; private set; }

        public Parent(Guid identityUserId, string firstName, string lastName, string email, string phoneNumber, int? phoneNumberTypeId, string address, int? relationTypeId)
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
            PhoneNumberTypeId = phoneNumberTypeId;
            Address = address;
            RelationTypeId = relationTypeId;
        }

        public void UpdateRegistrationInformation(string phoneNumber, int? phoneNumberTypeId, string otherPhoneNumber, string address)
        {
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentException(null, nameof(phoneNumber));
            if (string.IsNullOrEmpty(address)) throw new ArgumentException(null, nameof(address));

            PhoneNumber = phoneNumber;
            PhoneNumberTypeId = phoneNumberTypeId;
            OtherPhoneNumber = otherPhoneNumber;
            Address = address;
        }
    }
}
