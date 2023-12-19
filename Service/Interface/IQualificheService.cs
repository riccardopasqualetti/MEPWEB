using Mep01Web.DTO;
using Mep01Web.Models.Views;
using MepWeb.DTO.Response;

namespace MepWeb.Service.Interface
{
    public interface IQualificheService
    {
        Task<ResponseBase<List<Mvxzz12>?>> GetAllFromMvxzz12Async();
    }
}
