using MonoRepo.Framework.Core.Domain;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.Application.Domain.Entities
{
    public class Instructor : Entity
    {
        /// <summary>
        /// Instructor unique identifier
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// IdentityUser Unique identifier
        /// </summary>
        public Guid IdentityUserId { get; set; }

        /// <summary>
        /// Instructor's First Name
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Instructor's Last Name
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Instructor's Gender
        /// </summary>
        public string Gender { get; private set; }

        /// <summary>
        /// Instructor's Email
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Instructor's Phone Number
        /// </summary>
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// Type of phone number (mobile/landline) 
        /// </summary>
        public int? PhoneNumberTypeId { get; private set; }

        /// <summary>
        /// Instructor's Other Phone Number
        /// </summary>
        public string OtherPhoneNumber { get; private set; }

        /// <summary>
        /// Instructor's Address
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Instructor's Joining Date
        /// </summary>
        public DateTime? JoiningDate { get; private set; }

        public IEnumerable<InstructorCourse> InstructorCourses { get; private set; }

        public Instructor(Guid identityUserId, string firstName, string lastName, string email, string phoneNumber, int? phoneNumberTypeId, string address, DateTime? joiningDate)
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
            JoiningDate = joiningDate;
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
