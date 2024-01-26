using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class UserRepo : Repository<UserEntity>
{
    private readonly LocalDataContext _context;

    public UserRepo(LocalDataContext context) : base(context)
    {
        _context = context;
    }

    public List<string> GetUserNames()
    {
        //?? How did I get just part of entity using EFC
        List<string> userNames = new List<string>();
        return userNames;
    }
}
