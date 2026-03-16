using AutoMapper;
using BankingApiSample.Application.DTOs;
using BankingApiSample.Domain.Entities;

namespace BankingApiSample.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Proposal, ProposalDto>().ReverseMap();
            CreateMap<CreateProposalDto, Proposal>();
        }
    }
}
