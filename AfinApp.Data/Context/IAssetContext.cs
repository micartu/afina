using Microsoft.EntityFrameworkCore;
using AfinApp.Data.Entities;

namespace AfinApp.Data.Context;

public interface IAssetContext : IDbContextCommon
{
    DbSet<Asset> Assets { get; set; }
}


