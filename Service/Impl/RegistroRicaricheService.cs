using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using MepWeb.Costants;
using MepWeb.DTO.Request;
using MepWeb.DTO.Response;
using MepWeb.Exeptions;
using MepWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MepWeb.Service.Impl
{
    public class RegistroRicaricheService : IRegistroRicaricheService
    {
        private readonly SataconsultingContext _dbContext;
        private readonly UserScope _scope;

        public RegistroRicaricheService(SataconsultingContext dbContext, UserScope scope)
        {
            _dbContext = dbContext;
            _scope = scope;
        }

        public async Task<ResponseBase<List<RegistroRicaricheResponse?>>> GetAllRecordsByIdDocAsync(decimal idDoc)
        {
            var query = await (from c003 in _dbContext.PscCo03s
                        join zz12 in _dbContext.Mvxzz12s.DefaultIfEmpty() on new { v1 = c003.Grpcdl, v2 = "grpcdl" } equals new { v1 = zz12.Cod, v2 = zz12.Cprfc }
                        into c0zz
                        from zz12 in c0zz.DefaultIfEmpty()
                        where c003.IdDoc == idDoc
                        select new RegistroRicaricheResponse
                        {
                            Qualifica = zz12.DescrizioneRidotta,
                            RiferimentoOfferta = c003.RifOfferta,
                            DataRicarica = c003.DtRicarica,
                            OreAcquistate = c003.HhAcq,
                            Note = c003.Note
                        }).ToListAsync();

            return ResponseBase<List<RegistroRicaricheResponse?>>.Success(query);
        }

        public async Task<ResponseBase<RegistroRicaricheResponse?>> GetSingleRecordAsync(decimal id)
        {
            var query = await (from c003 in _dbContext.PscCo03s
                        join zz12 in _dbContext.Mvxzz12s.DefaultIfEmpty() on new { v1 = c003.Grpcdl, v2 = "grpcdl" } equals new { v1 = zz12.Cod, v2 = zz12.Cprfc }
                        into c0zz
                        from zz12 in c0zz.DefaultIfEmpty()
                        where c003.Id == id
                        select new RegistroRicaricheResponse
                        {
                            Qualifica = zz12.DescrizioneRidotta,
                            RiferimentoOfferta = c003.RifOfferta,
                            DataRicarica = c003.DtRicarica,
                            OreAcquistate = c003.HhAcq,
                            Note = c003.Note
                        }).ToListAsync();
            if(query.Count == 0)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordInesistente, "Il record che si sta cercando di aggiornare non è presente all'interno della tabella");
            }

            return ResponseBase<RegistroRicaricheResponse?>.Success(query[0]);
        }

        public async Task<ResponseBase<RegistroRicaricheResponse?>> CreateRecordAsync(RegistroRicaricheCreateRequest createRequest)
        {
            string cDitta = CommonCostants.CDitta;

            if (createRequest.IdDocumento == 0)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.CampoObbligatorio, "Id Documento non passato, questo campo è obbligatorio");
            }

            if (createRequest.Qualifica == 0)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.CampoObbligatorio, "Qualifica non passata, questo campo è obbligatorio");
            }

            var checkExistence = await _dbContext.PscCo03s.FirstOrDefaultAsync(x => x.CDitta == cDitta && x.IdDoc == createRequest.IdDocumento && x.Grpcdl == createRequest.Qualifica);
            if (checkExistence != null)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordGiaEsistente, "Il record che si sta cercando di creare è già presente");
            }

            PscCo03 c003 = new PscCo03();
            c003.CDitta = cDitta;
            c003.IdDoc = createRequest.IdDocumento;
            c003.Grpcdl = createRequest.Qualifica;
            c003.RifOfferta = createRequest.RiferimentoOfferta;
            c003.DtRicarica = createRequest.DataRicarica;
            c003.HhAcq = createRequest.OreAcquistate;
            c003.DtIns = DateTime.Now;
            c003.UtenteIns = _scope.SV_USR_SIGLA;
            c003.DtUm = DateTime.Now;
            c003.UtenteUm = _scope.SV_USR_SIGLA;

            try
            {
                _dbContext.PscCo03s.Add(c003);
                var res = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordNonCreato, "Non è stato possibile creare il record");
            }

            return ResponseBase<RegistroRicaricheResponse?>.Success();

        }

        public async Task<ResponseBase<RegistroRicaricheResponse?>> UpdateRecordAsync(RegistroRicaricheUpdateRequest updateRequest)
        {
            string cDitta = CommonCostants.CDitta;
            if (updateRequest.IdDocumento == 0)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.CampoObbligatorio, "Id Documento non passato, questo campo è obbligatorio");
            }

            if (updateRequest.Qualifica == 0)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.CampoObbligatorio, "Qualifica non passata, questo campo è obbligatorio");
            }

            var c003 = await _dbContext.PscCo03s.FirstOrDefaultAsync(x => x.CDitta == cDitta && x.IdDoc == updateRequest.IdDocumento && x.Grpcdl == updateRequest.Qualifica);
            if (c003 == null)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordInesistente, "Il record che si sta cercando di aggiornare non è presente all'interno della tabella");
            }

            if(!string.IsNullOrEmpty(updateRequest.RiferimentoOfferta))
            {
                c003.RifOfferta = updateRequest.RiferimentoOfferta;
            }

            if(updateRequest.DataRicarica != null)
            {
                c003.DtRicarica = updateRequest.DataRicarica;
            }

            if (updateRequest.OreAcquistate != 0)
            {
                c003.HhAcq = updateRequest.OreAcquistate;
            }

            if (!string.IsNullOrEmpty(updateRequest.Note))
            {
                c003.Note = updateRequest.Note;
            }

            c003.DtUm = DateTime.Now;
            c003.UtenteUm = _scope.SV_USR_SIGLA;

            try
            {
                var res = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordNonAggiornato, "Non è stato possibile aggiornare il record");
            }

            return ResponseBase<RegistroRicaricheResponse?>.Success();
        }

        public async Task<ResponseBase<RegistroRicaricheResponse?>> DeleteRecordAsync(decimal id)
        {
            if (id == 0)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.CampoObbligatorio, "Id Documento non passato, questo campo è obbligatorio");
            }

            var c003 = await _dbContext.PscCo03s.FirstOrDefaultAsync(x => x.Id == id);
            if (c003 == null)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordGiaEsistente, "Il record che si sta cercando di eliminare non è presente all'interno della tabella");
            }

            try
            {
                _dbContext.PscCo03s.Remove(c003);
                var res = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordNonCreato, "Non è stato possibile eliminare il record su PSC_C003");
            }

            //var c001 = await _dbContext.PscCo01s.FirstOrDefaultAsync(x => x.CDitta == cDitta && x.IdDoc == idDoc && x.Grpcdl == grpcdl);

            //try
            //{
            //    _dbContext.PscCo01s.Remove(c001);
            //    var res = await _dbContext.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{
            //    return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordNonEliminato, "Non è stato possibile eliminare il record su PSC_C001");
            //}

            return ResponseBase<RegistroRicaricheResponse?>.Success();
        }
    }
}
