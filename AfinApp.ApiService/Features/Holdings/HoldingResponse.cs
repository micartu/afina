namespace AfinApp.ApiService.Features.Holdings;

public record HoldingResponse(Guid Id, Guid AccountId, Guid AssetId, decimal Quantity, decimal AverageCostBasis);
