using Mep01Web.Models;

namespace Mep01Web.DTO.Response
{
    public class TbcpResponse
    {
        public FlussoTbcp CommMasterData { get; set; }
        public string CommRCli { get; set; }
    }

	public class TbcpLightResponse
	{
		public string CommCompCode { get; set; }
		public string CommDesc { get; set; }
		public string CommCCli { get; set; }
		public string CommRCli { get; set; }
	}
}
