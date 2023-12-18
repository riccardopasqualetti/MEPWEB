using Mep01Web.DTO;
using Mep01Web.DTO.Response;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Mep01Web.Models.Views;
using MepWeb.Costants;
using MepWeb.DTO;
using MepWeb.DTO.Request;
using MepWeb.DTO.Response;
using MepWeb.Exeptions;
using MepWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MepWeb.Service.Impl
{
    public class PscCo02Service : IPscCo02Service
    {
        private readonly SataconsultingContext _db;

        public PscCo02Service(SataconsultingContext dbContext)
        {
            _db = dbContext;
        }

		public async Task<ResponseBase<PscCo02PagedResponse?>> GetAllFromPscCo02PagedAsync(decimal idDoc, BasePagedRequest pagedRequest)
		{
			var count = await _db.PscCo02s.Where(x => x.IdDoc == idDoc).CountAsync();

			var pageRequest = new BasePageCriteria(pagedRequest.Page, pagedRequest.Limit);

			string cDitta = CommonCostants.CDitta;
			var query = from c002 in _db.PscCo02s
						join zz12 in _db.Mvxzz12s.DefaultIfEmpty() on new { v1 = c002.Grpcdl, v2 = "grpcdl" } equals new { v1 = zz12.Cod, v2 = zz12.Cprfc }
						into c0zz
						from zz12 in c0zz.DefaultIfEmpty()
						join macc in _db.FlussoMaccs.DefaultIfEmpty() on c002.CRisorsa equals macc.MaccCMatricola
						into c0macc02
						from macc in c0macc02.DefaultIfEmpty()
						where c002.IdDoc == idDoc && c002.CDitta == cDitta
						select new PscCo02Response
						{
							IdDoc = idDoc,
							CRisorsa = macc.MaccDesc,
							Grpcdl = zz12.DescrizioneRidotta,

						};
			var pagedQuery = query
			.Skip(pagedRequest.Page)
			.Take(pagedRequest.Limit);

			var res = new PscCo02PagedResponse
			{
				TotalPages = (int)Math.Ceiling((decimal)count / pageRequest.Limit),
				TotalRecords = count,
				response = await pagedQuery.ToListAsync()
			};
			return ResponseBase<PscCo02PagedResponse?>.Success(res);

		}

		public async  Task<ResponseBase<List<PscCo02Response?>>> GetAllFromPscCo02Async(decimal idDoc)
        {
            string cDitta = CommonCostants.CDitta;
            var query = await (from c002 in _db.PscCo02s
                               join zz12 in _db.Mvxzz12s.DefaultIfEmpty() on new { v1 = c002.Grpcdl, v2 = "grpcdl" } equals new { v1 = zz12.Cod, v2 = zz12.Cprfc }
                               into c0zz
                               from zz12 in c0zz.DefaultIfEmpty()
                               join macc in _db.FlussoMaccs.DefaultIfEmpty() on c002.CRisorsa equals macc.MaccCMatricola
                              into c0macc02
                               from macc in c0macc02.DefaultIfEmpty()
                               where c002.IdDoc == idDoc && c002.CDitta==cDitta
                               select new PscCo02Response
                               {
                                   CRisorsa = macc.MaccDesc,
                                   Grpcdl = zz12.DescrizioneRidotta,
                                   
                               }).ToListAsync();

            return ResponseBase<List<PscCo02Response?>>.Success(query);
            
        }

        public async  Task<ResponseBase<PscCo02Response?>> GetSingleRecordAsync(string cRisorsa, decimal idDoc)
        {
            string cDitta = CommonCostants.CDitta;
            var query = await(from c002 in _db.PscCo02s
                              join zz12 in _db.Mvxzz12s.DefaultIfEmpty() on new { v1 = c002.Grpcdl, v2 = "grpcdl" } equals new { v1 = zz12.Cod, v2 = zz12.Cprfc }
                              into c0zz
                              from zz12 in c0zz.DefaultIfEmpty()
                              join macc in _db.FlussoMaccs.DefaultIfEmpty() on c002.CRisorsa equals macc.MaccCMatricola
                             into c0macc02
                              from macc in c0macc02.DefaultIfEmpty()
                              where c002.IdDoc == idDoc && c002.CDitta == cDitta && c002.CRisorsa == cRisorsa
                              select new PscCo02Response
                              {
                                  CRisorsa = macc.MaccDesc,
                                  Grpcdl = zz12.DescrizioneRidotta,

                              }).ToListAsync();
            if (query.Count == 0)
            {
                return ResponseBase<PscCo02Response?>.Failed(GenericException.RecordInesistente, "Il record  non è presente all'interno della tabella");
            }

            return ResponseBase<PscCo02Response?>.Success(query[0]);
            
        }

		public async Task<ResponseBase<PscCo02?>> CreateRecordAsync(PscCo02CreateRequest request)
		{
			var pscCo02 = new PscCo02
			{
				CDitta = "01",
				IdDoc = request.IdDoc,
				CRisorsa = request.CRisorsa,
				Grpcdl = request.Grpcdl,
				UtenteIns = request.UtenteIns,
				DtIns = DateTime.Now,
				UtenteUm = request.UtenteUm,
				DtUm = DateTime.Now,

			};
			_db.PscCo02s.Add(pscCo02);
			var affected = await _db.SaveChangesAsync();
			if (affected < 1)
			{
				return ResponseBase<PscCo02?>.Failed(PscCo02Errors.Inserimentofallito, "Inserimento fallito");
			}
			return ResponseBase<PscCo02?>.Success();
		}

		public async Task<ResponseBase<PscCo02?>> UpdateRecordAsync(PscCo02UpdateRequest request)
		{
			string cDitta = CommonCostants.CDitta;
			PscCo02 pscCo02 = await _db.PscCo02s.SingleOrDefaultAsync(e => e.CRisorsa == request.CRisorsa && e.CDitta == cDitta && e.IdDoc == request.IdDoc);
			if (pscCo02 == null)
			{
				return ResponseBase<PscCo02?>.Failed(PscCo02Errors.RecordNonTrovati, "Non sono stati trovati record in PscCo02");
			}
			if (request.Grpcdl == 0)
			{
				return ResponseBase<PscCo02?>.Failed(PscCo02Errors.GrpCdlDeveEssereValorizzato, "GrpCdl deve essere valorizzato");
			}
			pscCo02.Grpcdl = request.Grpcdl;
			var result = await _db.SaveChangesAsync();
			if (result < 1)
			{
				return ResponseBase<PscCo02?>.Failed(PscCo02Errors.Aggiornamentofallito, "Aggiornamento fallito");
			}
			return ResponseBase<PscCo02?>.Success();

		}

		public async Task<ResponseBase<PscCo02?>> DeleteRecordAsync(decimal idDoc, string cRisorsa)
		{
			string cDitta = CommonCostants.CDitta;
			PscCo02 pscCo02 = await _db.PscCo02s.SingleOrDefaultAsync(e => e.IdDoc == idDoc && e.CRisorsa == cRisorsa && e.CDitta == cDitta);
			if (pscCo02 == null)
			{
				return ResponseBase<PscCo02?>.Failed(PscCo02Errors.RecordNonTrovati, "Non sono stati trovati record in PscCo02");
			}
			_db.PscCo02s.Remove(pscCo02);
			return ResponseBase<PscCo02?>.Success();
		}

	}
}
