using Microsoft.EntityFrameworkCore;
using PerfumeMarket.DAL.Interfaces;
using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Enum;
using PerfumeMarket.Domain.Extentions;
using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.Order;
using PerfumeMarket.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Service.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Perfume> _perfumeRepository;

        public BasketService(IBaseRepository<User> userRepository, IBaseRepository<Perfume> perfumeRepository)
        {
            _userRepository = userRepository;
            _perfumeRepository = perfumeRepository;
        }
        public IBaseResponse<OrderViewModel> GetItem(string userName, long id)
        {
            try
            {
              User? user =  _userRepository.GetAll()
                     .Include(x => x.Basket)
                     .ThenInclude(x => x.Orders)
                     .FirstOrDefault(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                List<Order>? orders = user.Basket.Orders.Where(x => x.Id == id).ToList();

                if (orders == null || orders.Count() == 0)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = "Заказов нет",
                        StatusCode = StatusCode.OrderNotFound
                    };
                }

                OrderViewModel? response = (from p in orders
                                            join c in _perfumeRepository.GetAll() on p.PerfumeId equals c.Id
                                            select new OrderViewModel()
                                            {
                                               Id = p.Id,
                                               PerfumeName = c.Name,
                                               Brand = c.Brand,
                                               TypePerfume = c.TypePerfume.GetDisplayName(),
                                               Address = p.Address,
                                               FirstName = p.FirstName,
                                               LastName = p.LastName,
                                               MiddleName = p.MiddleName,
                                               DateCreate = p.DateCreated.ToLongDateString(),
                                               Image = c.Avatar

                                            }).FirstOrDefault();
                
                if (response == null)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = "Заказов нет",
                        StatusCode = StatusCode.OrderNotFound
                    };
                }

                return new BaseResponse<OrderViewModel>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex) 
            {
                return new BaseResponse<OrderViewModel>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<IEnumerable<OrderViewModel>> GetItems(string userName)
        {
           try
            {
                User? user = _userRepository.GetAll()
                     .Include(x => x.Basket)
                     .ThenInclude(x => x.Orders)
                     .FirstOrDefault(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<IEnumerable<OrderViewModel>>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                List<Order>? orders = user.Basket?.Orders;
                var response = from o in orders
                               join p in _perfumeRepository.GetAll() on o.PerfumeId equals p.Id
                               select new OrderViewModel()
                               {
                                   Id = o.Id,
                                   PerfumeName = p.Name,
                                   Brand = p.Brand,
                                   TypePerfume = p.TypePerfume.GetDisplayName(),
                                   Image = p.Avatar,
                                   GroupOfFragrance = p.GroupOfFragrances
                               };

                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex) 
            {
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }
    }
}
