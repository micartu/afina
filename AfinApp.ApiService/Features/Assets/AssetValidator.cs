using AfinApp.ApiService.Features.Assets.Requests;
using FluentValidation;

namespace AfinApp.ApiService.Features.Assets;

public class AssetValidator : AbstractValidator<IAssetRequest>
{
    public AssetValidator()
    {
        RuleFor(x => x.Ticker).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}