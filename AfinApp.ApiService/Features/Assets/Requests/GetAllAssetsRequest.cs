using Mediator;

namespace AfinApp.ApiService.Features.Assets.Requests;

public record GetAllAssetsRequest() : IRequest<IEnumerable<AssetResponse>>;
