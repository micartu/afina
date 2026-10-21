namespace AfinApp.ApiService.Features.AssetPrices.Requests;

public interface IAssetPriceRequest { Guid AssetId { get; } DateTime Date { get; } decimal ClosePrice { get; } }