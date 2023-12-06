using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class FlussoOlca
{
    public string? OlcaCdl { get; set; }

    public string? OlcaTRis { get; set; }

    public string? OlcaClasse { get; set; }

    public string OlcaTstDoc { get; set; } = null!;

    public string OlcaPrfDoc { get; set; } = null!;

    public decimal OlcaADoc { get; set; }

    public decimal OlcaNDoc { get; set; }

    public decimal OlcaNRigaDoc { get; set; }

    public decimal OlcaNFrazDoc { get; set; }

    public decimal OlcaNOper { get; set; }

    public string OlcaCDitta { get; set; } = null!;

    public decimal OlcaLivOper { get; set; }

    public decimal OlcaNOperPadre { get; set; }

    public string? OlcaCProv { get; set; }

    public decimal OlcaOrdinatore { get; set; }

    public string? OlcaCSemiliv { get; set; }

    public string? OlcaTAliq { get; set; }

    public decimal OlcaQtaUni { get; set; }

    public decimal OlcaQtaTot { get; set; }

    public decimal OlcaQtaManc { get; set; }

    public decimal OlcaQtaFabb { get; set; }

    public decimal OlcaQtaInOrd { get; set; }

    public decimal OlcaQtaInOrdP { get; set; }

    public decimal OlcaQtaImp { get; set; }

    public string? OlcaTAliqRun { get; set; }

    public decimal OlcaQtaScRun { get; set; }

    public decimal OlcaRunSecUni { get; set; }

    public decimal OlcaRunSecTot { get; set; }

    public decimal OlcaTmAttrSec { get; set; }

    public decimal OlcaTmAggSec { get; set; }

    public decimal OlcaDuraLavSec { get; set; }

    public decimal OlcaDuraMancSec { get; set; }

    public string? OlcaClassePrior { get; set; }

    public DateTime? OlcaDtPrior { get; set; }

    public string? OlcaFlgAnti { get; set; }

    public decimal OlcaQtaAnti { get; set; }

    public decimal OlcaTmAntiSec { get; set; }

    public DateTime? OlcaDtIApp { get; set; }

    public decimal OlcaOraIApp { get; set; }

    public DateTime? OlcaDtFApp { get; set; }

    public decimal OlcaOraFApp { get; set; }

    public DateTime? OlcaDtIApt { get; set; }

    public decimal OlcaOraIApt { get; set; }

    public DateTime? OlcaDtFApt { get; set; }

    public decimal OlcaOraFApt { get; set; }

    public DateTime? OlcaDtISched { get; set; }

    public decimal OlcaOraISched { get; set; }

    public DateTime? OlcaDtFSched { get; set; }

    public decimal OlcaOraFSched { get; set; }

    public DateTime? OlcaDtFSO { get; set; }

    public decimal OlcaSwCritico { get; set; }

    public decimal OlcaOraFSO { get; set; }

    public string? OlcaTOper { get; set; }

    public string? OlcaFlgSched { get; set; }

    public string? OlcaFlgEvaso { get; set; }

    public decimal OlcaQtaLEcon { get; set; }

    public decimal OlcaTmLEconSec { get; set; }

    public decimal OlcaNCicloOrig { get; set; }

    public string? OlcaDesc { get; set; }

    public string? OlcaFlgMulti { get; set; }

    public DateTime? OlcaDtIns { get; set; }

    public string? OlcaUtenteIns { get; set; }

    public DateTime? OlcaDtUm { get; set; }

    public string? OlcaUtenteUm { get; set; }

    public string? OlcaCmaatt { get; set; }

    public decimal OlcaId { get; set; }

    public string? OlcaTpartint { get; set; }

    public string? OlcaSuffPart { get; set; }

    public decimal OlcaPercCplPrec { get; set; }

    public decimal OlcaPercComp { get; set; }

    public decimal OlcaCMagPrel { get; set; }

    public decimal OlcaCrrgQtaVrst { get; set; }

    public decimal OlcaCrrgQtaScarto { get; set; }

    public decimal OlcaCrrgQtaRilavora { get; set; }

    public string? OlcaDescOper { get; set; }
}
