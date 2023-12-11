using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class VsConsXComm
{
    public string TbcpTstComm { get; set; } = null!;

    public string TbcpPrfComm { get; set; } = null!;

    public decimal TbcpAComm { get; set; }

    public decimal TbcpNComm { get; set; }

    public string? TbcpStatus { get; set; }

    public string? TbcpCCli { get; set; }

    public string? AcliRagSoc1 { get; set; }

    public string? TbcpOffPrev { get; set; }

    public string? TbcpRifCliente { get; set; }

    public decimal TbcpCarea { get; set; }

    public string? DescrizioneRidotta { get; set; }

    public string? Descrizione { get; set; }

    public string? TbcpM1Project { get; set; }

    public string? TbcpDesc { get; set; }

    public string? TbcpProjectManager { get; set; }

    public string? Usr1Desc { get; set; }

    public decimal TbcpId { get; set; }

    public decimal? CPgm2 { get; set; }

    public decimal? CSoa3 { get; set; }

    public decimal? CPjm4 { get; set; }

    public decimal? CBuc8 { get; set; }

    public decimal? CSys7 { get; set; }

    public decimal? CGen { get; set; }

    public decimal? CrrgPgm2 { get; set; }

    public decimal? CrrgSoa3 { get; set; }

    public decimal? CrrgPjm4 { get; set; }

    public decimal? CrrgBuc8 { get; set; }

    public decimal? CrrgSys7 { get; set; }

    public decimal? CrrgGen { get; set; }
}
