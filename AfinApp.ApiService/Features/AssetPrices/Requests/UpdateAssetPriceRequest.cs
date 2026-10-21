using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.AssetPrices.Requests;

public record UpdateAssetPriceRequest(Guid Id, Guid AssetId, DateTime Date, decimal ClosePrice) : IAssetPriceRequest, IRequest<Result<AssetPriceResponse>>;
