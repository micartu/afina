using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Accounts.Requests;

public record UpdateAccountRequest(Guid Id, Guid FamilyId, string Name, AccountType Type, string Currency, decimal InitialBalance) : IAccountRequest, IRequest<Result<AccountResponse>>;
