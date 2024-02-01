using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class UserProfileRepo : Repository<UserProfileEntity>
{
    private readonly LocalUserDataContext _context;

    public UserProfileRepo(LocalUserDataContext context) : base(context)
    {
        _context = context;
    }
}
