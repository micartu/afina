namespace AfinApp.Data.Entities;

// 5. Stocks / Assets
public class Asset 
{
    public Guid Id { get; set; }
    public string Ticker { get; set; } = string.Empty; // e.g., "AAPL", "VOO"
    public string Name { get; set; } = string.Empty;   // e.g., "Apple Inc."
    public AssetType AssetType { get; set; }
    
    public ICollection<Holding> Holdings { get; set; } = new List<Holding>();
    public ICollection<AssetPrice> PriceHistory { get; set; } = new List<AssetPrice>();
}