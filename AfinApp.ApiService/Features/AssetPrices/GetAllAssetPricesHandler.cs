using Mediator;
using AfinApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using AfinApp.ApiService.Features.AssetPrices.Requests;

namespace AfinApp.ApiService.Features.AssetPrices;

public class GetAllAssetPricesHandler(IAssetPriceRelevantContext ctx) : IRequestHandler<GetAllAssetPricesRequest, IEnumerable<AssetPriceResponse>>
{
    public async ValueTask<IEnumerable<AssetPriceResponse>> Handle(GetAllAssetPricesRequest req, CancellationToken ct)
        => await ctx.AssetPrices.Select(x => x.ConvertToResponse()).ToListAsync(ct);
}
