using GoogleApi.Entities;
using MailChimp.Net.Core;
using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.Perfume;
using Unipluss.Sign.Client.Code;

namespace PerfumeMarket.Service.Interfaces
{
    public interface IPerfumeService
    {
        BaseResponse<Dictionary<int, string>> GetTypes();
        IBaseResponse<List<Perfume>> GetPerfumes();
        IBaseResponse<PerfumeViewModel> GetPerfume(long id);
        BaseResponse<Dictionary<long, string>> GetPerfume(string term);
        IBaseResponse<Perfume> Create(PerfumeViewModel model, byte[] imageData);
        IBaseResponse<bool> DeletePerfume(long id);
        IBaseResponse<Perfume> Edit(long id, PerfumeViewModel model);

    }
}
