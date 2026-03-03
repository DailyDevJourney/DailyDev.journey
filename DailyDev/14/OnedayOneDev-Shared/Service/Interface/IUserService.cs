using OnedayOneDev_Shared.Identification;
using OnedayOneDev_Shared.Repository.Interface;

namespace OnedayOneDev_Shared.Service.Interface
{
    public interface IUserService
    {
        IUserRepository UserRepository { get; set; }

        IEnumerable<User>? GetUsersByUsername(string Username);
        IEnumerable<User>? GetUsersList();
        bool IsUser(string Username, string password);
    }
}