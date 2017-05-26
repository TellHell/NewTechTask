using DBModel.Models;

namespace DBModel.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool Validate(string userName, string password);
    }
}
