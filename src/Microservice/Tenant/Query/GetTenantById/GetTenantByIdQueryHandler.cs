using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Tenant.Domain.ViewModel;
using MonoRepo.Microservice.Tenant.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Tenant.Query.GetTenantById
{
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, TenantViewModel>
    {
        private readonly TenantDbContext context;
        private readonly IMemoryCache memoryCache;

        public GetTenantByIdQueryHandler(TenantDbContext context, IMemoryCache memoryCache)
        {
            this.context = context;
            this.memoryCache = memoryCache;
        }

        public async Task<TenantViewModel> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            if (!memoryCache.TryGetValue(request.Id, out TenantViewModel tenant))
            {
                tenant = await context.Tenants
                                      .AsNoTracking()
                                      .Select(x => new TenantViewModel
                                      {
                                          Id = x.Id,
                                          Name = x.Name,
                                          DisplayName = x.DisplayName,
                                          TenantProducts = x.TenantProducts.Where(z => z.IsActive).Select(y => new UserProduct(y.ProductId, y.Product.Name, y.IsActive, y.TenantProductUrl))
                                      })
                                      .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (tenant == null)
                    throw new KeyNotFoundException($"Could not find {nameof(tenant)} with {nameof(request.Id)}: {request.Id}");

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(10));

                memoryCache.Set(request.Id, tenant, cacheEntryOptions);
            }
            return tenant;
        }
    }
}
