using System;

namespace MonoRepo.Microservice.IdentityServer.B2C.Domain.Entities
{
    public class Config
    {
        public Guid ConfigId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public Guid TenantId { get; set; }
        public string Description { get; set; }
        public int? Order { get; set; }
    }
}
