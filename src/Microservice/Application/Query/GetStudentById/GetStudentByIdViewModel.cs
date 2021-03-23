using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.Application.Query.GetStudentById
{
    public class GetStudentByIdViewModel
    {
        /// <summary>
        /// Student unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Student's First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Student's Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Student's Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Student's Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Student's Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Type of phone number (mobile/landline) 
        /// </summary>
        public int? PhoneNumberTypeId { get; set; }

        /// <summary>
        /// Student's Other Phone Number
        /// </summary>
        public string OtherPhoneNumber { get; set; }

        /// <summary>
        /// Student's Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Student's Birth Date
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Student's admission date
        /// </summary>
        public DateTime? AdmissionDate { get; set; }

        /// <summary>
        /// Other Name
        /// </summary>
        public string OtherName { get; set; }

        /// <summary>
        /// MiddleName
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// E.g. Mr.
        /// </summary>
        public string NameSuffix { get; set; }

        /// <summary>
        /// Id of the Parent
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Parent Navigation Property
        /// </summary>
        public ParentViewModel Parent { get; set; }

        public ICollection<StudentCourseViewModel> StudentCourses { get; set; }
    }

    public class ParentViewModel 
    {
        /// <summary>
        /// Parent unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Parent's First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Parent's Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Type of relation (father/mother/guardian) 
        /// </summary>
        public int? RelationTypeId { get; set; }
    }

    public class StudentCourseViewModel
    {
        /// <summary>
        /// Course unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Course name
        /// </summary>
        public string Name { get; set; }
    }
}
