using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.Data.Entities;
using AfinApp.ApiService.Features.Assets.Requests;

namespace AfinApp.ApiService.Features.Assets;

public class CreateAssetHandler(IAssetRelevantContext ctx, IValidator<IAssetRequest> val) : IRequestHandler<CreateAssetRequest, Result<AssetResponse>>
{
    public async ValueTask<Result<AssetResponse>> Handle(CreateAssetRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiAssets, "Validation failed");
        var item = new Asset
        {
            Id = Guid.NewGuid(),
            Ticker = req.Ticker,
            Name = req.Name,
            AssetType = req.AssetType
        };
        ctx.Assets.Add(item);
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}