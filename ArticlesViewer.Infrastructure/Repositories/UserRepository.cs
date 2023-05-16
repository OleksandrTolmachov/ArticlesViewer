using ArticlesViewer.Domain;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticlesViewer.Infrastructure;

public class UserRepository : Repository<User>
{
    public UserRepository(AppDbContext context) : base(context)
    { }

    public override async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(x => x.WrittenArticles)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
