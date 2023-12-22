using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Service.Interfaces
{
    public interface IBasketService
    {
        IBaseResponse<IEnumerable<OrderViewModel>> GetItems(string userName);

        IBaseResponse<OrderViewModel> GetItem(string userName, long id);
    }
}
