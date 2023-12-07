namespace MepWeb.Service.Interface
{
    public interface ILoginService
    {
        Task<string> GetMepUsrSigla(string email);
    }
}
