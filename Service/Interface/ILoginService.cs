using MepWeb.DTO.Request;

namespace MepWeb.Service.Interface
{
    public interface ILoginService
    {
        Task<string> GetMepUsrSigla(string email);
        Task<HttpResponseMessage> LogInAsync(LoginRequest loginRequest);
    }
}
