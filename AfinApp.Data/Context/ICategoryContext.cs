using Microsoft.EntityFrameworkCore;
using AfinApp.Data.Entities;

namespace AfinApp.Data.Context;

public interface ICategoryContext : IDbContextCommon
{
    DbSet<Category> Categories { get; set; }
}