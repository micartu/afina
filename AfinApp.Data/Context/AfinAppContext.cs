namespace AfinApp.Data.Context;

using AfinApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class AfinAppContext(DbContextOptions<AfinAppContext> options) : DbContext(options),
    IUsersContext,
    IAccountRelevantContext,
    IAccountContext,
    ITransactionRelevantContext,
    ITransactionContext,
    ICategoryRelevantContext,
    ICategoryContext,
    IAssetContext,
    IAssetRelevantContext,
    IHoldingContext,
    IHoldingRelevantContext,
    IFamilyRelevantContext,
    IFamilyContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Family> Families { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Holding> Holdings { get; set; }
    public DbSet<AssetPrice> AssetPrices { get; set; }
}