using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Holdings.Requests;

public record DeleteHoldingRequest(Guid Id) : IRequest<Result>;
