using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Accounts.Requests;

public interface IAccountRequest { Guid FamilyId { get; } string Name { get; } AccountType Type { get; } string Currency { get; } decimal InitialBalance { get; } }