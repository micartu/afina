namespace AfinApp.ApiService.Features.Holdings.Requests;

public interface IHoldingRequest { Guid AccountId { get; } Guid AssetId { get; } decimal Quantity { get; } decimal AverageCostBasis { get; } }
