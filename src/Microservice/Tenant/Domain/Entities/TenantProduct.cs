using System;

namespace MonoRepo.Microservice.Tenant.Domain.Entities
{
    public class TenantProduct
    {
        public string TenantId { get; set; }
        public string ProductId { get; set; }
        public DateTime PurchasedDate { get; set; }
        public DateTime GoLiveDate { get; set; }
        public bool IsActive { get; set; }
        public string TenantProductUrl { get; set; }
        public Tenant Tenant { get; set; }
        public Product Product { get; set; }
    }
}
