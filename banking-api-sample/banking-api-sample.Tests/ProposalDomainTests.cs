using System;
using BankingApiSample.Domain.Entities;
using BankingApiSample.Domain.Exceptions;
using Xunit;

namespace BankingApiSample.Tests
{
    public class ProposalDomainTests
    {
        [Fact]
        public void CreatingProposal_WithRequestedAmountGreaterThan5xIncome_ShouldThrow()
        {
            var ex = Assert.Throws<DomainException>(() => new Proposal("Test", "000", 6000m, 1000m));
            Assert.Contains("cannot exceed 5x", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void CreatingProposal_WithValidAmounts_ShouldCreate()
        {
            var p = new Proposal("Test", "000", 1000m, 1000m);
            Assert.Equal(1000m, p.RequestedAmount);
            Assert.Equal(Enums.ProposalStatus.Pending, p.Status);
        }
    }
}
