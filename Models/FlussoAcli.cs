using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class FlussoAcli
{
    public decimal AcliAEsercizio { get; set; }

    public string AcliCCliente { get; set; } = null!;

    public string AcliCDitta { get; set; } = null!;

    public string? AcliCNazione { get; set; }

    public string? AcliPartitaIva { get; set; }

    public decimal AcliFlgCessato { get; set; }

    public decimal AcliCConto { get; set; }

    public string? AcliRagSoc1 { get; set; }

    public string? AcliRagSoc2 { get; set; }

    public string? AcliMnemonico { get; set; }

    public string? AcliCFiscale { get; set; }

    public decimal AcliTipologia { get; set; }

    public decimal AcliArea { get; set; }

    public decimal AcliPorto { get; set; }

    public decimal AcliSpedAMezzo { get; set; }

    public decimal AcliCImballo { get; set; }

    public decimal AcliVettore1 { get; set; }

    public decimal AcliVettore2 { get; set; }

    public string? AcliCEsenzione { get; set; }

    public decimal AcliCBancaApp { get; set; }

    public string? AcliCPagamento { get; set; }

    public decimal AcliCopieFatt { get; set; }

    public DateTime? AcliDtUltFatt { get; set; }

    public decimal AcliPercSconto { get; set; }

    public string? AcliPersFisica { get; set; }

    public string? AcliPersona1 { get; set; }

    public string? AcliPersona2 { get; set; }

    public string? AcliPersona3 { get; set; }

    public string? AcliPersona4 { get; set; }

    public string? AcliCAgente { get; set; }

    public string? AcliFlgAdSpese { get; set; }

    public DateTime? AcliDtIntenti { get; set; }

    public string? AcliUfficioIva { get; set; }

    public decimal AcliPrInterno { get; set; }

    public decimal AcliPrEsterno { get; set; }

    public decimal AcliNPagina { get; set; }

    public decimal AcliDa1 { get; set; }

    public decimal AcliA1 { get; set; }

    public decimal AcliTrasf1 { get; set; }

    public decimal AcliDa2 { get; set; }

    public decimal AcliA2 { get; set; }

    public decimal AcliTrasf2 { get; set; }

    public decimal AcliDa3 { get; set; }

    public decimal AcliA3 { get; set; }

    public decimal AcliTrasf3 { get; set; }

    public decimal AcliFineMese { get; set; }

    public string? AcliCAbi { get; set; }

    public string? AcliCCab { get; set; }

    public string? AcliCCin { get; set; }

    public decimal AcliCanale { get; set; }

    public decimal AcliValFido { get; set; }

    public decimal AcliValFidoAgg { get; set; }

    public decimal AcliCValuta { get; set; }

    public string? AcliAutocertif { get; set; }

    public string? AcliTipoListino { get; set; }

    public decimal AcliIndirizOrd { get; set; }

    public decimal AcliIndirizBam { get; set; }

    public decimal AcliIndirizFat { get; set; }

    public decimal AcliIndirizEff { get; set; }

    public decimal AcliCampo1 { get; set; }

    public decimal AcliCampo2 { get; set; }

    public decimal AcliCampo3 { get; set; }

    public string? AcliCampo4 { get; set; }

    public string? AcliCampo5 { get; set; }

    public string? AcliCampo6 { get; set; }

    public string? AcliCodiceLingua { get; set; }

    public decimal AcliListino { get; set; }

    public DateTime? AcliDtIns { get; set; }

    public string? AcliUtenteIns { get; set; }

    public DateTime? AcliDtUm { get; set; }

    public string? AcliUtenteUm { get; set; }

    public string? AcliCMps { get; set; }

    public string? AcliCContoRicavo { get; set; }

    public decimal AcliIdAnagrafica { get; set; }

    public string? AcliFlgRipFatt { get; set; }

    public string? AcliFlgGestConai { get; set; }

    public decimal AcliPercConai { get; set; }

    public DateTime? AcliDataEsenzioneConai { get; set; }

    public decimal AcliId { get; set; }

    public string? AcliAutTrasp { get; set; }

    public string? AcliProvContabile { get; set; }

    public string? AcliCClienteFatt { get; set; }

    public string? AcliCodiceInterscambio { get; set; }

    public string? AcliIndirizzoPec { get; set; }

    public string? AcliNomeRitenute { get; set; }

    public string? AcliCogRitenute { get; set; }
}
