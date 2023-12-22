using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Service.Interfaces
{
    public interface IOrderService
    {
        IBaseResponse<Order> Create(CreateOrderViewModel model);

        IBaseResponse<bool> Delete(long id);
    }
}
