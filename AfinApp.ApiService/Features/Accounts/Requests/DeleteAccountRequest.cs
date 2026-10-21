using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Accounts.Requests;

public record DeleteAccountRequest(Guid Id) : IRequest<Result>;
