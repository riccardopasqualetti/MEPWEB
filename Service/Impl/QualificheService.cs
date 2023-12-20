using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using Mep01Web.Models.Views;
using MepWeb.DTO.Response;
using MepWeb.Exeptions;
using MepWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MepWeb.Service.Impl
{
    public class QualificheService : IQualificheService
    {
        private readonly SataconsultingContext _db;
        public QualificheService(SataconsultingContext dbContext)
        {
            _db = dbContext;
        }
        public async Task<ResponseBase<List<Mvxzz12>?>> GetAllFromMvxzz12Async()
        {
            var response = await _db.Mvxzz12s.Where(x => x.Cprfc == "grpcdl").ToListAsync();
            if (response.Count() == 0)
            {
                return ResponseBase<List<Mvxzz12>?>.Failed(GenericException.RecordInesistente, "Non sono stati trovati records");
            }
            return ResponseBase<List<Mvxzz12>?>.Success(response);
        }
    }
}
