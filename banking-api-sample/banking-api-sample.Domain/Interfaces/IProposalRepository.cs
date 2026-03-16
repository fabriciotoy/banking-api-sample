using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankingApiSample.Domain.Entities;
using BankingApiSample.Domain.Enums;

namespace BankingApiSample.Domain.Interfaces
{
    public interface IProposalRepository
    {
        Task<Proposal?> GetByIdAsync(Guid id);
        Task<IEnumerable<Proposal>> ListAsync(ProposalStatus? status = null);
        Task AddAsync(Proposal proposal);
        Task UpdateAsync(Proposal proposal);
        Task<int> SaveChangesAsync();
    }
}
