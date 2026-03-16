using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApiSample.Domain.Entities;
using BankingApiSample.Domain.Enums;
using BankingApiSample.Domain.Interfaces;
using BankingApiSample.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankingApiSample.Infrastructure.Repositories
{
    public class ProposalRepository : IProposalRepository
    {
        private readonly BankingDbContext _db;

        public ProposalRepository(BankingDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Proposal proposal)
        {
            await _db.Proposals.AddAsync(proposal);
        }

        public async Task<Proposal?> GetByIdAsync(Guid id)
        {
            return await _db.Proposals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Proposal>> ListAsync(ProposalStatus? status = null)
        {
            var query = _db.Proposals.AsQueryable();
            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);
            return await query.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public Task UpdateAsync(Proposal proposal)
        {
            _db.Proposals.Update(proposal);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
