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
            builder.HasKey(x => new {x.UserId, x.ProjectId});
            
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(x => x.Project)
                .WithMany()
                .HasForeignKey(x => x.ProjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}