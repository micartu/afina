using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Holdings.Requests;

public record GetHoldingByIdRequest(Guid Id) : IRequest<Result<HoldingResponse>>;
