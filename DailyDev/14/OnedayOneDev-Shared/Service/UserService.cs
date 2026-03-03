using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.Identification;
using OnedayOneDev_Shared.Repository.Interface;
using OnedayOneDev_Shared.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnedayOneDev_Shared.Service
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public IUserRepository UserRepository { get; set; } = userRepository;
        public IEnumerable<User>? GetUsersList()
        {
            var Users = UserRepository.GetAllUsers();

            return Users;
        }

        public IEnumerable<User>? GetUsersByUsername(string Username)
        {
            var Users = UserRepository.GetAllUsers();

            return Users;
        }
        public bool IsUser(string Username,string password)
        {
            return UserRepository.IsUser(Username,password);

        }
    }
}
