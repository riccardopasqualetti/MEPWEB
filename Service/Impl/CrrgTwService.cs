using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using MepWeb.Costants;
using MepWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MepWeb.Service.Impl
{
    public class CrrgTwService : ICrrgTwService
    {
        private readonly SataconsultingContext _db;

        public CrrgTwService(SataconsultingContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<FlussoCrrgTw?> GetByTimelogIdAsync(long twTimelogId)
        {
            return await _db.FlussoCrrgTws.AsNoTracking()
                .SingleOrDefaultAsync(x => x.TwTimelogId == twTimelogId);
        }

        public async Task<List<FlussoCrrgTw>> GetByTimelogIdsAsync(IEnumerable<long> twTimelogIds)
        {
            var ids = twTimelogIds.Distinct().ToList();
            if (ids.Count == 0)
            {
                return new List<FlussoCrrgTw>();
            }

            return await _db.FlussoCrrgTws.AsNoTracking()
                .Where(x => ids.Contains(x.TwTimelogId))
                .ToListAsync();
        }

        public async Task<FlussoCrrgTw?> GetByCrrgCSrlAsync(decimal crrgCSrl)
        {
            return await _db.FlussoCrrgTws.AsNoTracking()
                .SingleOrDefaultAsync(x => x.CrrgCSrl == crrgCSrl);
        }

        public async Task<bool> IsCollegatoAsync(decimal crrgCSrl)
        {
            return await _db.FlussoCrrgTws.AnyAsync(x => x.CrrgCSrl == crrgCSrl);
        }

        public async Task<List<FlussoCrrgTw>> GetByPeriodoAsync(string sigla, DateTime dalUtc, DateTime alUtc)
        {
            return await _db.FlussoCrrgTws.AsNoTracking()
                .Where(x => x.Sigla == sigla && x.TwDate >= dalUtc && x.TwDate <= alUtc)
                .OrderBy(x => x.TwDate)
                .ToListAsync();
        }

        public async Task<List<FlussoCrrgTw>> GetSenzaConsuntivoAsync(string sigla)
        {
            string cDitta = CommonCostants.CDitta;
            return await _db.FlussoCrrgTws.AsNoTracking()
                .Where(x => x.Sigla == sigla
                    && !_db.FlussoCrrgs.Any(c => c.CrrgCSrl == x.CrrgCSrl && c.CrrgCDitta == cDitta))
                .OrderBy(x => x.TwDate)
                .ToListAsync();
        }

        public async Task<FlussoCrrg?> GetUltimoConsuntivoPerTaskAsync(long twTaskId)
        {
            string cDitta = CommonCostants.CDitta;
            var query = from tw in _db.FlussoCrrgTws
                        join crrg in _db.FlussoCrrgs on tw.CrrgCSrl equals crrg.CrrgCSrl
                        where tw.TwTaskId == twTaskId && crrg.CrrgCDitta == cDitta
                        orderby crrg.CrrgDtIns descending, crrg.CrrgCSrl descending
                        select crrg;

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<ResponseBase> InsertAsync(FlussoCrrgTw collegamento)
        {
            if (await _db.FlussoCrrgTws.AnyAsync(x => x.TwTimelogId == collegamento.TwTimelogId))
            {
                return ResponseBase.Failed(CrrgTwErrors.RegistrazioneGiaCollegata,
                    $"La registrazione Teamwork {collegamento.TwTimelogId} è già collegata a un consuntivo");
            }
            if (await _db.FlussoCrrgTws.AnyAsync(x => x.CrrgCSrl == collegamento.CrrgCSrl))
            {
                return ResponseBase.Failed(CrrgTwErrors.ConsuntivoGiaCollegato,
                    $"Il consuntivo {collegamento.CrrgCSrl} è già collegato a una registrazione Teamwork");
            }

            _db.FlussoCrrgTws.Add(collegamento);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Due import contemporanei della stessa registrazione: vince il primo, la chiave univoca blocca il secondo.
                _db.Entry(collegamento).State = EntityState.Detached;
                return ResponseBase.Failed(CrrgTwErrors.RegistrazioneGiaCollegata,
                    $"La registrazione Teamwork {collegamento.TwTimelogId} è già stata importata");
            }

            return new ResponseBase(true);
        }

        public async Task<ResponseBase> AggiornaFotografiaAsync(FlussoCrrgTw collegamento)
        {
            var record = await _db.FlussoCrrgTws.SingleOrDefaultAsync(x => x.TwTimelogId == collegamento.TwTimelogId);
            if (record == null)
            {
                return ResponseBase.Failed(CrrgTwErrors.CollegamentoNonTrovato,
                    $"Collegamento non trovato per la registrazione Teamwork {collegamento.TwTimelogId}");
            }

            record.TwTaskId = collegamento.TwTaskId;
            record.TwMinutes = collegamento.TwMinutes;
            record.TwDate = collegamento.TwDate;
            record.TwCommessa = collegamento.TwCommessa;
            record.TwDescrizioneReg = collegamento.TwDescrizioneReg;
            record.TwUpdatedAt = collegamento.TwUpdatedAt;

            await _db.SaveChangesAsync();
            return new ResponseBase(true);
        }

        public async Task<ResponseBase> DeleteAsync(long twTimelogId)
        {
            var record = await _db.FlussoCrrgTws.SingleOrDefaultAsync(x => x.TwTimelogId == twTimelogId);
            if (record == null)
            {
                return ResponseBase.Failed(CrrgTwErrors.CollegamentoNonTrovato,
                    $"Collegamento non trovato per la registrazione Teamwork {twTimelogId}");
            }

            _db.FlussoCrrgTws.Remove(record);
            await _db.SaveChangesAsync();
            return new ResponseBase(true);
        }
    }
}
