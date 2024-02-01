using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class RoleRepo : Repository<RoleEntity>
{
    private readonly LocalUserDataContext _context;

    public RoleRepo(LocalUserDataContext context) : base(context)
    {
        _context = context;
    }
}
