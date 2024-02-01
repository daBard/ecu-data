using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class UserRoleRepo : Repository<UserRoleEntity>
{
    private readonly LocalUserDataContext _context;

    public UserRoleRepo(LocalUserDataContext context) : base(context)
    {
        _context = context;
    }
}

