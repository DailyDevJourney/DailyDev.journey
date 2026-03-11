using Microsoft.AspNetCore.Http;
using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.Identification;
using OnedayOneDev_Shared.Repository.Interface;
using OnedayOneDev_Shared.ResultData;
using OnedayOneDev_Shared.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnedayOneDev_Shared.Repository
{
    public class UserRepository : IUserRepository
    {
        private TaskDbContext _DbContext { get; set; }
        public UserRepository(TaskDbContext taskDbContext)
        {
            _DbContext = taskDbContext;


            if (!_DbContext.Users.Any())
            {

                _DbContext.Add(new User
                {
                    UserName = "admin",
                    Password = "admin",
                    Role = UserRole.ADMIN

                });


                _DbContext.SaveChanges();
            }
        }

        public IEnumerable<User>? GetAllUsers()
        {
            try
            {
                var Users = _DbContext.Users;

                return Users.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User? GetUserByUserName(string UserName)
        {
            try
            {

                return _DbContext.Users.FirstOrDefault(u => u.UserName.ToLower() == UserName.ToLower());
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool VerifyPassword(string UserName,string password)
        {
            try
            {
                var user = GetUserByUserName(UserName);

                if (user == null)
                    return false;

                if (user?.Password == password)
                {
                    return true;
                }
                    

                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool IsUser(string UserName, string password)
        {
            try
            {
                return VerifyPassword(UserName, password);
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public int? HasUsers()
        {
            try
            {
                var usersList = _DbContext.Users.ToList();

                return usersList.Count;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User? GetUserById(int id)
        {
            try
            {

                return _DbContext.Users.Find(id);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public Result<User> UpdateUser(int id, User NewUser)
        {
            try
            {

                var entity = _DbContext.Users.Find(id);

                if (entity != null)
                {
                    _DbContext.Entry(entity).CurrentValues.SetValues(NewUser);
                    _DbContext.SaveChanges();
                    return Result<User>.Ok(entity, "Mise à jour réussi");
                }
                else
                {
                    return Result<User>.Failed("tache inexistante");
                }

            }
            catch (Exception ex)
            {

                return Result<User>.Failed($"erreur : {ex.Message}"); ;
            }

        }

        public Result<User> DeleteUser(int id)
        {
            try
            {
                

                var entity = _DbContext.Users.Find(id);

                if (entity != null)
                {
                    _DbContext.Users.Remove(entity);
                    _DbContext.SaveChanges();
                    return Result<User>.Ok(null, "suppression réussi");
                }
                else
                {
                    return Result<User>.Failed("utilisateur inexistant");
                }

            }
            catch (Exception ex)
            {

                return Result<User>.Failed($"erreur : {ex.Message}"); ;
            }

        }

        public Result<User> AddUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return Result<User>.Failed("Erreur informations utilisateur = null");
                }

                _DbContext.Users.Add(user);
                _DbContext.SaveChanges();

                return Result<User>.Ok(user, "Ajout réussi");
            }
            catch (Exception ex)
            {
                return Result<User>.Failed($"Erreur : {ex.Message}");
            }

        }

    }
}
