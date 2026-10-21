using Microsoft.EntityFrameworkCore;
using AfinApp.Data.Entities;

namespace AfinApp.Data.Context;

public interface IAccountContext : IDbContextCommon
{
    DbSet<Account> Accounts { get; set; }
}

