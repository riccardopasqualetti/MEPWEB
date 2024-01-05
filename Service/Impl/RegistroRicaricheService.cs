using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Mep01Web.Service.Interface;
using MepWeb.Costants;
using MepWeb.DTO;
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
        private readonly IOreQualificaService _oreQualificaService;
        private readonly ICrrgService _crrgService;

        public RegistroRicaricheService(SataconsultingContext dbContext, UserScope scope, IOreQualificaService oreQualificaService, ICrrgService crrgService)
        {
            _dbContext = dbContext;
            _scope = scope;
            _oreQualificaService = oreQualificaService;
            _crrgService = crrgService;
        }

		public async Task<ResponseBase<RegistroRicarichePagedResponse?>> GetAllRecordsByIdDocPagedAsync(decimal idDoc, decimal qualifica, BasePagedRequest pagedRequest)
		{
			var count = await _dbContext.PscCo03s.Where(x => x.IdDoc == idDoc && x.Grpcdl == qualifica).CountAsync();

			var pageRequest = new BasePageCriteria(pagedRequest.Page, pagedRequest.Limit);

			var query = from c003 in _dbContext.PscCo03s
						join zz12 in _dbContext.Mvxzz12s.DefaultIfEmpty() on new { v1 = c003.Grpcdl, v2 = "grpcdl" } equals new { v1 = zz12.Cod, v2 = zz12.Cprfc }
						into c0zz
						from zz12 in c0zz.DefaultIfEmpty()
						where c003.IdDoc == idDoc
                           && c003.Grpcdl == qualifica
						select new RegistroRicaricheResponse
						{
                            IdDocumento = c003.IdDoc,
                            Id = c003.Id,
							Qualifica = c003.Grpcdl,
							DescrizioneQualifica = zz12.DescrizioneRidotta,
							RiferimentoOfferta = c003.RifOfferta,
							DataRicarica = c003.DtRicarica,
							OreAcquistate = c003.HhAcq,
							Note = c003.Note
						};

			var pagedQuery = query
			.Skip(pageRequest.Offset)
			.Take(pageRequest.Limit);

			var res = new RegistroRicarichePagedResponse
			{
				TotalPages = (int)Math.Ceiling((decimal)count / pageRequest.Limit),
				TotalRecords = count,
				response = await pagedQuery.ToListAsync()
			};

			return ResponseBase<RegistroRicarichePagedResponse?>.Success(res);
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
							IdDocumento = idDoc,
							Id = c003.Id,
							Qualifica = c003.Grpcdl,
							DescrizioneQualifica = zz12.DescrizioneRidotta,
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
							IdDocumento = c003.IdDoc,
							Id = c003.Id,
							Qualifica = c003.Grpcdl,
							DescrizioneQualifica = zz12.DescrizioneRidotta,
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

			var c001 = await _oreQualificaService.GetSingleRecordAsync(createRequest.IdDocumento, (decimal)createRequest.Qualifica);
            if (!c001.Succeeded)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordInesistente, "Il record appartenente alla tabella PSC_C001 da cui deriva il record che si vuole creare non esiste");
            }

            //var checkC003Existence = await _dbContext.PscCo03s.FirstOrDefaultAsync(x => x.CDitta == cDitta && x.IdDoc == createRequest.IdDocumento && x.Grpcdl == createRequest.Qualifica);
            //if (checkC003Existence != null)
            //{
            //    return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordGiaEsistente, "Il record che si sta cercando di creare è già presente");
            //}

            PscCo03 c003 = new PscCo03();
            c003.CDitta = cDitta;
            c003.IdDoc = createRequest.IdDocumento;
            c003.Grpcdl = (decimal)createRequest.Qualifica;
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

            OreQualificaUpdateRequest c001Update = new OreQualificaUpdateRequest();
            c001Update.IdDocumento = createRequest.IdDocumento;
            c001Update.Qualifica = createRequest.Qualifica;
            c001Update.OreAcquistate = c001.Body.OreAcquistate + createRequest.OreAcquistate;

            var update = await _oreQualificaService.UpdateRecordAsync(c001Update);
            if (!update.Succeeded)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordNonAggiornato, "Non è stato possibile aggiornare il record nulla tabella di testata PSC_C001");
            }


            return ResponseBase<RegistroRicaricheResponse?>.Success();

        }

        public async Task<ResponseBase<RegistroRicaricheResponse?>> UpdateRecordAsync(RegistroRicaricheUpdateRequest updateRequest)
        {
            string cDitta = CommonCostants.CDitta;

            if (updateRequest.Id == 0)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.CampoObbligatorio, "Id non passato, questo campo è obbligatorio");
            }

            if (updateRequest.IdDocumento == 0)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.CampoObbligatorio, "Id Documento non passato, questo campo è obbligatorio");
            }

			var c003 = await _dbContext.PscCo03s.FirstOrDefaultAsync(x => x.Id == updateRequest.Id);
            if (c003 == null)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordInesistente, "Il record che si sta cercando di aggiornare non è presente all'interno della tabella");
            }

            OreQualificaUpdateRequest c001Update1 = new OreQualificaUpdateRequest();
            OreQualificaUpdateRequest c001Update2 = new OreQualificaUpdateRequest();

            decimal? diff = 0;
            decimal availableHours = 0;

            c001Update1.IdDocumento = updateRequest.IdDocumento;

			var c001 = await _oreQualificaService.GetSingleRecordAsync(updateRequest.IdDocumento, c003.Grpcdl);
			if (!c001.Succeeded)
			{
				return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordInesistente, "Il record appartenente alla tabella PSC_C001 da cui deriva il record che si vuole aggiornare non esiste");
			}

			//Se modifico le ore ma non la qualifica devo solo verficare che nel caso le ore siano diminuite, il totale in testata non diventi negativi
			//in caso sia maggiore del valore precedente sommo solo la differenza al totale.
			if (updateRequest.OreAcquistate != 0 && updateRequest.Qualifica == c003.Grpcdl)
			{
				c001Update1.Qualifica = updateRequest.Qualifica;
				if (updateRequest.OreAcquistate < c003.HhAcq)
				{
					diff = c003.HhAcq - updateRequest.OreAcquistate;
					availableHours = await _crrgService.CheckHoursAvailability(c003.IdDoc, (decimal)diff, c001.Body.DescrizioneQualifica);
					if (availableHours < 0)
					{
						return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordNonEliminato, "Non è possibile eliminare il secord desiderato, in quanto le ore acquistate andrebbero in negativo");
					}
					c001Update1.OreAcquistate = c001.Body.OreAcquistate - diff;
				}
				else
				{
					diff = updateRequest.OreAcquistate - c003.HhAcq;
					c001Update1.OreAcquistate = c001.Body.OreAcquistate + diff;
				}
				c003.HhAcq = updateRequest.OreAcquistate;

			}

			if (updateRequest.Qualifica != c003.Grpcdl)
            {

				c001Update2.IdDocumento = updateRequest.IdDocumento;
				var c001N1 = await _oreQualificaService.GetSingleRecordAsync(updateRequest.IdDocumento, (decimal)updateRequest.Qualifica);
                if (!c001N1.Succeeded)
                {
                    return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordInesistente, "Il record appartenente alla tabella PSC_C001 da cui deriva il record che si vuole aggiornare non esiste");
                }

                var c001N2 = await _oreQualificaService.GetSingleRecordAsync(updateRequest.IdDocumento, c003.Grpcdl);
                if (!c001N2.Succeeded)
                {
                    return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordInesistente, "Il record appartenente alla tabella PSC_C001 da cui deriva il record che si vuole aggiornare non esiste");
                }

                availableHours = await _crrgService.CheckHoursAvailability(c003.IdDoc, (decimal)c003.HhAcq, c001N2.Body.DescrizioneQualifica);
                if (availableHours < 0)
                {
                    return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordNonEliminato, "Non è possibile eliminare il secord desiderato, in quanto le ore acquistate andrebbero in negativo");
                }
                //Se modifico solo la qualifica, tolgo le ore dalla qualifica precedente (Controllando che il totale in testata non sia negativo) e le aggiungo a quella nuova.
                if (updateRequest.OreAcquistate == 0)
                {
                    c001Update1.OreAcquistate = c001N1.Body.OreAcquistate + c003.HhAcq;
                    c001Update2.OreAcquistate = c001N2.Body.OreAcquistate - c003.HhAcq;
                }
                //Se modifico entrambe, controllo sempre che le ore totali della qualifica dalla quale tolgo le ore non siano negative
                else
                {
                    c001Update1.OreAcquistate = c001N1.Body.OreAcquistate + updateRequest.OreAcquistate;
                    c001Update2.OreAcquistate = c001N2.Body.OreAcquistate - c003.HhAcq;
                }
                c001Update1.Qualifica = updateRequest.Qualifica;
                c001Update2.Qualifica = c003.Grpcdl;
                c003.Grpcdl = (decimal)updateRequest.Qualifica;
            }

            if (!string.IsNullOrEmpty(updateRequest.RiferimentoOfferta))
            {
                c003.RifOfferta = updateRequest.RiferimentoOfferta;
            }

            if(updateRequest.DataRicarica != null)
            {
                c003.DtRicarica = updateRequest.DataRicarica;
            }

            if (!string.IsNullOrEmpty(updateRequest.Note))
            {
                c003.Note = updateRequest.Note;
            }

            c003.DtUm = DateTime.Now;
            c003.UtenteUm = _scope.SV_USR_SIGLA;


            try
            {
                
				var update = await _oreQualificaService.UpdateRecordAsync(c001Update1);
				if (!update.Succeeded)
				{
					return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordNonAggiornato, "Non è stato possibile aggiornare il record nella tabella di testata PSC_C001");
				}
                if (c001Update2.IdDocumento != 0)
                {
					update = await _oreQualificaService.UpdateRecordAsync(c001Update2);
					if (!update.Succeeded)
					{
						return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordNonAggiornato, "Non è stato possibile aggiornare il record nella tabella di testata PSC_C001");
					}
				}
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

            var c001 = await _oreQualificaService.GetSingleRecordAsync(c003.IdDoc, c003.Grpcdl);
            if (!c001.Succeeded)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordInesistente, "Il record appartenente alla tabella PSC_C001 da cui deriva il record che si vuole eliminare non esiste");
            }

            OreQualificaUpdateRequest c001Update = new OreQualificaUpdateRequest();
            c001Update.IdDocumento = c003.IdDoc;
            c001Update.Qualifica = c003.Grpcdl;
            c001Update.OreAcquistate = c001.Body.OreAcquistate - c003.HhAcq;
            var availableHours = await _crrgService.CheckHoursAvailability(c003.IdDoc, (decimal)c003.HhAcq, c001.Body.DescrizioneQualifica);
            if (availableHours < 0)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordNonEliminato, "Non è possibile eliminare il secord desiderato, in quanto le ore acquistate andrebbero in negativo");
            }
            try
            {
                _dbContext.PscCo03s.Remove(c003);
                var res = await _dbContext.SaveChangesAsync();
                var update = await _oreQualificaService.UpdateRecordAsync(c001Update);
                if (!update.Succeeded)
                {
                    return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordNonAggiornato, "Non è stato possibile aggiornare il record nella tabella di testata PSC_C001");
                }
            }
            catch (Exception ex)
            {
                return ResponseBase<RegistroRicaricheResponse?>.Failed(GenericException.RecordNonEliminato, "Non è stato possibile eliminare il record su PSC_C003");
            }

            return ResponseBase<RegistroRicaricheResponse?>.Success();
        }
    }
}
