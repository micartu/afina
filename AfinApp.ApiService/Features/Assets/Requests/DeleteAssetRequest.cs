using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Assets.Requests;

public record DeleteAssetRequest(Guid Id) : IRequest<Result>;
