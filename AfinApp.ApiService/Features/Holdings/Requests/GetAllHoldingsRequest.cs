using Mediator;

namespace AfinApp.ApiService.Features.Holdings.Requests;

public record GetAllHoldingsRequest() : IRequest<IEnumerable<HoldingResponse>>;
