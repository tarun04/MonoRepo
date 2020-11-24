using MediatR;
using MonoRepo.Microservice.Tenant.Domain.ViewModel;
using System;

namespace MonoRepo.Microservice.Tenant.Query.GetTenantById
{
    public class GetTenantByIdQuery : IRequest<TenantViewModel>
    {
        public Guid Id { get; set; }
    }
}
