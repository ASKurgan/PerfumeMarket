using Microsoft.Extensions.Logging;
using PerfumeMarket.DAL.Interfaces;
using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Enum;
using PerfumeMarket.Domain.Extentions;
using PerfumeMarket.Domain.Helpers;
using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.User;
using PerfumeMarket.Service.Interfaces;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Service.Implementations
{
    public class UserService : IUserService
    {  
        private readonly ILogger<UserService> _logger;
        private readonly IBaseRepository<User> _userRepository ;
        private readonly IBaseRepository <Profile> _profileRepository;
        public UserService(ILogger<UserService> logger,IBaseRepository<User> user,
               IBaseRepository<Profile> profile) 
        {
          _logger = logger;
          _userRepository = user;
          _profileRepository = profile;
        }
        public IBaseResponse<User> Create(UserViewModel model)
        {
           try
            {
              User? user =  _userRepository.GetAll().FirstOrDefault(x => x.Name == model.Name);
                if (user != null)
                {
                    return new BaseResponse<User>()
                    {
                       Description = "Пользователь с таким логином уже есть",
                       StatusCode = Domain.Enum.StatusCode.UserAlreadyExists
                    };
                }
                user = new User()
                {
                    Name = model.Name,
                    Role = Enum.Parse<Role>(model.Role),
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                };

                _userRepository.Create(user);

                Profile? profile = new Profile()
                {
                    Address = string.Empty,
                    Age = 0,
                    UserId = user.Id,
                };

                _profileRepository.Create(profile);

                return new BaseResponse<User>()
                {
                    Data = user,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"[UserService.Create] error: {ex.Message}");
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public IBaseResponse<bool> DeleteUser(long id)
        {
           try
            {
                User? user =  _userRepository.GetAll().FirstOrDefault(u => u.Id == id);
                
                if (user == null)
                {
                    return new BaseResponse<bool>() 
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Пользователь не найден",
                        Data = false
                    };
                }

                _userRepository.Delete(user);
                _logger.LogInformation($"[UserService.DeleteUser] пользователь удален");
                
                
                return new BaseResponse<bool>()
                { 
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }

            catch (Exception ex) 
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
                Dictionary<int, string>? roles = ((Role[])Enum.GetValues(typeof(Role)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());
                
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<IEnumerable<UserViewModel>> GetUsers()
        {
            try
            {
                var users = _userRepository.GetAll()
                    .Select(u => new UserViewModel()
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Role = u.Role.GetDisplayName()                    
                    })
                    .ToList() ;
               
                _logger.LogInformation($"[UserService.GetUsers] получено элементов {users.Count}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex) 
            {
                _logger.LogError(ex, $"[UserSerivce.GetUsers] error: {ex.Message}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
        
    }
}
