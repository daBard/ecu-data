using Infrastructure.Entities;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class AddressRepo : Repository<AddressEntity>
{
    private readonly LocalUserDataContext _context;

    public AddressRepo(LocalUserDataContext context) : base(context)
    {
        _context = context;
    }
}
