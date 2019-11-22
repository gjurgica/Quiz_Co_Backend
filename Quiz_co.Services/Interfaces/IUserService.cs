using Quiz_co.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.Services.Interfaces
{
    public interface IUserService
    {
        UserViewModel Register(RegisterViewModel registerModel);
        UserViewModel Login(LoginViewModel loginModel);
        UserViewModel GetCurrentUser(string username);
        void Logout();
        UserViewModel GetUserById(string id);
        void UpdateUser(UserViewModel user);
        IEnumerable<UserViewModel> GetAllUsers();
    }
}
