using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.AssetPrices.Requests;

public record DeleteAssetPriceRequest(Guid Id) : IRequest<Result>;
