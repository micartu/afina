using Microsoft.EntityFrameworkCore;
using AfinApp.Data.Entities;

namespace AfinApp.Data.Context;

public interface IUsersContext : IDbContextCommon
{
    DbSet<User> Users { get; set; }
}
