using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.Identification;
using OnedayOneDev_Shared.Repository.Interface;
using OnedayOneDev_Shared.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnedayOneDev_Shared.Repository
{
    public class UserRepository : IUserRepository
    {
        private TaskDbContext _TaskDbContext { get; set; }
        public UserRepository(TaskDbContext taskDbContext)
        {
            _TaskDbContext = taskDbContext;


            if (_TaskDbContext.Users.Any())
            {

                _TaskDbContext.Add(new User
                {
                    UserName = "admin",
                    Password = "admin",
                    Role = "admin"

                });


                _TaskDbContext.SaveChanges();
            }
        }

        public IEnumerable<User>? GetAllUsers()
        {
            try
            {
                var Users = _TaskDbContext.Users;

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

                return _TaskDbContext.Users.Find(UserName);
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

    }
}
