using AfinApp.ApiService.Features.Accounts.Requests;
using FluentValidation;

namespace AfinApp.ApiService.Features.Accounts;

public class AccountValidator : AbstractValidator<IAccountRequest>
{
    public AccountValidator() { RuleFor(x => x.Name).NotEmpty(); RuleFor(x => x.Currency).NotEmpty(); }
}
