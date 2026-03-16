using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BankingApiSample.Application.DTOs;
using BankingApiSample.Application.Interfaces;
using BankingApiSample.Domain.Entities;
using BankingApiSample.Domain.Enums;
using BankingApiSample.Domain.Interfaces;

namespace BankingApiSample.Application.Services
{
    public class ProposalService : IProposalService
    {
        private readonly IProposalRepository _repository;
        private readonly IMapper _mapper;

        public ProposalService(IProposalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProposalDto> CreateAsync(CreateProposalDto dto)
        {
            var proposal = new Proposal(dto.CustomerName, dto.DocumentNumber, dto.RequestedAmount, dto.MonthlyIncome);
            await _repository.AddAsync(proposal);
            await _repository.SaveChangesAsync();
            return _mapper.Map<ProposalDto>(proposal);
        }

        public async Task<ProposalDto?> GetByIdAsync(Guid id)
        {
            var p = await _repository.GetByIdAsync(id);
            return p == null ? null : _mapper.Map<ProposalDto>(p);
        }

        public async Task<IEnumerable<ProposalDto>> ListAsync(ProposalStatus? status = null)
        {
            var list = await _repository.ListAsync(status);
            return _mapper.Map<IEnumerable<ProposalDto>>(list);
        }

        public async Task ReviewAsync(Guid id)
        {
            var p = await _repository.GetByIdAsync(id) ?? throw new Exception("Proposal not found");
            p.MoveToUnderReview();
            await _repository.UpdateAsync(p);
            await _repository.SaveChangesAsync();
        }

        public async Task ApproveAsync(Guid id)
        {
            var p = await _repository.GetByIdAsync(id) ?? throw new Exception("Proposal not found");
            p.Approve();
            await _repository.UpdateAsync(p);
            await _repository.SaveChangesAsync();
        }

        public async Task RejectAsync(Guid id)
        {
            var p = await _repository.GetByIdAsync(id) ?? throw new Exception("Proposal not found");
            p.Reject();
            await _repository.UpdateAsync(p);
            await _repository.SaveChangesAsync();
        }
    }
}
