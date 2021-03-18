using MonoRepo.Framework.Core.Domain;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.Application.Domain.Entities
{
    public class Student : Entity
    {
        /// <summary>
        /// Student unique identifier
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// IdentityUser Unique identifier
        /// </summary>
        public Guid IdentityUserId { get; set; }

        /// <summary>
        /// Student's First Name
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Student's Last Name
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Student's Gender
        /// </summary>
        public string Gender { get; private set; }

        /// <summary>
        /// Student's Email
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Student's Phone Number
        /// </summary>
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// Type of phone number (mobile/landline) 
        /// </summary>
        public int? PhoneNumberTypeId { get; private set; }

        /// <summary>
        /// Student's Other Phone Number
        /// </summary>
        public string OtherPhoneNumber { get; private set; }

        /// <summary>
        /// Student's Address
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Student's Birth Date
        /// </summary>
        public DateTime? BirthDate { get; private set; }

        /// <summary>
        /// Student's admission date
        /// </summary>
        public DateTime? AdmissionDate { get; private set; }

        /// <summary>
        /// Other Name
        /// </summary>
        public string OtherName { get; private set; }

        /// <summary>
        /// MiddleName
        /// </summary>
        public string MiddleName { get; private set; }

        /// <summary>
        /// E.g. Mr.
        /// </summary>
        public string NameSuffix { get; private set; }

        /// <summary>
        /// Id of the Parent
        /// </summary>
        public int ParentId { get; private set; }

        /// <summary>
        /// Parent Navigation Property
        /// </summary>
        public Parent Parent { get; private set; }

        public IEnumerable<StudentCourse> StudentCourses { get; private set; }

        public ICollection<Aggregates.DegreeAggregate.Degree> Degrees { get; private set; }

        public Student(Guid identityUserId, string firstName, string lastName, string email, string phoneNumber, int? phoneNumberTypeId, string address, DateTime? birthDate, DateTime? admissionDate)
        {
            if (identityUserId == default) throw new ArgumentException(null, nameof(identityUserId));
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException(null, nameof(firstName));
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentException(null, nameof(lastName));
            if (string.IsNullOrEmpty(email)) throw new ArgumentException(null, nameof(email));
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentException(null, nameof(phoneNumber));
            if (string.IsNullOrEmpty(address)) throw new ArgumentException(null, nameof(address));
            if (birthDate == default) throw new ArgumentException(null, nameof(birthDate));
            if (admissionDate == default) throw new ArgumentException(null, nameof(admissionDate));

            IdentityUserId = identityUserId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            PhoneNumberTypeId = phoneNumberTypeId;
            Address = address;
            BirthDate = birthDate;
            AdmissionDate = admissionDate;
        }

        public void UpdateRegistrationInformation(string phoneNumber, int? phoneNumberTypeId, string otherPhoneNumber, string address, string otherName, string middleName, string nameSuffix)
        {
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentException(null, nameof(phoneNumber));
            if (string.IsNullOrEmpty(address)) throw new ArgumentException(null, nameof(address));

            PhoneNumber = phoneNumber;
            PhoneNumberTypeId = phoneNumberTypeId;
            OtherPhoneNumber = otherPhoneNumber;
            Address = address;
            OtherName = otherName;
            MiddleName = middleName;
            NameSuffix = nameSuffix;
        }
    }
}
