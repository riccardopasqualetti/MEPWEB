namespace MepWeb.Service
{
    public class UserScope
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserScope(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string SV_USR_SIGLA
        {
            get
            {
                return _contextAccessor.HttpContext.Session.GetString(nameof(SV_USR_SIGLA));
            }
            set
            {
                _contextAccessor.HttpContext.Session.SetString(nameof(SV_USR_SIGLA), value);
            }
        }
        public string SV_USR_EMAIL
        {
            get
            {
                return _contextAccessor.HttpContext.Session.GetString(nameof(SV_USR_EMAIL));
            }
            set
            {
                _contextAccessor.HttpContext.Session.SetString(nameof(SV_USR_EMAIL), value);
            }
        }
    }
}
