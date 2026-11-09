using Mep01Web.DTO;
using Mep01Web.Models;

namespace MepWeb.Service.Interface
{
    /// <summary>[231009] Connessione Teamwork dell'utente, con il token già decifrato.</summary>
    public class TeamworkConnessione
    {
        public decimal Usr1CSrl { get; init; }
        public long TwUserId { get; init; }
        /// <summary>Indirizzo del sito Teamwork, sempre con la barra finale (es. https://sataconsulting.teamwork.com/).</summary>
        public string ApiEndpoint { get; init; } = "";
        public string AccessToken { get; init; } = "";
    }

    /// <summary>
    /// [231009] Login Teamwork per utente (App Login Flow / OAuth) e conservazione del token in flusso_usr1_tw.
    /// Gli utenti MEPWEB sono identificati da flusso_usr1.usr1_c_srl.
    /// </summary>
    public interface ITeamworkAuthService
    {
        /// <summary>False se manca ClientId o ClientSecret nella configurazione.</summary>
        bool IsConfigurato { get; }

        /// <summary>usr1_c_srl dell'utente collegato a MEPWEB (dall'email di login, poi tenuto in sessione). Null se non trovato.</summary>
        Task<decimal?> GetUsr1CSrlCorrenteAsync();

        /// <summary>Indirizzo della pagina di login Teamwork verso cui mandare l'utente.</summary>
        string GetLoginUrl(string redirectUri, string state);

        /// <summary>
        /// Scambia il code ricevuto nella callback con l'access token, verifica che l'account Teamwork abbia la stessa
        /// email dell'utente MEPWEB (flusso_usr1.usr1_e_mail) e salva il collegamento.
        /// </summary>
        Task<ResponseBase> CompletaLoginAsync(string code, string redirectUri, decimal usr1CSrl);

        /// <summary>Riga del collegamento senza token, per mostrare lo stato. Null se l'utente non è collegato.</summary>
        Task<FlussoUsr1Tw?> GetCollegamentoAsync(decimal usr1CSrl);

        /// <summary>Connessione con token decifrato. Null se l'utente non è collegato o il token non è più leggibile.</summary>
        Task<TeamworkConnessione?> GetConnessioneAsync(decimal usr1CSrl);

        Task ScollegaAsync(decimal usr1CSrl);

        /// <summary>
        /// Chiama /projects/api/v3/me.json con il token dell'utente. Se Teamwork risponde 401 il collegamento viene
        /// eliminato e l'utente deve rifare il login.
        /// </summary>
        Task<ResponseBase<string>> VerificaAsync(decimal usr1CSrl);
    }
}
