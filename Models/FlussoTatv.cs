using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class FlussoTatv
{
    public decimal TatvId { get; set; }

    public string TatvCDitta { get; set; } = null!;

    public DateTime? TatvDtReg { get; set; }

    public string? TatvCPart { get; set; }

    public string? TatvDesc { get; set; }

    public string? TatvCCli { get; set; }

    public string? TatvRifCliente { get; set; }

    public DateTime? TatvDtConsRic { get; set; }

    public DateTime? TatvDtConsPrev { get; set; }

    public string? TatvTstComm { get; set; }

    public string? TatvPrfComm { get; set; }

    public decimal TatvAComm { get; set; }

    public decimal TatvNComm { get; set; }

    public string? TatvNote { get; set; }

    public string? TatvEspRev { get; set; }

    public DateTime? TatvDtRev { get; set; }

    public decimal? TatvN0dIdxge21 { get; set; }

    public decimal? TatvN0dIdxge22 { get; set; }

    public string? TatvCdl { get; set; }

    public decimal TatvTplatt { get; set; }

    public decimal TatvPrior { get; set; }

    public string? TatvCPartApp { get; set; }

    public string? TatvTstOli { get; set; }

    public string? TatvPrfOli { get; set; }

    public decimal TatvAOli { get; set; }

    public decimal TatvNOli { get; set; }

    public decimal TatvIdPadre { get; set; }

    public DateTime? TatvDtIns { get; set; }

    public string? TatvUtenteIns { get; set; }

    public DateTime? TatvDtUm { get; set; }

    public string? TatvUtenteUm { get; set; }

    public string? TatvIndR1 { get; set; }

    public string? TatvIndR2 { get; set; }

    public string? TatvIndR3 { get; set; }

    public string? TatvIndR4 { get; set; }

    public string? TatvReleaseNotes { get; set; }

    public decimal TatvTplapp { get; set; }

    public decimal TatvStimaGg { get; set; }

    public string? TatvCdlFunzionale { get; set; }

    public DateTime? TatvDtChiusura { get; set; }

    public decimal TatvResiduoGg { get; set; }

    public decimal TatvFlgOfferta { get; set; }

    public string? TatvCdlDelivery { get; set; }

    public string? TatvNumVerbale { get; set; }

    public DateTime? TatvDtDeliveryEndTest { get; set; }

    public decimal TatvStimaTestGg { get; set; }

    public decimal TatvTestAggregato { get; set; }

    public string? TatvTestConfermato { get; set; }

    public string? TatvBuildCandidate { get; set; }

    public string? TatvBuildRelease { get; set; }

    public string? TatvTestCritico { get; set; }

    public string? TatvVincoloCli { get; set; }

    public DateTime? TatvDtPrevConsFunz { get; set; }

    public string? TatvCdlProgetto { get; set; }

    public decimal TatvStimaFunzGg { get; set; }

    public DateTime? TatvDataRichCliOrig { get; set; }

    public decimal TatvFlgValidConfermato { get; set; }

    public string? TatvTipo { get; set; }

    public string? TatvTag { get; set; }

    public string? TatvCausAtt { get; set; }

    public string? TatvSviluppoFittizio { get; set; }

    public decimal TatvResiduoGgTest { get; set; }

    public string? TatvTestFittizio { get; set; }

    public decimal TatvResFreezeGg { get; set; }

    public decimal TatvResFreezeTestGg { get; set; }
}
