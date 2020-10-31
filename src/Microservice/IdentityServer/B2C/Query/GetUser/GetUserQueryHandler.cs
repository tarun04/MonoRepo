using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Microservice.IdentityServer.B2C.Infrastructure;
using MonoRepo.Microservice.IdentityServer.B2C.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2C.Query.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
    {
        private readonly IdentityB2CDbContext context;
        
        public GetUserQueryHandler(IdentityB2CDbContext context)
        {
            this.context = context;
        }

        public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await context.Users
                                .AsNoTracking()
                                .Where(x => x.TenantId == request.TenantId && x.Email == request.Email)
                                .Select(x => new UserViewModel
                                {
                                    Id = x.Id,
                                    TenantId = x.TenantId,
                                    FirstName = x.FirstName,
                                    LastName = x.LastName,
                                    Email = x.Email,
                                    PhoneNumber = x.PhoneNumber
                                })
                                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
