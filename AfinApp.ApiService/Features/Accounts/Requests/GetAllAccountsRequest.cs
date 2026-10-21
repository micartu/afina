using Mediator;

namespace AfinApp.ApiService.Features.Accounts.Requests;

public record GetAllAccountsRequest() : IRequest<IEnumerable<AccountResponse>>;
