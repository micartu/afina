using AfinApp.ApiService.Features.AssetPrices.Requests;
using FluentValidation;

namespace AfinApp.ApiService.Features.AssetPrices;

public class AssetPriceValidator : AbstractValidator<IAssetPriceRequest>
{
    public AssetPriceValidator() { RuleFor(x => x.ClosePrice).GreaterThan(0); }
}
