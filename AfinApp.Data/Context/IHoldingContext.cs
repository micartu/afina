using Microsoft.EntityFrameworkCore;
using AfinApp.Data.Entities;

namespace AfinApp.Data.Context;

public interface IHoldingContext : IDbContextCommon
{
    DbSet<Holding> Holdings { get; set; }
}