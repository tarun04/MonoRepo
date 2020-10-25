using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Framework.Core.Domain;

namespace MonoRepo.Framework.Infrastructure.EntityConfigurations
{
    public abstract class MultiTenantEntityConfiguration<T> : EntityConfiguration<T> where T : MultitenantEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.TenantId).IsRequired();
        }
    }
}
