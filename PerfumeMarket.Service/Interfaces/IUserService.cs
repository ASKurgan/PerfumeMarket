using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Service.Interfaces
{
    public interface IUserService
    {
       IBaseResponse<User> Create (UserViewModel model);
       BaseResponse<Dictionary<int, string>> GetRoles();
       BaseResponse<IEnumerable<UserViewModel>> GetUsers();
       IBaseResponse<bool> DeleteUser(long id); 
    }
}
