namespace AfinApp.Data.Entities;

// 7. Asset Price History (To calculate net worth over time)
public class AssetPrice 
{
    public Guid Id { get; set; }
    public Guid AssetId { get; set; }
    public DateTime Date { get; set; }
    public decimal ClosePrice { get; set; }
    
    public Asset Asset { get; set; } = null!;
}