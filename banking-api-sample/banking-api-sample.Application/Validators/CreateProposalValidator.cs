using FluentValidation;
using BankingApiSample.Application.DTOs;

namespace BankingApiSample.Application.Validators
{
    public class CreateProposalValidator : AbstractValidator<CreateProposalDto>
    {
        public CreateProposalValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.DocumentNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.RequestedAmount).GreaterThan(0);
            RuleFor(x => x.MonthlyIncome).GreaterThan(0);
            RuleFor(x => x).Custom((dto, context) =>
            {
                if (dto.RequestedAmount > dto.MonthlyIncome * 5)
                {
                    context.AddFailure("RequestedAmount", "RequestedAmount cannot exceed 5x monthly income.");
                }
            });
        }
    }
}
