namespace AfinApp.Data.Entities;

using System.ComponentModel.DataAnnotations;

// 2. Accounts (Bank, Cash, and Investment accounts live here)
public class Account 
{
    public Guid Id { get; set; }
    public Guid FamilyId { get; set; } // Shared across the family
    public string Name { get; set; } = string.Empty; // e.g., "Chase Checking", "Vanguard Brokerage"
    public AccountType Type { get; set; }
    public string Currency { get; set; } = "USD";
    public decimal InitialBalance { get; set; }
    
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public ICollection<Holding> Holdings { get; set; } = new List<Holding>(); // Only used if Type == Investment
}