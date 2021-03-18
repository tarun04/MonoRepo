using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Query.GetInstructors
{
    public class GetInstructorsQueryHandler : IRequestHandler<GetInstructorsQuery, IReadOnlyList<GetInstructorsViewModel>>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public GetInstructorsQueryHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<IReadOnlyList<GetInstructorsViewModel>> Handle(GetInstructorsQuery request, CancellationToken cancellationToken)
        {
            return await context.Instructor
                                .AsNoTracking()
                                .Select(x => new GetInstructorsViewModel
                                {
                                    Id = x.Id,
                                    FirstName = x.FirstName,
                                    LastName = x.LastName,
                                    Gender = x.Gender,
                                    Email = x.Email,
                                    PhoneNumber = x.PhoneNumber,
                                    PhoneNumberTypeId = x.PhoneNumberTypeId,
                                    OtherPhoneNumber = x.OtherPhoneNumber,
                                    Address = x.Address,
                                    JoiningDate = x.JoiningDate
                                })
                                .ToListAsync(cancellationToken);
        }
    }
}
