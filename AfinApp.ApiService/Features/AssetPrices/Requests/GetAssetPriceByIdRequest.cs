using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.AssetPrices.Requests;

public record GetAssetPriceByIdRequest(Guid Id) : IRequest<Result<AssetPriceResponse>>;
