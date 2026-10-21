using Microsoft.EntityFrameworkCore;
using AfinApp.Data.Entities;

namespace AfinApp.Data.Context;

public interface ITransactionContext : IDbContextCommon
{
    DbSet<Transaction> Transactions { get; set; }
}
