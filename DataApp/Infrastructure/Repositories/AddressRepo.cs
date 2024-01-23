using Infrastructure.Entities;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class AddressRepo : Repository<AddressEntity>
{
    private readonly LocalDataContext _context;

    public AddressRepo(LocalDataContext context) : base(context)
    {
        _context = context;
    }
}
