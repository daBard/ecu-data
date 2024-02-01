using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class UserRepo : Repository<UserEntity>
{
    private readonly LocalUserDataContext _context;

    public UserRepo(LocalUserDataContext context) : base(context)
    {
        _context = context;
    }
}
