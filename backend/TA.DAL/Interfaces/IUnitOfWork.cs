namespace TA.DAL.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    ICategoryRepository Categories { get; }
    ITaskRepository Tasks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}