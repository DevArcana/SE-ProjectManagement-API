using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.API.Domain.Projects.Entities;

namespace ProjectManagement.API.Infrastructure.Persistence.Configurations
{
    public class UserProjectAccessConfiguration : IEntityTypeConfiguration<UserProjectAccess>
    {
        public void Configure(EntityTypeBuilder<UserProjectAccess> builder)
        {
            builder.HasKey(x => new {x.User, x.Project});
            builder.HasOne(x => x.User).WithMany().IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Project).WithMany().IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}