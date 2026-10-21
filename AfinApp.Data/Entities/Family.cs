namespace AfinApp.Data.Entities;

// 1. Family & Users (Supports multiple family members)
public class Family 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = new List<User>();
}