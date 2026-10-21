using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Assets;

public record AssetResponse(Guid Id, string Ticker, string Name, AssetType AssetType);
