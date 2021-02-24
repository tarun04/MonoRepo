using System;

namespace MonoRepo.Microservice.Application.Domain.Views
{
    public class DegreeSummary
    {
        /// <summary>
        /// Degree Unique identifier
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Tenant Id who owns the degree
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// Student on this Degree
        /// </summary>
        public string Student { get; private set; }

        /// <summary>
        /// DegreeType Navigation Property
        /// </summary>
        public string DegreeType { get; private set; }

        /// <summary>
        /// Degree's start date
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Indicator on degree progress
        /// </summary>
        public int? CreditsCompleted { get; private set; }

        /// <summary>
        /// Degree's graduation date
        /// </summary>
        public DateTime GraduationDate { get; private set; }

        /// <summary>
        /// Flag representing if this degree is completed
        /// </summary>
        public bool? IsCompleted { get; private set; }

        /// <summary>
        /// GPA for this degree
        /// </summary>
        public double? AquiredGPA { get; private set; }
    }
}
