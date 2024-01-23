using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class RoleRepo : Repository<RoleEntity>
{
    private readonly LocalDataContext _context;

    public RoleRepo(LocalDataContext context) : base(context)
    {
        _context = context;
    }
}
