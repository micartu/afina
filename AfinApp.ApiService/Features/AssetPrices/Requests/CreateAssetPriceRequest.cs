using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.AssetPrices.Requests;

public record CreateAssetPriceRequest(Guid AssetId, DateTime Date, decimal ClosePrice) : IAssetPriceRequest, IRequest<Result<AssetPriceResponse>>;
