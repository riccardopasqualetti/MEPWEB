using Mep01Web.DTO;
using Mep01Web.Models.Views;

namespace Mep01Web.Service.Interface
{
    public interface IMvxpa01Service
    {
		Task<ResponseBase<List<Mvxpa01>>?> GetMvxpa01Async();
    }
}
