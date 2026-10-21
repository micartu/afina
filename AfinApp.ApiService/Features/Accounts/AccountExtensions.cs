using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Accounts;

public static class AccountExtensions
{
    public static AccountResponse ConvertToResponse(this Account i) => new(i.Id, i.FamilyId, i.Name, i.Type, i.Currency, i.InitialBalance);
}
