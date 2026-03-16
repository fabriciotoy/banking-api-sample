using BankingApiSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingApiSample.Infrastructure.Persistence
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) { }

        public DbSet<Proposal> Proposals { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.ProposalConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
