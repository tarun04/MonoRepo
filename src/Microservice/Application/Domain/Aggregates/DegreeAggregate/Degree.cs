using MonoRepo.Framework.Core.Domain;
using MonoRepo.Framework.Core.Interfaces;
using MonoRepo.Microservice.Application.Domain.Entities;
using System;

namespace MonoRepo.Microservice.Application.Domain.Aggregates.DegreeAggregate
{
    public class Degree : MultitenantEntity, IAggregateRoot
    {
        private const string DegreeHasBeenCompletedMessage = "Degree has already been completed.";

        /// <summary>
        /// Degree Unique identifier
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Unique Identifier from IdentityUser
        /// </summary>
        public Guid IdentityUserId { get; set; }

        /// <summary>
        /// Identity User Id
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Student on this Degree
        /// </summary>
        public Student Student { get; private set; }

        /// <summary>
        /// Id of the DegreeType
        /// </summary>
        public int DegreeTypeId { get; private set; }

        /// <summary>
        /// DegreeType Navigation Property
        /// </summary>
        public DegreeType DegreeType { get; private set; }

        /// <summary>
        /// Degree's start date
        /// </summary>
        public DateTime? StartDate { get; private set; }

        /// <summary>
        /// Indicator on degree progress
        /// </summary>
        public int CreditsCompleted { get; private set; }

        /// <summary>
        /// Degree's graduation date
        /// </summary>
        public DateTime? GraduationDate { get; private set; }

        /// <summary>
        /// Flag representing if this degree is completed
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// GPA for this degree
        /// </summary>
        public double AquiredGPA { get; private set; }

        public Degree(Guid id, Guid identityUserId, Guid tenantId, int degreeTypeId)
        {
            if (id == Guid.Empty) throw new ArgumentException(null, nameof(id));
            if (identityUserId == Guid.Empty) throw new ArgumentException(null, nameof(identityUserId));
            if (tenantId == Guid.Empty) throw new ArgumentException(null, nameof(tenantId));
            if (degreeTypeId < 0) throw new ArgumentException(null, nameof(degreeTypeId));

            Id = id;
            IdentityUserId = identityUserId;
            TenantId = tenantId;
            DegreeTypeId = degreeTypeId;

            CreditsCompleted = 0;
            AquiredGPA = 0.0;
        }

        public Degree(Guid identityUserId, Guid tenantId, int degreeTypeId) : this(Guid.NewGuid(), identityUserId, tenantId, degreeTypeId) { }

        public void StartDegree(DateTime startDate)
        {
            // Check if completed.
            if (IsCompleted) throw new InvalidOperationException(DegreeHasBeenCompletedMessage);

            StartDate = startDate;
        }

        public void GraduateDegree(DateTime graduationDate)
        {
            // Check if completed.
            if (IsCompleted) throw new InvalidOperationException(DegreeHasBeenCompletedMessage);

            GraduationDate = graduationDate;
        }
    }
}
