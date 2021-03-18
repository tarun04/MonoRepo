using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Query.GetParentById
{
    public class GetParentByIdQueryHandler : IRequestHandler<GetParentByIdQuery, GetParentByIdViewModel>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public GetParentByIdQueryHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<GetParentByIdViewModel> Handle(GetParentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await context.Parent
                                       .AsNoTracking()
                                       .Where(x => x.Id == request.Id)
                                       .Include(y => y.Children)
                                       .Select(x => new GetParentByIdViewModel
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
                                           Children = x.Children.Select(y => new StudentViewModel
                                           {
                                               Id = y.Id,
                                               FirstName = y.FirstName,
                                               LastName = y.LastName
                                           }).ToList()
                                       })
                                       .FirstOrDefaultAsync(cancellationToken);

            return student;
        }
    }
}
