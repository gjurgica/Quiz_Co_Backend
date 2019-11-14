using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Quiz_co.DataAccess.Interfaces;
using Quiz_co.Domain;
using Quiz_co.Services.Interfaces;
using Quiz_co.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserService(IUserRepository<User> userService, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userRepository = userService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<UserViewModel>>(_userRepository.GetAll());
        }

        public UserViewModel GetCurrentUser(string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null)
            {
                throw new Exception("User does not exists!");
            }
            return _mapper.Map<UserViewModel>(user);
        }

        public UserViewModel GetUserById(string id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                throw new Exception("User does not exists!");
            }
            return _mapper.Map<UserViewModel>(user);
        }

        public string Login(LoginViewModel loginModel)
        {
            SignInResult result = _signInManager.PasswordSignInAsync(
                loginModel.UserName,
                loginModel.Password,
                false,
                false).Result;

            if (!result.Succeeded)
            {
                return "Failed";
            }
            return "Succeeded";
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }

        public void Register(RegisterViewModel registerModel)
        {
            if (_userRepository.GetByUsername(registerModel.UserName) != null)
            {

                throw new Exception("Username already exists!");
            }
            if (registerModel.Password != registerModel.ConfirmPassword)
            {

                throw new Exception("Passwords does not match!");
            }
            var user = _mapper.Map<User>(registerModel);
            var result = _userManager.CreateAsync(user, registerModel.Password).Result;

            if (result.Succeeded)
            {
                var currentUser = _userManager.FindByNameAsync(user.UserName).Result;
                _userManager.AddToRoleAsync(currentUser, "user");
            }
            else
                throw new Exception(result.Errors.ToString());
            Login(new LoginViewModel
            {
                UserName = registerModel.UserName,
                Password = registerModel.Password
            });
        }

        public void UpdateUser(UserViewModel user)
        {
            _userRepository.Update(_mapper.Map<User>(user));
        }
    }
}
