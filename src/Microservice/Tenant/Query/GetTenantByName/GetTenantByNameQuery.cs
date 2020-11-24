using MediatR;
using MonoRepo.Microservice.Tenant.Domain.ViewModel;

namespace MonoRepo.Microservice.Tenant.Query.GetTenantByName
{
    public class GetTenantByNameQuery : IRequest<TenantViewModel>
    {
        public string Name { get; set; }
    }
}
