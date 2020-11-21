using MonoRepo.Framework.Core.Domain;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.Tenant.Domain.Entities
{
    public class Tenant : Entity
    {
        // Guid or String?
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }

        public ICollection<TenantProduct> TenantProducts { get; set; }

        public Tenant(string id, string name, string displayName)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(displayName)) throw new ArgumentNullException(nameof(displayName));

            Id = id;
            Name = name;
            DisplayName = displayName;
        }

        public Tenant(string name, string displayName) : this(Guid.NewGuid().ToString(), name, displayName) { }
    }
}
