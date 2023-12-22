using Microsoft.Extensions.Logging;
using PerfumeMarket.DAL.Interfaces;
using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Enum;
using PerfumeMarket.Domain.Helpers;
using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.Account;
using PerfumeMarket.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Service.Implementations
{
    public class AccountService:IAccountService
    {
        private readonly IBaseRepository<Profile> _proFileRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Basket> _basketRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IBaseRepository<User> userRepository,
            ILogger<AccountService> logger, IBaseRepository<Profile> proFileRepository,
            IBaseRepository<Basket> basketRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _proFileRepository = proFileRepository;
            _basketRepository = basketRepository;
        }

        static ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
        public BaseResponse<bool> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                User? user = _userRepository.GetAll().FirstOrDefault(x => x.Name == model.UserName);
                
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Пользователь не найден"
                    };
                }

                user.Password = HashPasswordHelper.HashPassowrd(model.NewPassword);
                _userRepository.Update(user);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Пароль обновлен"
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ChangePassword]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<ClaimsIdentity> Login(LoginViewModel model)
        {
            try
            {
                User? user = _userRepository.GetAll().FirstOrDefault(x => x.Name == model.Name);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }

                if (user.Password != HashPasswordHelper.HashPassowrd(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или логин"
                    };
                }
                ClaimsIdentity? result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<ClaimsIdentity> Register(RegisterViewModel model)
        {
            try
            {
              User? user = _userRepository.GetAll().FirstOrDefault(u => u.Name == model.Name);
                
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином уже есть",
                    };
                }

                user = new User()
                {
                    Name = model.Name,
                    Role = Role.User,
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                };

                _userRepository.Create(user);

                var profile = new Profile()
                { 
                    UserId = user.Id 
                };

                var basket = new Basket()
                {
                    UserId = user.Id
                };

                _proFileRepository.Create(profile);
                _basketRepository.Create(basket);
                ClaimsIdentity? result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Объект добавился",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }

            //static ClaimsIdentity Authenticate(User user)
            //{
            //    var claims = new List<Claim>
            //{
            //    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
            //    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            //};
            //    return new ClaimsIdentity(claims, "ApplicationCookie",
            //        ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            //}
        }
    }
}
