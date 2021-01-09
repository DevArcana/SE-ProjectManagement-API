using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.API.Domain.Issues.Entities;
using ProjectManagement.API.Domain.Projects.Entities;

namespace ProjectManagement.API.Infrastructure.Persistence.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.HasOne(x => x.Manager).WithMany().IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany<Issue>().WithOne(x => x.Project).IsRequired();
        }
    }
}