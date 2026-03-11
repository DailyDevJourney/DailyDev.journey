using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.Identification;
using OnedayOneDev_Shared.Repository.Interface;
using OnedayOneDev_Shared.ResultData;

namespace OnedayOneDev_Shared.Service.Interface
{
    public interface IUserService
    {
        IUserRepository UserRepository { get; set; }

        Result<User> CreateNewUser(string? UserName, string? _Password, UserRole Role = UserRole.USER);
        Result<User> DeleteUser(int identifiant);
        User? GetUsersByUsername(string Username);
        IEnumerable<User>? GetUsersList();
        bool IsUser(string Username, string password);
        Result<User> UpdateUser(int identifiant, string NewName, string NewPassword, UserRole NewRole);
    }
}