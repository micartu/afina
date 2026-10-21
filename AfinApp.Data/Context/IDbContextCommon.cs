namespace AfinApp.Data.Context;

public interface IDbContextCommon : IDisposable
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}