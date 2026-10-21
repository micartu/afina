using AfinApp.ApiService.Features.Holdings.Requests;
using FluentValidation;

namespace AfinApp.ApiService.Features.Holdings;

public class HoldingValidator : AbstractValidator<IHoldingRequest>
{
    public HoldingValidator() { RuleFor(x => x.Quantity).GreaterThan(0); }
}
