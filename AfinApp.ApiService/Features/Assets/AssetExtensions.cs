using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Assets;

public static class AssetExtensions
{
    public static AssetResponse ConvertToResponse(this Asset i) => new(i.Id, i.Ticker, i.Name, i.AssetType);
}
