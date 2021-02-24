using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Microservice.Application.Domain.Views;

namespace MonoRepo.Microservice.Application.Infrastructure.EntityConfigurations
{
    public class DegreeSummaryConfiguration : IEntityTypeConfiguration<DegreeSummary>
    {
        public void Configure(EntityTypeBuilder<DegreeSummary> builder)
        {
            builder.HasNoKey();
            builder.ToView("View_DegreeSummary");
        }
    }
}
