using System;
using BankingApiSample.Domain.Enums;

namespace BankingApiSample.Application.DTOs
{
    public class ProposalDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public decimal RequestedAmount { get; set; }
        public decimal MonthlyIncome { get; set; }
        public ProposalStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
