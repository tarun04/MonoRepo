using MonoRepo.Framework.Core.Domain;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.Tenant.Domain.Entities
{
    public class Product : Entity
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public ICollection<TenantProduct> TenantProducts { get; private set; }

        public Product(string id, string name)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException(nameof(id));
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(nameof(name));

            Id = id;
            Name = name;
        }

        public Product(string name) : this(Guid.NewGuid().ToString(), name) { }
    }
}
