using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AfinApp.ApiService.Features.AssetPrices.Requests;

namespace AfinApp.ApiService.Features.AssetPrices;

public class GetAssetPriceByIdHandler(IAssetPriceRelevantContext ctx) : IRequestHandler<GetAssetPriceByIdRequest, Result<AssetPriceResponse>>
{
    public async ValueTask<Result<AssetPriceResponse>> Handle(GetAssetPriceByIdRequest req, CancellationToken ct)
    {
        var item = await ctx.AssetPrices.Where(x => x.Id == req.Id)
            .Select(x => x.ConvertToResponse())
            .FirstOrDefaultAsync(ct);
        return item != null
            ? item
            : new Error(ErrorType.NotFound, $"{Const.apiAssetPrices}.NotFound", "Not found");
    }
}