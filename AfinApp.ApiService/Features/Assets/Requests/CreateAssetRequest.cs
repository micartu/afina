using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Assets.Requests;

public record CreateAssetRequest(string Ticker, string Name, AssetType AssetType) : IAssetRequest, IRequest<Result<AssetResponse>>;
