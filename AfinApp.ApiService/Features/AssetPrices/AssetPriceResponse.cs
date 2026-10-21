namespace AfinApp.ApiService.Features.AssetPrices;

public record AssetPriceResponse(Guid Id, Guid AssetId, DateTime Date, decimal ClosePrice);
