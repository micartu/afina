using Mediator;
using AfinApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using AfinApp.ApiService.Features.Assets.Requests;

namespace AfinApp.ApiService.Features.Assets;

public class GetAllAssetsHandler(IAssetRelevantContext ctx) : IRequestHandler<GetAllAssetsRequest, IEnumerable<AssetResponse>>
{
    public async ValueTask<IEnumerable<AssetResponse>> Handle(GetAllAssetsRequest req, CancellationToken ct)
        => await ctx.Assets.Select(x => x.ConvertToResponse()).ToListAsync(ct);
}