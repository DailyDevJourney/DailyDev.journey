using OnedayOneDev_Shared.Identification;
using OnedayOneDev_Shared.ResultData;

namespace OnedayOneDev_Shared.Repository.Interface
{
    public interface IUserRepository
    {
        Result<User> AddUser(User user);
        Result<User> DeleteUser(int id);
        IEnumerable<User>? GetAllUsers();
        User? GetUserByUserName(string UserName);
        User? GetUserById(int id);
        int? HasUsers();
        bool IsUser(string UserName, string password);
        Result<User> UpdateUser(int id, User NewUser);
        bool VerifyPassword(string UserName, string password);
    }
}