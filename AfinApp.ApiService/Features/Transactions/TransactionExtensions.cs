using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Transactions;

public static class TransactionExtensions
{
    public static TransactionResponse ConvertToResponse(this Transaction i) => new(
            i.Id,
            i.AccountId,
            i.CategoryId,
            i.Amount,
            i.Date,
            i.Description,
            i.Type,
            i.TargetAccountId
            );
}
