using Mep01Web.DTO;
using MepWeb.DTO.Response;

namespace MepWeb.Service.Interface
{
    /// <summary>
    /// [231009] Import delle registrazioni Teamwork come consuntivi MEP.
    /// Per ora solo l'anteprima in lettura: registrazioni del periodo con il loro stato rispetto a flusso_crrg_tw.
    /// </summary>
    public interface ITeamworkImportService
    {
        /// <summary>
        /// Registrazioni Teamwork dell'utente collegato nel periodo (date locali, estremi inclusi), divise in nuove,
        /// importate e da allineare. Le registrazioni già collegate del periodo si ricontrollano per ID, così si
        /// trovano anche quelle spostate su altre date o cancellate.
        /// </summary>
        Task<ResponseBase<TwAnteprimaResponse>> GetAnteprimaAsync(DateTime dal, DateTime al);
    }
}
