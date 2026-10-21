namespace AfinApp.Data.Entities;

// 6. Holdings (Links Investment Accounts to Assets)
public class Holding 
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public Guid AssetId { get; set; }
    public decimal Quantity { get; set; }
    public decimal AverageCostBasis { get; set; } // To calculate profit/loss
    
    public Account Account { get; set; } = null!;
    public Asset Asset { get; set; } = null!;
}