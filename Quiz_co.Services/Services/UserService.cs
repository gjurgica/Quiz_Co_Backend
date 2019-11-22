using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quiz_co.DataAccess.Interfaces;
using Quiz_co.Domain;
using Quiz_co.Services.Helpers;
using Quiz_co.Services.Interfaces;
using Quiz_co.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Quiz_co.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _options;

        public UserService(IUserRepository<User> userService, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IOptions<AppSettings> options)
        {
            _userRepository = userService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _options = options;
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

        public UserViewModel Login(LoginViewModel loginModel)
        {
            var user = _userRepository.GetByUsername(loginModel.UserName);
            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name,
                        $"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.NameIdentifier,
                        user.Id)
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            SignInResult result = _signInManager.PasswordSignInAsync(
                loginModel.UserName,
                loginModel.Password,
                false,
                false).Result;

            if (!result.Succeeded)
            {
                return null;
            }
            var loggedUser = _mapper.Map<UserViewModel>(user);
            loggedUser.Token = tokenHandler.WriteToken(token);
            return loggedUser;
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }

        public UserViewModel Register(RegisterViewModel registerModel)
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
           return  Login(new LoginViewModel
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
