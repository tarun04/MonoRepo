using MonoRepo.Framework.Core.Security;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.Tenant.Domain.ViewModel
{
    public class TenantViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<UserProduct> TenantProducts { get; set; }
    }
}
