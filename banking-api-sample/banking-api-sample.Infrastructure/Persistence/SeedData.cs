using System;
using System.Linq;
using System.Threading.Tasks;
using BankingApiSample.Domain.Entities;
using BankingApiSample.Domain.Enums;

namespace BankingApiSample.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static async Task EnsureSeedAsync(BankingDbContext db)
        {
            if (db.Proposals.Any()) return;

            var p1 = new Proposal("Alice Silva", "12345678901", 5000m, 2000m);
            var p2 = new Proposal("Bruno Costa", "98765432100", 800m, 1000m);
            p2.Approve();

            await db.Proposals.AddRangeAsync(p1, p2);
            await db.SaveChangesAsync();
        }
    }
}
