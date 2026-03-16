using System;

namespace BankingApiSample.Domain.Entities
{
    public class Proposal
    {
        public Guid Id { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;
        public string DocumentNumber { get; private set; } = string.Empty;
        public decimal RequestedAmount { get; private set; }
        public decimal MonthlyIncome { get; private set; }
        public Enums.ProposalStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Proposal() { }

        public Proposal(string customerName, string documentNumber, decimal requestedAmount, decimal monthlyIncome)
        {
            Id = Guid.NewGuid();
            CustomerName = customerName;
            DocumentNumber = documentNumber;
            RequestedAmount = requestedAmount;
            MonthlyIncome = monthlyIncome;
            Status = Enums.ProposalStatus.Pending;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            ValidateBusinessRules();
        }

        private void ValidateBusinessRules()
        {
            if (RequestedAmount <= 0) throw new Exceptions.DomainException("RequestedAmount must be greater than zero.");
            if (MonthlyIncome <= 0) throw new Exceptions.DomainException("MonthlyIncome must be greater than zero.");
            if (RequestedAmount > MonthlyIncome * 5) throw new Exceptions.DomainException("RequestedAmount cannot exceed 5x monthly income.");
        }

        public void MoveToUnderReview()
        {
            if (Status != Enums.ProposalStatus.Pending)
                throw new Exceptions.DomainException("Only pending proposals can be moved to review.");

            Status = Enums.ProposalStatus.UnderReview;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Approve()
        {
            if (Status == Enums.ProposalStatus.Approved)
                throw new Exceptions.DomainException("Proposal is already approved.");

            Status = Enums.ProposalStatus.Approved;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Reject()
        {
            if (Status == Enums.ProposalStatus.Rejected)
                throw new Exceptions.DomainException("Proposal is already rejected.");

            Status = Enums.ProposalStatus.Rejected;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateRequestedAmount(decimal amount)
        {
            RequestedAmount = amount;
            ValidateBusinessRules();
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
