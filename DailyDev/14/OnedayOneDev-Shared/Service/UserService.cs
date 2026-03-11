using Microsoft.VisualBasic;
using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.Identification;
using OnedayOneDev_Shared.Repository;
using OnedayOneDev_Shared.Repository.Interface;
using OnedayOneDev_Shared.ResultData;
using OnedayOneDev_Shared.Service.Interface;
using OnedayOneDev_Shared.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnedayOneDev_Shared.Service
{
    public class UserService(IUserRepository userRepository, ILog _LogHandler,
        IDateTimeProvider _DateTimeProvider) : IUserService
    {
        public IUserRepository UserRepository { get; set; } = userRepository;
        private readonly IDateTimeProvider _DateTime = _DateTimeProvider;
        ILog _LogHandler { get; set; } = _LogHandler;
        public IEnumerable<User>? GetUsersList()
        {
            var Users = UserRepository.GetAllUsers();

            return Users;
        }

        public User? GetUsersByUsername(string Username)
        {
            var Users = UserRepository.GetUserByUserName(Username);

            return Users;
        }
        public bool IsUser(string Username,string password)
        {
            return UserRepository.IsUser(Username,password);

        }
        private bool HasUsers()
        {
            return UserRepository.HasUsers() > 0 ? true : false;
        }
        public Result<User> DeleteUser(int identifiant)
        {

            var user = UserRepository.GetUserById(identifiant);

            if (user == null)
                return Result<User>.Failed("Utilisateur introuvable");

            if (user.Role == UserRole.ADMIN)
            {
                var adminCount = UserRepository.GetAllUsers()?.Count(u => u.Role == UserRole.ADMIN) ?? 0;

                if (adminCount <= 1)
                    return Result<User>.Failed("Impossible de supprimer le dernier administrateur");
            }

            return UserRepository.DeleteUser(identifiant);
        }
       
        public Result<User> UpdateUser(int identifiant, string NewName,string NewPassword, UserRole NewRole)
        {
            if (!HasUsers())
            {
                return Result<User>.Failed("Aucun utilisateur n'as été trouvée");
            }
            if (string.IsNullOrWhiteSpace(NewName))
                return Result<User>.Failed("Le nom ne peut pas être vide");
            if (string.IsNullOrWhiteSpace(NewPassword))
                return Result<User>.Failed("Le mot de passe ne peut pas être vide");

            var AlreadyExists = UserRepository.GetUserByUserName(NewName);

            if (AlreadyExists != null)
            {
                return Result<User>.Failed("Un autre utilisateur possédant ce nom existe déja");
            }

            var User = UserRepository.GetUserById(identifiant);

            if (User == null)
            {
                return Result<User>.Failed($"Aucune tâche ne correspond à l'identifiant {identifiant}");
            }

            User.UserName = NewName;
            User.Password = NewPassword;
            User.Role = NewRole;

            return UserRepository.UpdateUser(identifiant, User);

        }

        public Result<User> CreateNewUser(string? UserName, string? _Password, UserRole Role = UserRole.USER)
        {

            if (string.IsNullOrWhiteSpace(UserName))
                return Result<User>.Failed("Le nom d'utilisateur ne peut pas être vide.");
            if (string.IsNullOrWhiteSpace(_Password))
                return Result<User>.Failed("Le mot de passe ne peut pas être vide.");


            var normalized = UserName.Trim();
            var AlreadyExists = UserRepository.GetUserByUserName(normalized);

            if (AlreadyExists != null)
            {
                return Result<User>.Failed("Un autre utilisateur possédant ce nom existe déja");
            }
            else
            {

                var result = UserRepository.AddUser(new User(userName : normalized,
                                                            password : _Password,
                                                            role : Role));

                if (result.Success)
                {
                    _LogHandler.AddLog($"Création d'un nouvel utilisateur le {_DateTime.Today.ToString("dd/MM/yyyy")} \n UserName : {normalized} ");
                }

                return result;

            }

        }
    }
}
