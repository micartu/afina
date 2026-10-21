using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Transactions.Requests;

public interface ITransactionRequest { Guid AccountId { get; } Guid? CategoryId { get; } decimal Amount { get; } DateTime Date { get; } string Description { get; } TransactionType Type { get; } Guid? TargetAccountId { get; } }
