using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Assets.Requests;

public interface IAssetRequest { string Ticker { get; } string Name { get; } AssetType AssetType { get; } }
