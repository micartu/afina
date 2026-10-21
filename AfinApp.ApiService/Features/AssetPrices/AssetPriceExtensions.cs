using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.AssetPrices;

public static class AssetPriceExtensions
{
    public static AssetPriceResponse ConvertToResponse(this AssetPrice i) => new(
            i.Id,
            i.AssetId,
            i.Date,
            i.ClosePrice
            );
}