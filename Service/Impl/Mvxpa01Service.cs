using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using Mep01Web.Models.Views;
using Mep01Web.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Mep01Web.Service.Impl
{
    public class Mvxpa01Service : IMvxpa01Service
	{
		private readonly SataconsultingContext _db;

		public Mvxpa01Service(SataconsultingContext dbContext)
		{
			_db = dbContext;
		}


		//select xde00_cm0_desc, * from mtxpa01, mtxde00
		//where xpa01_c00_cditta = '01'
		//-- key mtxde00
		//and   xde00_c00_ctabella = 'mtxpa01'
		//and   xde00_n00_id_dato = xpa01_n00_id
		//and   xde00_n0d_idxtali = '3'
		//and   xde00_c00_cditta = xpa01_c00_cditta
		public async Task<ResponseBase<List<Mvxpa01>>?> GetMvxpa01Async()
		{
			List<Mvxpa01> mvxpa01 = await _db.Mvxpa01s.ToListAsync();

			if (mvxpa01 == null)
			{
				return ResponseBase<List<Mvxpa01>?>.Failed("-1", $"Non sono stati trovati record mvxpa01");
			}
			return ResponseBase <List<Mvxpa01>?>.Success(mvxpa01);
		}
	}
}
