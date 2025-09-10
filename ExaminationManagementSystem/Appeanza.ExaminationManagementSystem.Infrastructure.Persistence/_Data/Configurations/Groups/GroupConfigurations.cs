using Appeanza.ExaminationManagementSystem.Domain.Entities.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data.Configurations.Groups
{
    public class GroupConfigurations : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(G => G.Id);

            builder.Property(G => G.Id)
                .UseIdentityColumn();
            builder.Property(G => G.Name)
                .HasColumnType("NVARCHAR")
                .IsRequired();

            builder.Property(G => G.CreationDate)
                .HasColumnType("DATETIME")
                .HasComputedColumnSql("SYSUTCDATETIME()")
                .IsRequired();
        }
    }
}
