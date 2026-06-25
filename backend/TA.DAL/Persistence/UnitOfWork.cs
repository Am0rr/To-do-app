using TA.DAL.Interfaces;

namespace TA.DAL.Persistence;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; }
    public IRefreshTokenRepository RefreshTokens { get; }
    public ICategoryRepository Categories { get; }
    public ITaskRepository TaskItems { get; }

    public UnitOfWork(AppDbContext context,
        IUserRepository userRepository,
        ICategoryRepository categoryRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ITaskRepository taskRepository)
    {
        _context = context;

        Users = userRepository;
        Categories = categoryRepository;
        RefreshTokens = refreshTokenRepository;
        TaskItems = taskRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}