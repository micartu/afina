using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Transactions;

public record TransactionResponse(
        Guid Id,
        Guid AccountId,
        Guid? CategoryId,
        decimal Amount,
        DateTime Date,
        string Description,
        TransactionType Type,
        Guid? TargetAccountId
        );
