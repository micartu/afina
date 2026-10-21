using Microsoft.EntityFrameworkCore;
using AfinApp.Data.Entities;

namespace AfinApp.Data.Context;

public interface IFamilyContext : IDbContextCommon
{
    DbSet<Family> Families { get; set; }
}
