using Mediator;

namespace AfinApp.ApiService.Features.AssetPrices.Requests;

public record GetAllAssetPricesRequest() : IRequest<IEnumerable<AssetPriceResponse>>;
