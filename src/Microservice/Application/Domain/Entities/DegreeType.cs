using MonoRepo.Framework.Core.Domain;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.Application.Domain.Entities
{
    public class DegreeType : Entity
    {
        /// <summary>
        /// Degree unique identifier
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Degree name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Degree description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Degree Credits
        /// </summary>
        public int Credits { get; private set; }

        /// <summary>
        /// Flag representing if this degree is active
        /// </summary>
        public bool IsActive { get; private set; }

        public IEnumerable<Aggregates.DegreeAggregate.Degree> Degrees { get; private set; }

        public DegreeType(int id, string name, string description, int credits, bool isActive)
        {
            if (id < 0) throw new ArgumentException(null, nameof(id));
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(null, nameof(name));
            if (string.IsNullOrEmpty(description)) throw new ArgumentException(null, nameof(description));
            if (credits < 0) throw new ArgumentException(null, nameof(credits));

            Id = id;
            Name = name;
            Description = description;
            Credits = credits;
            IsActive = isActive;
        }

        public DegreeType(string name, string description, int credits, bool isActive)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(null, nameof(name));
            if (string.IsNullOrEmpty(description)) throw new ArgumentException(null, nameof(description));
            if (credits < 0) throw new ArgumentException(null, nameof(credits));

            Name = name;
            Description = description;
            Credits = credits;
            IsActive = isActive;
        }
    }
}
