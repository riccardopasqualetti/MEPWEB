using Mep01Web.DTO;

namespace MepWeb.Service.Interface
{
    /// <summary>[231009] Registrazione di tempo (timelog) letta da Teamwork. Le date sono in UTC.</summary>
    public class TwRegistrazione
    {
        public long Id { get; init; }
        public long? UserId { get; init; }
        public long? TaskId { get; init; }
        public long? ProjectId { get; init; }
        public int Minuti { get; init; }
        public DateTime TimeLoggedUtc { get; init; }
        public string? Descrizione { get; init; }
        public DateTime? UpdatedAtUtc { get; init; }
        public bool Cancellata { get; init; }
    }

    /// <summary>[231009] Task Teamwork con il valore del campo personalizzato Commessa/ISL.</summary>
    public class TwTask
    {
        public long Id { get; init; }
        public string? Nome { get; init; }
        public string? Commessa { get; init; }
    }

    /// <summary>[231009] Risultato della lettura dei task, con l'indicazione se Teamwork ha restituito i campi personalizzati.</summary>
    public class TwTasksRisultato
    {
        public Dictionary<long, TwTask> Tasks { get; init; } = new();
        /// <summary>False se la risposta non conteneva affatto i valori dei campi personalizzati (include non applicato).</summary>
        public bool CampiPersonalizzatiPresenti { get; init; }
    }

    /// <summary>
    /// [231009] Letture dalle API Teamwork v3 con il token dell'utente. Nessuna scrittura.
    /// Se Teamwork risponde 401 il collegamento dell'utente viene eliminato (errore con codice "401").
    /// </summary>
    public interface ITeamworkApiService
    {
        /// <summary>Registrazioni dell'utente collegato nel periodo (estremi inclusi).</summary>
        Task<ResponseBase<List<TwRegistrazione>>> GetRegistrazioniAsync(TeamworkConnessione conn, DateTime dal, DateTime al);

        /// <summary>Registrazioni per ID, comprese quelle cancellate (Cancellata = true). Gli ID non restituiti non esistono più o non sono visibili.</summary>
        Task<ResponseBase<List<TwRegistrazione>>> GetRegistrazioniPerIdAsync(TeamworkConnessione conn, IEnumerable<long> ids);

        /// <summary>Task per ID, con nome e campo Commessa/ISL.</summary>
        Task<ResponseBase<TwTasksRisultato>> GetTasksAsync(TeamworkConnessione conn, IEnumerable<long> ids);
    }
}
