using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankingApiSample.Application.DTOs;
using BankingApiSample.Domain.Enums;

namespace BankingApiSample.Application.Interfaces
{
    public interface IProposalService
    {
        Task<ProposalDto> CreateAsync(CreateProposalDto dto);
        Task<ProposalDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProposalDto>> ListAsync(ProposalStatus? status = null);
        Task ReviewAsync(Guid id);
        Task ApproveAsync(Guid id);
        Task RejectAsync(Guid id);
    }
}
