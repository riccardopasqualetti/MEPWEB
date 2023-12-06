using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class FlussoOrpb
{
    public string? OrpbTRiga { get; set; }

    public string? OrpbCPart { get; set; }

    public string OrpbTstDoc { get; set; } = null!;

    public string OrpbPrfDoc { get; set; } = null!;

    public decimal OrpbADoc { get; set; }

    public decimal OrpbNDoc { get; set; }

    public decimal OrpbPosDoc { get; set; }

    public string OrpbCDitta { get; set; } = null!;

    public decimal OrpbCVersDb { get; set; }

    public string? OrpbDesc { get; set; }

    public string? OrpbTOper { get; set; }

    public string? OrpbTstDocOrig { get; set; }

    public string? OrpbPrfDocOrig { get; set; }

    public decimal OrpbADocOrig { get; set; }

    public decimal OrpbNDocOrig { get; set; }

    public decimal OrpbPosDocOrig { get; set; }

    public decimal OrpbVmatUniPr { get; set; }

    public decimal OrpbVmatLUniPr { get; set; }

    public decimal OrpbVintUniPr { get; set; }

    public decimal OrpbVestUniPr { get; set; }

    public decimal OrpbVestLUniPr { get; set; }

    public decimal OrpbVmatUniLi { get; set; }

    public decimal OrpbVmatLUniLi { get; set; }

    public decimal OrpbVintUniLi { get; set; }

    public decimal OrpbVestUniLi { get; set; }

    public decimal OrpbVestLUniLi { get; set; }

    public decimal OrpbVmatTotPr { get; set; }

    public decimal OrpbVintTotPr { get; set; }

    public decimal OrpbVestTotPr { get; set; }

    public decimal OrpbVmatTotLi { get; set; }

    public decimal OrpbVintTotLi { get; set; }

    public decimal OrpbVestTotLi { get; set; }

    public decimal OrpbPercSc1 { get; set; }

    public decimal OrpbPercSc2 { get; set; }

    public decimal OrpbPercSc3 { get; set; }

    public decimal OrpbPercSc { get; set; }

    public string? OrpbTDest { get; set; }

    public string? OrpbTstDocDest { get; set; }

    public string? OrpbPrfDocDest { get; set; }

    public decimal OrpbADocDest { get; set; }

    public decimal OrpbNDocDest { get; set; }

    public decimal OrpbPosDocDest { get; set; }

    public string? OrpbCDest { get; set; }

    public decimal OrpbLeadtFix { get; set; }

    public string? OrpbNote { get; set; }

    public string? OrpbFlgUm { get; set; }

    public string? OrpbUm { get; set; }

    public string? OrpbTFattConv { get; set; }

    public decimal OrpbFattConv { get; set; }

    public decimal OrpbCCogest { get; set; }

    public string? OrpbContoCg { get; set; }

    public string? OrpbCCertcoll { get; set; }

    public string? OrpbEspRevDoc { get; set; }

    public DateTime? OrpbDtRevDoc { get; set; }

    public string? OrpbCTesto { get; set; }

    public string? OrpbCTesto1 { get; set; }

    public string? OrpbCDisegno { get; set; }

    public string? OrpbCDisegno1 { get; set; }

    public decimal OrpbNCiclo { get; set; }

    public decimal OrpbPercProvv { get; set; }

    public decimal OrpbCLive { get; set; }

    public string? OrpbVsRif { get; set; }

    public string? OrpbCIva { get; set; }

    public decimal OrpbPercIva { get; set; }

    public string? OrpbFlgSaldo { get; set; }

    public DateTime? OrpbDtSaldo { get; set; }

    public string? OrpbDocEstEv { get; set; }

    public string? OrpbCAgente { get; set; }

    public decimal OrpbProvv1 { get; set; }

    public decimal OrpbProvv2 { get; set; }

    public decimal OrpbProvv3 { get; set; }

    public string? OrpbFlgQc { get; set; }

    public decimal OrpbNOperSo { get; set; }

    public DateTime? OrpbDtIns { get; set; }

    public string? OrpbUtenteIns { get; set; }

    public DateTime? OrpbDtUm { get; set; }

    public string? OrpbUtenteUm { get; set; }

    public string? OrpbFlgMrp { get; set; }

    public string? OrpbConsegna { get; set; }

    public decimal OrpbCImb { get; set; }

    public string? OrpbImballoEst { get; set; }

    public string? OrpbCProv { get; set; }

    public decimal OrpbCItem { get; set; }

    public string? OrpbAltItem { get; set; }

    public string? OrpbNote2 { get; set; }

    public string? OrpbVsRif2 { get; set; }

    public string? OrpbVsRif3 { get; set; }

    public string? OrpbItemCli { get; set; }

    public decimal OrpbVmatUniPrAlt { get; set; }

    public decimal OrpbVmatLUniPrAlt { get; set; }

    public decimal OrpbVmatUniLiAlt { get; set; }

    public decimal OrpbVmatLUniLAlt { get; set; }

    public string? OrpbFlgPrezzoUm { get; set; }

    public string? OrpbUmAlt { get; set; }

    public decimal OrpbPrgDocOrig { get; set; }

    public decimal OrpbCN1 { get; set; }

    public decimal OrpbCN2 { get; set; }

    public string? OrpbCA1 { get; set; }

    public string? OrpbCA2 { get; set; }

    public string? OrpbCCoind { get; set; }

    public string? OrpbCDestCliFor { get; set; }

    public decimal OrpbCN3 { get; set; }

    public string? OrpbCA3 { get; set; }

    public decimal OrpbCN4 { get; set; }

    public string? OrpbCA4 { get; set; }

    public string? OrpbFlgPakl { get; set; }

    public string? OrpbFlgStatus { get; set; }

    public string? OrpbNomePc { get; set; }

    public decimal OrpbTPrzBase { get; set; }

    public decimal OrpbMarkup { get; set; }

    public decimal OrpbVmatUniPrMarkup { get; set; }

    public decimal OrpbDim1 { get; set; }

    public decimal OrpbDim2 { get; set; }

    public decimal OrpbCostoRif { get; set; }

    public string? OrpbProvvigioniMod { get; set; }

    public decimal OrpbImportoOmaggiPr { get; set; }

    public decimal OrpbImportoOmaggiLi { get; set; }

    public decimal OrpbStatogg { get; set; }

    public decimal OrpbQtaEvasaTrg { get; set; }

    public decimal OrpbQtaEvasaUmaTrg { get; set; }

    public decimal OrpbQtaInevasaTrg { get; set; }

    public decimal OrpbQtaInevasaUmaTrg { get; set; }

    public decimal OrpbQtaTotaleTrg { get; set; }

    public decimal OrpbQtaTotaleUmaTrg { get; set; }

    public string? OrpbFlgEmissOrd { get; set; }

    public string? OrpbTstDocOrd { get; set; }

    public string? OrpbPrfDocOrd { get; set; }

    public decimal OrpbADocOrd { get; set; }

    public decimal OrpbNDocOrd { get; set; }

    public decimal OrpbPosDocOrd { get; set; }

    public string? OrpbTstDocBe { get; set; }

    public string? OrpbPrfDocBe { get; set; }

    public decimal OrpbADocBe { get; set; }

    public decimal OrpbNDocBe { get; set; }

    public decimal OrpbPosDocBe { get; set; }

    public decimal OrpbPrgDocMin { get; set; }

    public string? OrpbCMatricola { get; set; }

    public string? OrpbRangeMatricole { get; set; }

    public decimal? OrpbN0dIdxbg02 { get; set; }

    public string? OrpbFlgFuoriBudget { get; set; }

    public DateTime? OrpbDtIniVal { get; set; }

    public DateTime? OrpbDtFinVal { get; set; }

    public decimal OrpbM1OrdinatoreDoc { get; set; }

    public string? OrpbEspRevDb { get; set; }

    public decimal OrpbMatricolaIniziale { get; set; }

    public string? OrpbM1TstCommRif { get; set; }

    public string? OrpbM1PrfCommRif { get; set; }

    public decimal OrpbM1ACommRif { get; set; }

    public decimal OrpbM1NCommRif { get; set; }

    public decimal OrpbId { get; set; }

    public string? OrpbCPartAlt { get; set; }

    public decimal OrpbPaklCSrl { get; set; }

    public string? OrpbRmaAccountproductid { get; set; }

    public string? OrpbFlgGaranzia { get; set; }

    public decimal OrpbCarea { get; set; }

    public string? OrpbM1Project { get; set; }

    public decimal OrpbM1CMag { get; set; }

    public DateTime? OrpbCD1 { get; set; }

    public DateTime? OrpbCD2 { get; set; }

    public DateTime? OrpbCD3 { get; set; }

    public string? OrpbM1Treetype { get; set; }

    public decimal OrpbM1Docentry { get; set; }

    public decimal OrpbM1Linenum { get; set; }

    public string? OrpbCLivAz { get; set; }

    public decimal OrpbIdfilemaker { get; set; }

    public decimal OrpbDocstatsped { get; set; }

    public decimal OrpbQtaInEtichetta { get; set; }

    public decimal OrpbM1NuovaRev { get; set; }

    public DateTime? OrpbDataFattRif { get; set; }

    public string? OrpbNumeroFattRif { get; set; }

    public string? OrpbCig { get; set; }

    public string? OrpbCup { get; set; }

    public string? OrpbM1Warehouse { get; set; }

    public decimal OrpbNRigaDettaglioXml { get; set; }

    public string? OrpbM1Objtype { get; set; }

    public string? OrpbM1Molo { get; set; }

    public string? OrpbM1Stabilimento { get; set; }

    public string? OrpbCNazProvMerce { get; set; }
}
