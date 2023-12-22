using Microsoft.EntityFrameworkCore;
using PerfumeMarket.DAL.Interfaces;
using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Enum;
using PerfumeMarket.Domain.Extentions;
using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.Perfume;
using PerfumeMarket.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Service.Implementations
{
    public class PerfumeService : IPerfumeService
    {
        private readonly IBaseRepository <Perfume> _perfumeRepository;
        public PerfumeService (IBaseRepository <Perfume> perfumeRepository)
        
        {
            _perfumeRepository = perfumeRepository;
        }

        public IBaseResponse<Perfume> Create(PerfumeViewModel model, byte[] imageData)
        {
            
            try
            {
                Perfume perfume = new()
                {
                  Name = model.Name,
                  Description = model.Description,
                  Brand = model.Brand,
                  GroupOfFragrances = model.GroupOfFragrances,
                  Price = model.Price,
                  DateCreate = DateTime.Now,
                  TypePerfume = (TypePerfume)Convert.ToInt32(model.TypePerfume),
                  Avatar = imageData
                };
                 _perfumeRepository.Create(perfume);
                
                return new BaseResponse<Perfume>()
                {
                    Data = perfume,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {

                return new BaseResponse<Perfume>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<bool> DeletePerfume(long id)
        {
            try
            {
                Perfume? perfume = _perfumeRepository.GetAll().FirstOrDefault(p => p.Id == id);
                
                if (perfume != null) 
                {
                   _perfumeRepository.Delete(perfume);
                }
                
                else
                
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse<bool>()
                {
                    Description = $"[DeletePerfume] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                }; 
            }
        }

        public IBaseResponse<Perfume> Edit(long id, PerfumeViewModel model)
        {
            try
            {
                Perfume? perfume = _perfumeRepository.GetAll().FirstOrDefault(p => p.Id == id);
                if (perfume == null)
                {
                    return new BaseResponse<Perfume>()
                    {
                        StatusCode = StatusCode.PerfumeNotFound,
                        Description = "Perfume not found"
                    };

                }

                perfume.Price = model.Price;
                perfume.Description = model.Description;
                perfume.GroupOfFragrances = model.GroupOfFragrances;
                perfume.Brand = model.Brand;
                perfume.DateCreate = DateTime.ParseExact(model.DateCreate, "yyyyMMdd HH:mm", null);
                perfume.Name = model.Name;

                _perfumeRepository.Update(perfume);

                return new BaseResponse<Perfume>() 
                {
                   Data = perfume,
                   StatusCode= StatusCode.OK    
                };
            }
            catch (Exception ex)
            {

                return new BaseResponse<Perfume>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            };
        }

        public IBaseResponse<PerfumeViewModel> GetPerfume(long id)
        {
            try
            {
                Perfume? perfume = _perfumeRepository.GetAll().FirstOrDefault(p => p.Id == id);
                
                if (perfume == null)
                
                {
                    return new BaseResponse<PerfumeViewModel>()
                    {
                        Description = "Аромат не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                PerfumeViewModel perfumeViewModel = new PerfumeViewModel() 
                {
                   DateCreate = perfume.DateCreate.ToLongDateString(),
                   Name = perfume.Name,
                   Brand = perfume.Brand,
                   Description = perfume.Description,
                   Price = perfume.Price,
                   TypePerfume = perfume.TypePerfume.GetDisplayName(),
                   GroupOfFragrances = perfume.GroupOfFragrances,
                };

                return new BaseResponse <PerfumeViewModel> ()
                {
                    StatusCode = StatusCode.OK,
                    Data = perfumeViewModel
                };

            }
            catch (Exception ex)
            {
               return new BaseResponse<PerfumeViewModel>()
                {
                    Description = $"[GetPerfume] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<Dictionary<long, string>> GetPerfume(string term)
        {
            BaseResponse<Dictionary<long,string>> baseResponse = new();
            try
            {
                Dictionary<long, string> perfume = _perfumeRepository.GetAll()
                    .Select(x => new PerfumeViewModel()

                    {
                        Id = x.Id,
                        Brand = x.Brand,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        TypePerfume = x.TypePerfume.GetDisplayName(),
                        GroupOfFragrances = x.GroupOfFragrances,
                        DateCreate = x.DateCreate.ToLongDateString(),

                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionary(x => x.Id, t => t.Name);

                baseResponse.Data = perfume;
                return baseResponse;
            }
            catch (Exception ex)
            {

                return new BaseResponse<Dictionary<long, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Perfume>> GetPerfumes()
        {
            try
            {
                List<Perfume> perfumes = _perfumeRepository.GetAll().ToList();
                if ( !perfumes.Any())
                {
                    return new BaseResponse<List<Perfume>>() 
                    {
                     Description = "Найдено ноль элементов", 
                     StatusCode = StatusCode.OK 
                    };
                }
                return new BaseResponse<List<Perfume>>() { Data = perfumes, StatusCode = StatusCode.OK };
            }
            catch (Exception ex)
            {

                return new BaseResponse<List<Perfume>>()
                {
                    Description = $"[GetPerfumes] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        

        public BaseResponse<Dictionary<int, string>> GetTypes()
        {
            try
            {
                Dictionary<int, string>? types = ((TypePerfume[])Enum.GetValues(typeof(TypePerfume)))
                     .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = types,
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
    }
}
