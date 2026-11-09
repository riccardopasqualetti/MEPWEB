using Mep01Web.DTO;
using MepWeb.DTO.Request;
using MepWeb.DTO.Response;

namespace MepWeb.Service.Interface
{
    /// <summary>[231009] Import delle registrazioni Teamwork come consuntivi MEP.</summary>
    public interface ITeamworkImportService
    {
        /// <summary>
        /// Registrazioni Teamwork dell'utente collegato nel periodo (date locali, estremi inclusi), divise in nuove,
        /// importate e da allineare, con i valori proposti per l'import. Le registrazioni già collegate del periodo si
        /// ricontrollano per ID, così si trovano anche quelle spostate su altre date o cancellate.
        /// </summary>
        Task<ResponseBase<TwAnteprimaResponse>> GetAnteprimaAsync(DateTime dal, DateTime al);

        /// <summary>
        /// Crea un consuntivo per ogni registrazione selezionata, con la validazione standard (CrrgValidator), e salva il
        /// collegamento in flusso_crrg_tw nella stessa transazione. Ore, data, task e commessa si rileggono da Teamwork.
        /// Ogni registrazione è indipendente: un errore su una non blocca le altre.
        /// </summary>
        Task<ResponseBase<TwImportEsito>> ImportaAsync(TwImportRequest request);

        /// <summary>
        /// Azione sulla griglia "Da allineare" per una registrazione collegata. I dati si rileggono da Teamwork e lo stato
        /// si ricontrolla: Aggiorna solo se la registrazione esiste, Elimina solo se è stata cancellata su Teamwork,
        /// Scollega se è stata cancellata o se il consuntivo non c'è più. Ritorna il messaggio per l'utente.
        /// </summary>
        Task<ResponseBase<string>> AllineaAsync(long timelogId, TwAzioneAllineamento azione);
    }
}
