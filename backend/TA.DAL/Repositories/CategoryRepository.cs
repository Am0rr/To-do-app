using TA.DAL.Entities.Tasks;
using TA.DAL.Interfaces;
using TA.DAL.Persistence;

namespace TA.DAL.Repositories;

public class CategoryRepository(AppDbContext context)
    : BaseRepository<Category>(context), ICategoryRepository;