using Microsoft.EntityFrameworkCore;
using PerfumeMarket.DAL.Interfaces;
using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Enum;
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
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Order> _orderRepository;
        public OrderService(IBaseRepository<User> userRepository, IBaseRepository<Order> orderRepository) 
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }
        public IBaseResponse<Order> Create(CreateOrderViewModel model)
        {
            try
            {
                User? user = _userRepository.GetAll()
                    .Include(x => x.Basket)
                    .FirstOrDefault(x => x.Name == model.Login);
                if (user == null)
                {
                    return new BaseResponse<Order>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var order = new Order()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    Address = model.Address,
                    DateCreated = DateTime.Now,
                    BasketId = user.Basket.Id,
                    PerfumeId = model.PerfumeId
                };

                _orderRepository.Create(order);

                return new BaseResponse<Order>()
                {
                    Description = "Заказ создан",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<bool> Delete(long id)
        {
            try
            {
                Order? order = _orderRepository.GetAll()
                    .Include(x => x.Basket)
                    .FirstOrDefault(x => x.Id == id);

                if (order == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.OrderNotFound,
                        Description = "Заказ не найден"
                    };
                }

                _orderRepository.Delete(order);
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.OK,
                    Description = "Заказ удален"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
