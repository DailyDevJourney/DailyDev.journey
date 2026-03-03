using OnedayOneDev_Shared.Identification;

namespace OnedayOneDev_Shared.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User>? GetAllUsers();
        User? GetUserByUserName(string UserName);
        bool IsUser(string UserName, string password);
        bool VerifyPassword(string UserName, string password);
    }
}