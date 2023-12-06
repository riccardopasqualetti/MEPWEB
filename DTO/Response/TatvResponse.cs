using Mep01Web.Models;

namespace Mep01Web.DTO.Response
{
	public class TatvResponse
	{
        public FlussoTatv ISLMasterData { get; set; }

        public string ISLRCli { get; set; }

        public TbcpResponse ISLCommData { get; set; }

    }
}
