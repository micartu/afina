using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Holdings.Requests;

public record CreateHoldingRequest(Guid AccountId, Guid AssetId, decimal Quantity, decimal AverageCostBasis) : IHoldingRequest, IRequest<Result<HoldingResponse>>;
