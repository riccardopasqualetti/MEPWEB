using Mep01Web.DTO;
using Mep01Web.Models;

namespace MepWeb.Service.Interface
{
    /// <summary>
    /// [231009] Accesso alla tabella flusso_crrg_tw, che collega le registrazioni di tempo
    /// Teamwork ai consuntivi MEP (flusso_crrg). Non chiama Teamwork.
    /// </summary>
    public interface ICrrgTwService
    {
        Task<FlussoCrrgTw?> GetByTimelogIdAsync(long twTimelogId);

        /// <summary>Collegamenti esistenti per un insieme di registrazioni lette da Teamwork.</summary>
        Task<List<FlussoCrrgTw>> GetByTimelogIdsAsync(IEnumerable<long> twTimelogIds);

        Task<FlussoCrrgTw?> GetByCrrgCSrlAsync(decimal crrgCSrl);

        /// <summary>Task Teamwork dei consuntivi collegati, per consuntivo (i consuntivi non collegati non compaiono).</summary>
        Task<Dictionary<decimal, long>> GetTaskPerConsuntiviAsync(IEnumerable<decimal> crrgCSrl);

        /// <summary>True se il consuntivo è collegato a una registrazione Teamwork (e quindi non modificabile da MEPWEB).</summary>
        Task<bool> IsCollegatoAsync(decimal crrgCSrl);

        /// <summary>
        /// Collegamenti importati da <paramref name="sigla"/> con data registrazione (UTC) nel periodo,
        /// estremi inclusi. Servono per il controllo per ID delle registrazioni già collegate.
        /// </summary>
        Task<List<FlussoCrrgTw>> GetByPeriodoAsync(string sigla, DateTime dalUtc, DateTime alUtc);

        /// <summary>Collegamenti il cui consuntivo non esiste più in flusso_crrg (stato "Consuntivo eliminato in MEP").</summary>
        Task<List<FlussoCrrgTw>> GetSenzaConsuntivoAsync(string sigla);

        /// <summary>Ultimo consuntivo importato da un task Teamwork: per riproporre operazione, causale e macroattività.</summary>
        Task<FlussoCrrg?> GetUltimoConsuntivoPerTaskAsync(long twTaskId);

        /// <summary>
        /// Inserisce il collegamento. Fallisce se la registrazione o il consuntivo sono già collegati.
        /// Da chiamare nella stessa transazione in cui si crea il consuntivo.
        /// </summary>
        Task<ResponseBase> InsertAsync(FlussoCrrgTw collegamento);

        /// <summary>Aggiorna la fotografia dei valori Teamwork dopo un allineamento.</summary>
        Task<ResponseBase> AggiornaFotografiaAsync(FlussoCrrgTw collegamento);

        /// <summary>Elimina solo il collegamento: il consuntivo resta e torna modificabile.</summary>
        Task<ResponseBase> DeleteAsync(long twTimelogId);
    }
}
