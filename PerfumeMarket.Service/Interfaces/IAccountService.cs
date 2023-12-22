using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Service.Interfaces
{
    public interface IAccountService
    {
        BaseResponse<ClaimsIdentity> Register(RegisterViewModel model);

        BaseResponse<ClaimsIdentity> Login(LoginViewModel model);

        BaseResponse<bool> ChangePassword(ChangePasswordViewModel model);
    }
}
