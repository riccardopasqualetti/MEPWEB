﻿using Azure.Core;
using Humanizer.Localisation;
using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using MepWeb.Costants;
using MepWeb.DTO;
using MepWeb.DTO.Request;
using MepWeb.DTO.Response;
using MepWeb.Exeptions;
using MepWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace MepWeb.Service.Impl
{
    public class OreQualificaService : IOreQualificaService
    {
        private readonly SataconsultingContext _dbContext;
        private readonly UserScope _scope;

        public OreQualificaService(SataconsultingContext dbContext, UserScope scope)
        {
            _dbContext = dbContext;
            _scope = scope;
        }

        public async Task<ResponseBase<OreQualificaPagedResponse?>> GetAllRecordsByIdDocPagedAsync(decimal idDoc, BasePagedRequest pagedRequest)
        {
            var queryCount = _dbContext.PscCo01s.Where(x => x.IdDoc == idDoc).ToList();
            var count = queryCount.Count;

            var pageRequest = new BasePageCriteria(pagedRequest.Page, pagedRequest.Limit);

            var query = from c001 in _dbContext.PscCo01s
                        join zz12 in _dbContext.Mvxzz12s.DefaultIfEmpty() on new { v1 = c001.Grpcdl, v2 = "grpcdl" } equals new { v1 = zz12.Cod, v2 = zz12.Cprfc }
                        into c0zz
                        from zz12 in c0zz.DefaultIfEmpty()
                        where c001.IdDoc == idDoc
                        select new OreQualificaResponse
                        {
                            IdDocumento = idDoc,
							Qualifica = c001.Grpcdl,
							DescrizioneQualifica = zz12.DescrizioneRidotta,
							TipoFatturazione = c001.TFatt,
                            OreAcquistate = c001.HhAcq,
                            Note = c001.Note
                        };

			var pagedQuery = query
            .Skip(pageRequest.Offset)
            .Take(pageRequest.Limit);

            var res = new OreQualificaPagedResponse
            {
                TotalPages = (int)Math.Ceiling((decimal)count / pageRequest.Limit),
                TotalRecords = count,
                response = await pagedQuery.ToListAsync()
            };

            return ResponseBase<OreQualificaPagedResponse?>.Success(res);
        }

        public async Task<ResponseBase<List<OreQualificaResponse?>>> GetAllRecordsByIdDocAsync(decimal idDoc)
        {
            var query = await (from c001 in _dbContext.PscCo01s
                              join zz12 in _dbContext.Mvxzz12s.DefaultIfEmpty() on new { v1 = c001.Grpcdl, v2 = "grpcdl" } equals new { v1 = zz12.Cod, v2 = zz12.Cprfc }
                              into c0zz
                              from zz12 in c0zz.DefaultIfEmpty()
                              where c001.IdDoc == idDoc
                              select new OreQualificaResponse
                              {
                                  IdDocumento = idDoc,
                                  Qualifica = c001.Grpcdl,
                                  DescrizioneQualifica = zz12.DescrizioneRidotta,
                                  TipoFatturazione = c001.TFatt,
                                  OreAcquistate = c001.HhAcq,
                                  Note = c001.Note
                              }).ToListAsync();

            return ResponseBase<List<OreQualificaResponse?>>.Success(query);
        }

        public async Task<ResponseBase<OreQualificaResponse?>> GetSingleRecordAsync(decimal idDoc, decimal grpcdl)
        {
            string cDitta = CommonCostants.CDitta;
            var query = await (from c001 in _dbContext.PscCo01s
                              join zz12 in _dbContext.Mvxzz12s.DefaultIfEmpty() on new { v1 = c001.Grpcdl, v2 = "grpcdl" } equals new { v1 = zz12.Cod, v2 = zz12.Cprfc }
                              into c0zz
                              from zz12 in c0zz.DefaultIfEmpty()
                              where c001.IdDoc == idDoc && c001.Grpcdl == grpcdl && c001.CDitta == cDitta
                              select new OreQualificaResponse
                              {
                                  IdDocumento = idDoc,
								  Qualifica = c001.Grpcdl,
								  DescrizioneQualifica = zz12.DescrizioneRidotta,
								  TipoFatturazione = c001.TFatt,
                                  OreAcquistate = c001.HhAcq,
                                  Note = c001.Note
                              }).ToListAsync();
            if (query.Count == 0)
            {
                return ResponseBase<OreQualificaResponse?>.Failed(GenericException.RecordInesistente, "Il record che si sta cercando di aggiornare non è presente all'interno della tabella");
            }

            return ResponseBase<OreQualificaResponse?>.Success(query[0]);
        }

        public async Task<ResponseBase<OreQualificaResponse?>> CreateRecordAsync(OreQualificaCreateRequest createRequest)
        {
            string cDitta = CommonCostants.CDitta;

            if(createRequest.IdDocumento == 0) 
            {
                return ResponseBase<OreQualificaResponse?>.Failed(GenericException.CampoObbligatorio , "Id Documento non passato, questo campo è obbligatorio");
            }

			//if (string.IsNullOrEmpty(createRequest.DescrizioneQualifica))
			//{
			//	return ResponseBase<OreQualificaResponse?>.Failed(GenericException.CampoObbligatorio, "Qualifica non passata, questo campo è obbligatorio");
			//}

			//var mvxzz12 = await _dbContext.Mvxzz12s.FirstOrDefaultAsync(x => x.Cprfc == "grpcdl" && x.DescrizioneRidotta == createRequest.DescrizioneQualifica);
   //         if(mvxzz12 == null)
   //         {
   //             return ResponseBase<OreQualificaResponse?>.Failed(GenericException.RecordInesistente, "Questo codice qualifica non è stato trovato");
   //         }
   //         createRequest.Qualifica = mvxzz12.Cod;

			var checkExistence = await _dbContext.PscCo01s.FirstOrDefaultAsync(x => x.CDitta == cDitta && x.IdDoc == createRequest.IdDocumento && x.Grpcdl == createRequest.Qualifica);
            if (checkExistence != null)
            {
                return ResponseBase<OreQualificaResponse?>.Failed(GenericException.RecordGiaEsistente, "Il record che si sta cercando di creare è già presente");
            }

            PscCo01 c001 = new PscCo01();
            c001.CDitta = cDitta;
            c001.IdDoc = createRequest.IdDocumento;
            c001.Grpcdl = (decimal)createRequest.Qualifica;
            c001.HhAcq = createRequest.OreAcquistate ?? 0;
            c001.TFatt = createRequest.TipoFatturazione;
            c001.Note = createRequest.Note;
            c001.DtIns = DateTime.Now;
            c001.UtenteIns = _scope.SV_USR_SIGLA;
            c001.DtUm = DateTime.Now;
            c001.UtenteUm = _scope.SV_USR_SIGLA;

            try
            {
                _dbContext.PscCo01s.Add(c001);
                var res = await _dbContext.SaveChangesAsync();
            }catch (Exception ex)
            {
                return ResponseBase<OreQualificaResponse?>.Failed(GenericException.RecordNonCreato, "Non è stato possibile creare il record");
            }

            return ResponseBase<OreQualificaResponse?>.Success();

        }

        public async Task<ResponseBase<OreQualificaResponse?>> UpdateRecordAsync(OreQualificaUpdateRequest updateRequest)
        {
            string cDitta = CommonCostants.CDitta;
            if (updateRequest.IdDocumento == 0)
            {
                return ResponseBase<OreQualificaResponse?>.Failed(GenericException.CampoObbligatorio, "Id Documento non passato, questo campo è obbligatorio");
            }

   //         if (string.IsNullOrEmpty(updateRequest.DescrizioneQualifica))
   //         {
   //             return ResponseBase<OreQualificaResponse?>.Failed(GenericException.CampoObbligatorio, "Qualifica non passata, questo campo è obbligatorio");
   //         }

			//var mvxzz12 = await _dbContext.Mvxzz12s.FirstOrDefaultAsync(x => x.Cprfc == "grpcdl" && x.DescrizioneRidotta == updateRequest.DescrizioneQualifica);
			//updateRequest.Qualifica = mvxzz12.Cod;

			var c001 = await _dbContext.PscCo01s.FirstOrDefaultAsync(x => x.CDitta == cDitta && x.IdDoc == updateRequest.IdDocumento && x.Grpcdl == updateRequest.Qualifica);
            if (c001 == null)
            {
                return ResponseBase<OreQualificaResponse?>.Failed(GenericException.RecordGiaEsistente, "Il record che si sta cercando di aggiornare non è presente all'interno della tabella");
            }

            if(updateRequest.OreAcquistate != null && updateRequest.OreAcquistate >= 0)
            {
                c001.HhAcq = updateRequest.OreAcquistate;
            }

            if(!string.IsNullOrEmpty(updateRequest.TipoFatturazione))
            {
                c001.TFatt = updateRequest.TipoFatturazione;
            }

            if (!string.IsNullOrEmpty(updateRequest.Note))
            {
                c001.Note = updateRequest.Note;
            }

            c001.DtUm = DateTime.Now;
            c001.UtenteUm = _scope.SV_USR_SIGLA;

            try
            {
                var res = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ResponseBase<OreQualificaResponse?>.Failed(GenericException.RecordNonCreato, "Non è stato possibile aggiornare il record");
            }

            return ResponseBase<OreQualificaResponse?>.Success();
        }

        public async Task<ResponseBase<OreQualificaResponse?>> DeleteRecordAsync(decimal idDoc, decimal grpcdl)
        {
            string cDitta = CommonCostants.CDitta;
            if (idDoc == 0)
            {
                return ResponseBase<OreQualificaResponse?>.Failed(GenericException.CampoObbligatorio, "Id Documento non passato, questo campo è obbligatorio");
            }

            if (grpcdl == null)
            {
                return ResponseBase<OreQualificaResponse?>.Failed(GenericException.CampoObbligatorio, "Qualifica non passata, questo campo è obbligatorio");
            }

            var c001 = await _dbContext.PscCo01s.FirstOrDefaultAsync(x => x.CDitta == cDitta && x.IdDoc == idDoc && x.Grpcdl == grpcdl);
            if (c001 == null)
            {
                return ResponseBase<OreQualificaResponse?>.Failed(GenericException.RecordGiaEsistente, "Il record che si sta cercando di eliminare non è presente all'interno della tabella");
            }

            if(c001.HhAcq > 0) 
            {
                return ResponseBase<OreQualificaResponse?>.Failed(GenericException.RecordNonEliminato, "Non è stato possibile eliminare il record in quanto le ore acquistate sono superiori a '0'");
            }

            try
            {
                _dbContext.PscCo01s.Remove(c001);
                var res = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ResponseBase<OreQualificaResponse?>.Failed(GenericException.RecordNonEliminato, "Si sono verificati degli errori durante l'eliminazione del record desiderato");
            }

            return ResponseBase<OreQualificaResponse?>.Success();
        }
    }
}
