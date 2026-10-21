using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AfinApp.ApiService.Features.Assets.Requests;

namespace AfinApp.ApiService.Features.Assets;

public class GetAssetByIdHandler(IAssetRelevantContext ctx) : IRequestHandler<GetAssetByIdRequest, Result<AssetResponse>>
{
    public async ValueTask<Result<AssetResponse>> Handle(GetAssetByIdRequest req, CancellationToken ct)
    {
        var item = await ctx.Assets.Where(x => x.Id == req.Id).Select(x => x.ConvertToResponse()).FirstOrDefaultAsync(ct);
        return item != null
            ? item
            : new Error(ErrorType.NotFound, $"{Const.apiAssets}.NotFound", "Not found");
    }
}