using AfinApp.ApiService.Features.Transactions.Requests;
using FluentValidation;

namespace AfinApp.ApiService.Features.Transactions;

public class TransactionValidator : AbstractValidator<ITransactionRequest>
{
    public TransactionValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Amount).NotEqual(0);
    }
}
