using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Accounts;

public record AccountResponse(Guid Id, Guid FamilyId, string Name, AccountType Type, string Currency, decimal InitialBalance);
