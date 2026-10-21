using Microsoft.EntityFrameworkCore;
using AfinApp.Data.Entities;

namespace AfinApp.Data.Context;

public interface IAssetPriceContext : IDbContextCommon
{
    DbSet<AssetPrice> AssetPrices { get; set; }
}
