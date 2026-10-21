using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Assets.Requests;

public record GetAssetByIdRequest(Guid Id) : IRequest<Result<AssetResponse>>;
