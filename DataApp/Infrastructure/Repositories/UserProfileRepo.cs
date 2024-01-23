using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class UserProfileRepo : Repository<UserProfileEntity>
{
    private readonly LocalDataContext _context;

    public UserProfileRepo(LocalDataContext context) : base(context)
    {
        _context = context;
    }
}
