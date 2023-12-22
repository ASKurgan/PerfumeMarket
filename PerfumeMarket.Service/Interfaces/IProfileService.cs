using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Service.Interfaces
{
    public interface IProfileService
    {
        BaseResponse<ProfileViewModel> GetProfile(string userName);

        BaseResponse<Profile> Save(ProfileViewModel model);
    }
}
