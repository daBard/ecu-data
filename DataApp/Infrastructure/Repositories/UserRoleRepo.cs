using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class UserRoleRepo : Repository<UserRoleEntity>
{
    private readonly LocalDataContext _context;

    public UserRoleRepo(LocalDataContext context) : base(context)
    {
        _context = context;
    }
}

