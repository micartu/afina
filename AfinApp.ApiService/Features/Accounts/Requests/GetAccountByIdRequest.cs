using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Accounts.Requests;

public record GetAccountByIdRequest(Guid Id) : IRequest<Result<AccountResponse>>;
