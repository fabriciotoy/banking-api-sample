using BankingApiSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingApiSample.Infrastructure.Configurations
{
    public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.ToTable("Proposals");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CustomerName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DocumentNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.RequestedAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.MonthlyIncome).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();
        }
    }
}
