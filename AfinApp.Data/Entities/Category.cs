namespace AfinApp.Data.Entities;

// 4. Categories
public class Category 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; // e.g., "Groceries", "Salary"
    public TransactionType Type { get; set; } // Income or Expense
    public string HexColor { get; set; } = "#000000";
}