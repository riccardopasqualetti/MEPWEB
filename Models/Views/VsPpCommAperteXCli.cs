using System;
using System.Collections.Generic;

namespace Mep01Web.Models.Views;

public partial class VsPpCommAperteXCli
{
	public string CCliRag { get; set; } = null!;

	public string CommDescDd { get; set; } = null!;

	public string? TbcpCCli { get; set; }

	public string? AcliRagSoc1 { get; set; }

	public string OrpbTstDoc { get; set; } = null!;

	public string OrpbPrfDoc { get; set; } = null!;

	public decimal OrpbADoc { get; set; }

	public decimal OrpbNDoc { get; set; }

	public string? DescrizioneRidotta { get; set; }

	public string? TbcpOffPrev { get; set; }

	public string? TbcpRifCliente { get; set; }

	public string? TbcpM1Project { get; set; }

	public string? TbcpProjectManager { get; set; }

	public string? Usr1Desc { get; set; }

	public decimal TbcpId { get; set; }
}
