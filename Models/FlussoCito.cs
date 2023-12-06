using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class FlussoCito
{
    public string CitoCodice { get; set; } = null!;

    public string CitoCDitta { get; set; } = null!;

    public string? CitoDescrizione { get; set; }

    public string? CitoTRis { get; set; }

    public string? CitoClasse { get; set; }

    public string? CitoCprov { get; set; }

    public string? CitoFlgSemiliv { get; set; }

    public string? CitoCSemiliv { get; set; }

    public string? CitoTAliq { get; set; }

    public decimal CitoQtaUnit { get; set; }

    public decimal CitoQtaRif { get; set; }

    public decimal CitoQtaLottoMin { get; set; }

    public string? CitoParTec { get; set; }

    public decimal CitoQtaLottoEco { get; set; }

    public decimal CitoQtaLottoMax { get; set; }

    public string? CitoNote { get; set; }

    public string? CitoTAttr { get; set; }

    public string? CitoFlgUm { get; set; }

    public string? CitoCdlI { get; set; }

    public string? CitoTAliqRunI { get; set; }

    public decimal CitoQtaScRunI { get; set; }

    public decimal CitoTmAttrSecI { get; set; }

    public decimal CitoTmAggSecI { get; set; }

    public decimal CitoRunSecUniI { get; set; }

    public string? CitoCdlE { get; set; }

    public string? CitoTAliqRunE { get; set; }

    public decimal CitoQtaScRunE { get; set; }

    public decimal CitoTmAttrSecE { get; set; }

    public decimal CitoTmAggSecE { get; set; }

    public decimal CitoRunSecUniE { get; set; }

    public string? CitoOperSuccI { get; set; }

    public string? CitoOperSuccE { get; set; }

    public DateTime? CitoDtIns { get; set; }

    public string? CitoUtenteIns { get; set; }

    public DateTime? CitoDtUm { get; set; }

    public string? CitoUtenteUm { get; set; }

    public string? CitoTOperFdc { get; set; }

    public string? CitoCmaatt { get; set; }

    public decimal CitoId { get; set; }

    public string? CitoTmiatt { get; set; }

    public string? CitoGrpCosto { get; set; }

    public decimal CitoSeqVisAvan { get; set; }

    public string? CitoTpartint { get; set; }

    public string? CitoSuffPart { get; set; }

    public decimal CitoPercCplPrec { get; set; }

    public decimal CitoFasctrl { get; set; }

    public string? CitoGrp1 { get; set; }
}
