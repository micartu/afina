namespace AfinApp.Data.Entities;

// 3. Transactions (For cash/bank flow)
public class Transaction 
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public Guid? CategoryId { get; set; }
    public decimal Amount { get; set; } // Positive for income, negative for expense
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public TransactionType Type { get; set; }
    
    // For Transfers: points to the destination account
    public Guid? TargetAccountId { get; set; } 
    
    public Account Account { get; set; } = null!;
    public Category? Category { get; set; }
}