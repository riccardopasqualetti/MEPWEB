using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class FlussoOrpa
{
    public string? OrpaCCliFor { get; set; }

    public string OrpaTstDoc { get; set; } = null!;

    public string OrpaPrfDoc { get; set; } = null!;

    public decimal OrpaADoc { get; set; }

    public decimal OrpaNDoc { get; set; }

    public string OrpaCDitta { get; set; } = null!;

    public DateTime? OrpaDtEmis { get; set; }

    public DateTime? OrpaDtIniVal { get; set; }

    public DateTime? OrpaDtFinVal { get; set; }

    public string? OrpaEspRevDoc { get; set; }

    public DateTime? OrpaDtRevDoc { get; set; }

    public string? OrpaFlgStatus { get; set; }

    public string? OrpaTstDocOrig { get; set; }

    public string? OrpaPrfDocOrig { get; set; }

    public decimal OrpaADocOrig { get; set; }

    public decimal OrpaNDocOrig { get; set; }

    public string? OrpaDocOrigineEst { get; set; }

    public decimal OrpaCResa { get; set; }

    public string? OrpaResaEst { get; set; }

    public string? OrpaAutTrasp { get; set; }

    public decimal OrpaVettore1 { get; set; }

    public decimal OrpaVettore2 { get; set; }

    public decimal OrpaVettore3 { get; set; }

    public decimal OrpaPercSc { get; set; }

    public decimal OrpaPercScPag { get; set; }

    public decimal OrpaPercSc3 { get; set; }

    public decimal OrpaCValuta { get; set; }

    public decimal OrpaCambioEm { get; set; }

    public string? OrpaCEsenzione { get; set; }

    public string? OrpaCPagamento { get; set; }

    public decimal OrpaCBancaApp { get; set; }

    public decimal OrpaIndSpedMat { get; set; }

    public decimal OrpaIndSpedFa { get; set; }

    public string? OrpaCAgente { get; set; }

    public string? OrpaNoteAgente { get; set; }

    public decimal OrpaProvv1 { get; set; }

    public decimal OrpaProvv2 { get; set; }

    public decimal OrpaProvv3 { get; set; }

    public decimal OrpaCPenale { get; set; }

    public string? OrpaPenaleEst { get; set; }

    public decimal OrpaGgPenaleEst { get; set; }

    public decimal OrpaGgFranchiEst { get; set; }

    public string? OrpaNote { get; set; }

    public string? OrpaUtenteAut { get; set; }

    public DateTime? OrpaDtAut { get; set; }

    public string? OrpaTInvio { get; set; }

    public DateTime? OrpaDtInvio { get; set; }

    public string? OrpaVsRif { get; set; }

    public decimal OrpaCAreaAcq { get; set; }

    public decimal OrpaCAreaDest { get; set; }

    public DateTime? OrpaDtStp { get; set; }

    public string? OrpaCTesto { get; set; }

    public string? OrpaCTesto1 { get; set; }

    public string? OrpaCTesto2 { get; set; }

    public decimal OrpaCLive { get; set; }

    public string? OrpaIndSRagSoc { get; set; }

    public string? OrpaIndSVia { get; set; }

    public string? OrpaIndSCap { get; set; }

    public string? OrpaIndSCitta { get; set; }

    public string? OrpaIndSProv { get; set; }

    public string? OrpaIndSNaz { get; set; }

    public string? OrpaFlgAddBolli { get; set; }

    public string? OrpaFlgAddSbanc { get; set; }

    public decimal OrpaSpBanc { get; set; }

    public decimal OrpaSpBancVi { get; set; }

    public decimal OrpaSpImb { get; set; }

    public decimal OrpaSpImbVi { get; set; }

    public decimal OrpaSpTrasp { get; set; }

    public decimal OrpaSpTraspVi { get; set; }

    public decimal OrpaAcconto { get; set; }

    public decimal OrpaAccontoVi { get; set; }

    public string? OrpaCCausTrasp { get; set; }

    public string? OrpaFlgRipFatt { get; set; }

    public DateTime? OrpaDtIniTr { get; set; }

    public decimal OrpaOraIniTr { get; set; }

    public decimal? OrpaPesoLKg { get; set; }

    public decimal? OrpaPesoNKg { get; set; }

    public decimal? OrpaNColli { get; set; }

    public string? OrpaAspettoBeni { get; set; }

    public DateTime? OrpaDtIns { get; set; }

    public string? OrpaUtenteIns { get; set; }

    public DateTime? OrpaDtUm { get; set; }

    public string? OrpaUtenteUm { get; set; }

    public string? OrpaCCliFatt { get; set; }

    public string? OrpaPagamentoEst { get; set; }

    public string? OrpaGaranzia { get; set; }

    public string? OrpaEspRevT { get; set; }

    public DateTime? OrpaDtRevT { get; set; }

    public string? OrpaCAvDoc { get; set; }

    public decimal OrpaCImb { get; set; }

    public string? OrpaCImballoEst { get; set; }

    public string? OrpaAttne { get; set; }

    public string? OrpaCc { get; set; }

    public decimal OrpaCIntesta { get; set; }

    public string? OrpaIntesta { get; set; }

    public string? OrpaCertificazione { get; set; }

    public string? OrpaConsegna { get; set; }

    public string? OrpaUtente1 { get; set; }

    public string? OrpaUtente2 { get; set; }

    public string? OrpaUtente3 { get; set; }

    public string? OrpaUtente4 { get; set; }

    public string? OrpaUtente5 { get; set; }

    public decimal OrpaCLingua { get; set; }

    public string? OrpaIndPRagSoc { get; set; }

    public string? OrpaIndPVia { get; set; }

    public string? OrpaIndPCap { get; set; }

    public string? OrpaIndPCitta { get; set; }

    public string? OrpaIndPProv { get; set; }

    public string? OrpaIndPNaz { get; set; }

    public string? OrpaFlgMagazzino { get; set; }

    public decimal OrpaScontoIncd { get; set; }

    public string? OrpaMezzoSped { get; set; }

    public string? OrpaCPackDoc { get; set; }

    public string? OrpaTFattNota { get; set; }

    public decimal OrpaPerc1 { get; set; }

    public string? OrpaCod1 { get; set; }

    public DateTime? OrpaDtVsRif { get; set; }

    public string? OrpaRevVsRif { get; set; }

    public string? OrpaFlgCee { get; set; }

    public string? OrpaCA1 { get; set; }

    public string? OrpaCA2 { get; set; }

    public string? OrpaCA3 { get; set; }

    public decimal OrpaCN1 { get; set; }

    public decimal OrpaCN2 { get; set; }

    public decimal OrpaCN3 { get; set; }

    public DateTime? OrpaDtConsDef { get; set; }

    public string? OrpaTstComm { get; set; }

    public string? OrpaPrfComm { get; set; }

    public decimal OrpaAComm { get; set; }

    public decimal OrpaNComm { get; set; }

    public decimal OrpaPosComm { get; set; }

    public decimal OrpaIdTrasportoIntra { get; set; }

    public decimal OrpaIdNaturaIntra { get; set; }

    public decimal OrpaIdCondConsIntra { get; set; }

    public decimal OrpaIdNazOrigineIntra { get; set; }

    public decimal OrpaIdNazDestIntra { get; set; }

    public string? OrpaCProvOrigineIntra { get; set; }

    public string? OrpaCProvDestIntra { get; set; }

    public decimal OrpaPaklCSrl { get; set; }

    public string? OrpaTEmis { get; set; }

    public string? OrpaFlgPagAnticipato { get; set; }

    public string? OrpaPaeseDest { get; set; }

    public decimal OrpaCServizio { get; set; }

    public DateTime? OrpaDtFabb { get; set; }

    public decimal OrpaValTot { get; set; }

    public string? OrpaFlgNoBid { get; set; }

    public DateTime? OrpaDtRicevDoc { get; set; }

    public decimal OrpaQtaTot { get; set; }

    public string? OrpaEspRevLdr { get; set; }

    public DateTime? OrpaDtRevLdr { get; set; }

    public string? OrpaCF1 { get; set; }

    public string? OrpaCF2 { get; set; }

    public string? OrpaCF3 { get; set; }

    public string? OrpaCF4 { get; set; }

    public string? OrpaCF5 { get; set; }

    public string? OrpaTDest { get; set; }

    public string? OrpaCAtex { get; set; }

    public string? OrpaNomePc { get; set; }

    public string? OrpaCSil { get; set; }

    public string? OrpaCertifSil { get; set; }

    public string? OrpaTipologiaDoc { get; set; }

    public string? OrpaCittaResa { get; set; }

    public string? OrpaTOrdCli { get; set; }

    public string? OrpaFlgFatt { get; set; }

    public decimal OrpaMarkup { get; set; }

    public string? OrpaCPagamentoAgg { get; set; }

    public string? OrpaPagamentoEstAgg { get; set; }

    public string? OrpaCodTOrdCli { get; set; }

    public string? OrpaMtbf { get; set; }

    public string? OrpaFlgArrotondaFil { get; set; }

    public string? OrpaFlgAutorizzaFil { get; set; }

    public string? OrpaMezzoArrivoMerce { get; set; }

    public string? OrpaBollaArrivoMerce { get; set; }

    public string? OrpaProvMerce { get; set; }

    public decimal OrpaNBancaliMerce { get; set; }

    public string? OrpaTordoc { get; set; }

    public string? OrpaTsogc { get; set; }

    public string? OrpaTSoggProv { get; set; }

    public string? OrpaCSoggProv { get; set; }

    public string? OrpaTSoggDest { get; set; }

    public string? OrpaCSoggDest { get; set; }

    public string? OrpaAssegnaProvvigioni { get; set; }

    public string? OrpaTcons { get; set; }

    public decimal? OrpaN0dIdxge22 { get; set; }

    public decimal OrpaId { get; set; }

    public string? OrpaFlgSaldoTrg { get; set; }

    public decimal OrpaProgressivoIndSped { get; set; }

    public string? OrpaFlgEscludiCosto { get; set; }

    public DateTime? OrpaDataDecPagamento { get; set; }

    public decimal? OrpaN0dIdxge21 { get; set; }

    public string? OrpaFlgFuoriBudget { get; set; }

    public string? OrpaCu0Cbudg { get; set; }

    public DateTime? OrpaDtCalcoloScad { get; set; }

    public string? OrpaModIncassoIntra { get; set; }

    public string? OrpaModErogazioneIntra { get; set; }

    public string? OrpaIdRma { get; set; }

    public string? OrpaM1Currcode { get; set; }

    public string? OrpaM1Currname { get; set; }

    public string? OrpaM1Project { get; set; }

    public string? OrpaCGrp { get; set; }

    public decimal OrpaM1Docentry { get; set; }

    public decimal OrpaPercTrasportoIntra { get; set; }

    public decimal OrpaIdValutaIntra { get; set; }

    public decimal OrpaCambioIntra { get; set; }

    public string? OrpaPaeseIncassoIntra { get; set; }

    public string? OrpaIdArxivar { get; set; }

    public DateTime? OrpaDtExpArxivar { get; set; }

    public string? OrpaCLivAz { get; set; }

    public string? OrpaIndiceRevisioneFilemaker { get; set; }

    public decimal OrpaTipcatdoc { get; set; }

    public string? OrpaM1Warehouse { get; set; }

    public string? OrpaFlgProcessato1 { get; set; }

    public string? OrpaFlgProcessato2 { get; set; }

    public string? OrpaFlgExpXml { get; set; }

    public string? OrpaNomeFileXml { get; set; }

    public DateTime? OrpaDtLiquidazioneIva { get; set; }

    public DateTime? OrpaDtRegIva { get; set; }

    public DateTime? OrpaDtGiornale { get; set; }

    public string? OrpaMm1InvioMassivo { get; set; }

    public string? OrpaTipoDocXml { get; set; }

    public DateTime? OrpaDataFattRif { get; set; }

    public string? OrpaNumeroFattRif { get; set; }

    public string? OrpaCig { get; set; }

    public string? OrpaCup { get; set; }

    public string? OrpaPercorsoNomeFileXml { get; set; }

    public string? OrpaSerialiLight { get; set; }

    public string? OrpaM1Objtype { get; set; }
}
