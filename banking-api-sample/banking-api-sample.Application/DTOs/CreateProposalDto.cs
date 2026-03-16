using System;

namespace BankingApiSample.Application.DTOs
{
    public class CreateProposalDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public decimal RequestedAmount { get; set; }
        public decimal MonthlyIncome { get; set; }
    }
}
