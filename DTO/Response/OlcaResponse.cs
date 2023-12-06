using Mep01Web.Models;

namespace Mep01Web.DTO.Response
{
	public class OlcaResponse
	{
		public FlussoOlca FlussoOlca { get; set; }		
	}

	public class OlcaCito
	{
		public FlussoOlca FlussoOlca { get; set; }
		public FlussoCito FlussoCito { get; set; }
		public OlcaCito(FlussoOlca fOlca, FlussoCito fCito)
		{
			FlussoOlca = fOlca;
			FlussoCito = fCito;
		}
	}


	public class OlcaCitoResponse
	{
        public List<OlcaCito> OlcaCitoList { get; set; }
}
}
