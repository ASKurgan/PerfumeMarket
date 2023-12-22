using Microsoft.Extensions.Logging;
using PerfumeMarket.DAL.Interfaces;
using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Enum;
using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.Profile;
using PerfumeMarket.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Service.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IBaseRepository<Profile> _profileRepository;
        private readonly ILogger<ProfileService> _logger;
        public ProfileService(IBaseRepository<Profile> profileRepository, ILogger<ProfileService> logger) 
        {
            _profileRepository = profileRepository;
            _logger = logger;
        }


        public BaseResponse<ProfileViewModel> GetProfile(string userName)
        {
            try
            {
                ProfileViewModel? profile = _profileRepository.GetAll()
                    .Select(x => new ProfileViewModel()
                    {
                        Id = x.Id,
                        Address = x.Address,
                        Age = x.Age,
                        UserName = x.User.Name
                    })
                    .FirstOrDefault(x => x.UserName == userName);

                if (profile == null)
                {
                    _logger.LogError($"[ProfileService.GetProfile]");
                    return new BaseResponse<ProfileViewModel>()
                    {
                        Description = "Профиль не найден",
                        StatusCode = StatusCode.InternalServerError
                    };
                }

                return new BaseResponse<ProfileViewModel>()
                {
                    Data = profile,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ProfileService.GetProfile] error: {ex.Message}");
                return new BaseResponse<ProfileViewModel>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public BaseResponse<Profile> Save(ProfileViewModel model)
        {
            try 
            {
                Profile? profile = _profileRepository.GetAll().FirstOrDefault(p => p.Id == model.Id );
                
                if (profile == null)
                {
                    _logger.LogError($"[ProfileService.Save]");
                    return new BaseResponse<Profile>()
                    {
                      Description = "Профиль не найден",
                      StatusCode = StatusCode.InternalServerError
                    };
                }

                profile.Address = model.Address;
                profile.Age = model.Age;

                _profileRepository.Update(profile);

                return new BaseResponse<Profile>()
                {
                    Data = profile,
                    Description = "Данные обновлены",
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ProfileService.Save] error: {ex.Message}");
                return new BaseResponse<Profile>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
