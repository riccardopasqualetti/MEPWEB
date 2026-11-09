using Mep01Web.DTO.Request;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
        /// <summary>[231009] flusso_usr1.usr1_c_srl dell'utente collegato, letto la prima volta che serve.</summary>
        public string? SV_USR_C_SRL
        {
            get
            {
                return _contextAccessor.HttpContext?.Session.GetString(nameof(SV_USR_C_SRL));
            }
            set
            {
                if (value == null)
                {
                    _contextAccessor.HttpContext?.Session.Remove(nameof(SV_USR_C_SRL));
                }
                else
                {
                    _contextAccessor.HttpContext?.Session.SetString(nameof(SV_USR_C_SRL), value);
                }
            }
        }

        /// <summary>
        /// [231009] Stato del collegamento Teamwork per il pulsante nel menu: "S" collegato, "N" non collegato,
        /// null se non ancora letto in questa sessione (lo legge TeamworkStatoViewComponent).
        /// </summary>
        public string? SV_TW_COLLEGATO
        {
            get
            {
                return _contextAccessor.HttpContext?.Session.GetString(nameof(SV_TW_COLLEGATO));
            }
            set
            {
                if (value == null)
                {
                    _contextAccessor.HttpContext?.Session.Remove(nameof(SV_TW_COLLEGATO));
                }
                else
                {
                    _contextAccessor.HttpContext?.Session.SetString(nameof(SV_TW_COLLEGATO), value);
                }
            }
        }
        public string SV_USR_CRRG_FILTER
        {
            get
            {
                return _contextAccessor.HttpContext.Session.GetString(nameof(SV_USR_CRRG_FILTER));
            }
            set
            {
                _contextAccessor.HttpContext.Session.SetString(nameof(SV_USR_CRRG_FILTER), value);
            }
        }
    }
}
