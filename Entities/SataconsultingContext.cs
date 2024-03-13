using Microsoft.EntityFrameworkCore;
using Mep01Web.Models;
using Mep01Web.Models.Views;
using MepWeb.Models;
using MepWeb.Models.Views;

namespace Mep01Web.Infrastructure;

public partial class SataconsultingContext : DbContext
{
    public SataconsultingContext()
    {
    }

    public SataconsultingContext(DbContextOptions<SataconsultingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FlussoAcli> FlussoAclis { get; set; }
    public virtual DbSet<FlussoMacc> FlussoMaccs { get; set; }

    public virtual DbSet<FlussoCrrg> FlussoCrrgs { get; set; }

    public virtual DbSet<FlussoTatv> FlussoTatvs { get; set; }

    public virtual DbSet<FlussoTbcp> FlussoTbcps { get; set; }

    public virtual DbSet<Mtxde00> Mtxde00s { get; set; }

    public virtual DbSet<Mtxge22> Mtxge22s { get; set; }

    public virtual DbSet<Mtxzz12> Mtxzz12s { get; set; }

    public virtual DbSet<Tbcli> Tbclis { get; set; }

	public virtual DbSet<FlussoCito> FlussoCitos { get; set; }

	public virtual DbSet<FlussoOlca> FlussoOlcas { get; set; }

	public virtual DbSet<Mtxpa01> Mtxpa01s { get; set; }

	public virtual DbSet<FlussoTbpn> FlussoTbpns { get; set; }

    public virtual DbSet<FlussoOrpa> FlussoOrpas { get; set; }

    public virtual DbSet<FlussoOrpb> FlussoOrpbs { get; set; }

    public virtual DbSet<FlussoUsr1> FlussoUsr1s { get; set; }

    public virtual DbSet<PscCo01> PscCo01s { get; set; }

    public virtual DbSet<PscCo02> PscCo02s { get; set; }

    public virtual DbSet<PscCo03> PscCo03s { get; set; }

	public virtual DbSet<FlussoTcdl> FlussoTcdls { get; set; }

	public virtual DbSet<PscQual> PscQuals { get; set; }

    public virtual DbSet<VsPpCalendarRis> VsPpCalendarRis { get; set; }
	public virtual DbSet<Psc001a> Psc001as { get; set; }



	// Views
	public virtual DbSet<Mvxpa01> Mvxpa01s { get; set; }
    public virtual DbSet<VsPpMonitorIsl> VsPpMonitorIsl { get; set; }
    public virtual DbSet<Mvxzz12> Mvxzz12s { get; set; }
    public virtual DbSet<VsConsXComm> VsConsXComms { get; set; }
	public virtual DbSet<VsPpCommAperteXCli> VsPpCommAperteXClis { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Main");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
              .HasDefaultSchema("dba")
              .UseCollation("Latin1_General_CI_AS");


        modelBuilder.Entity<FlussoAcli>(entity =>
        {
            entity.HasKey(e => new { e.AcliAEsercizio, e.AcliCCliente, e.AcliCDitta }).HasName("acli_chiave");

            entity.ToTable("flusso_acli");

            entity.Property(e => e.AcliAEsercizio)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_a_esercizio");
            entity.Property(e => e.AcliCCliente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("acli_c_cliente");
            entity.Property(e => e.AcliCDitta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("acli_c_ditta");
            entity.Property(e => e.AcliA1)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_a_1");
            entity.Property(e => e.AcliA2)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_a_2");
            entity.Property(e => e.AcliA3)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_a_3");
            entity.Property(e => e.AcliArea)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_area");
            entity.Property(e => e.AcliAutTrasp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("acli_aut_trasp");
            entity.Property(e => e.AcliAutocertif)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("acli_autocertif");
            entity.Property(e => e.AcliCAbi)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("acli_c_abi");
            entity.Property(e => e.AcliCAgente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("acli_c_agente");
            entity.Property(e => e.AcliCBancaApp)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("acli_c_banca_app");
            entity.Property(e => e.AcliCCab)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("acli_c_cab");
            entity.Property(e => e.AcliCCin)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("acli_c_cin");
            entity.Property(e => e.AcliCClienteFatt)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("acli_c_cliente_fatt");
            entity.Property(e => e.AcliCConto)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_c_conto");
            entity.Property(e => e.AcliCContoRicavo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("acli_c_conto_ricavo");
            entity.Property(e => e.AcliCEsenzione)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("acli_c_esenzione");
            entity.Property(e => e.AcliCFiscale)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("acli_c_fiscale");
            entity.Property(e => e.AcliCImballo)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_c_imballo");
            entity.Property(e => e.AcliCMps)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("acli_c_mps");
            entity.Property(e => e.AcliCNazione)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("acli_c_nazione");
            entity.Property(e => e.AcliCPagamento)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("acli_c_pagamento");
            entity.Property(e => e.AcliCValuta)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_c_valuta");
            entity.Property(e => e.AcliCampo1)
                .HasColumnType("numeric(18, 3)")
                .HasColumnName("acli_campo_1");
            entity.Property(e => e.AcliCampo2)
                .HasColumnType("numeric(18, 3)")
                .HasColumnName("acli_campo_2");
            entity.Property(e => e.AcliCampo3)
                .HasColumnType("numeric(18, 3)")
                .HasColumnName("acli_campo_3");
            entity.Property(e => e.AcliCampo4)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("acli_campo_4");
            entity.Property(e => e.AcliCampo5)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("acli_campo_5");
            entity.Property(e => e.AcliCampo6)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("acli_campo_6");
            entity.Property(e => e.AcliCanale)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_canale");
            entity.Property(e => e.AcliCodiceInterscambio)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("acli_codice_interscambio");
            entity.Property(e => e.AcliCodiceLingua)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("acli_codice_lingua");
            entity.Property(e => e.AcliCogRitenute)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("acli_cog_ritenute");
            entity.Property(e => e.AcliCopieFatt)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("acli_copie_fatt");
            entity.Property(e => e.AcliDa1)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_da_1");
            entity.Property(e => e.AcliDa2)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_da_2");
            entity.Property(e => e.AcliDa3)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_da_3");
            entity.Property(e => e.AcliDataEsenzioneConai)
                .HasColumnType("datetime")
                .HasColumnName("acli_data_esenzione_conai");
            entity.Property(e => e.AcliDtIns)
                .HasColumnType("datetime")
                .HasColumnName("acli_dt_ins");
            entity.Property(e => e.AcliDtIntenti)
                .HasColumnType("datetime")
                .HasColumnName("acli_dt_intenti");
            entity.Property(e => e.AcliDtUltFatt)
                .HasColumnType("datetime")
                .HasColumnName("acli_dt_ult_fatt");
            entity.Property(e => e.AcliDtUm)
                .HasColumnType("datetime")
                .HasColumnName("acli_dt_um");
            entity.Property(e => e.AcliFineMese)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("acli_fine_mese");
            entity.Property(e => e.AcliFlgAdSpese)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("acli_flg_ad_spese");
            entity.Property(e => e.AcliFlgCessato)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("acli_flg_cessato");
            entity.Property(e => e.AcliFlgGestConai)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("acli_flg_gest_conai");
            entity.Property(e => e.AcliFlgRipFatt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("acli_flg_rip_fatt");
            entity.Property(e => e.AcliId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("acli_id");
            entity.Property(e => e.AcliIdAnagrafica)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("acli_id_anagrafica");
            entity.Property(e => e.AcliIndirizBam)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_indiriz_bam");
            entity.Property(e => e.AcliIndirizEff)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_indiriz_eff");
            entity.Property(e => e.AcliIndirizFat)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_indiriz_fat");
            entity.Property(e => e.AcliIndirizOrd)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_indiriz_ord");
            entity.Property(e => e.AcliIndirizzoPec)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("acli_indirizzo_pec");
            entity.Property(e => e.AcliListino)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("acli_listino");
            entity.Property(e => e.AcliMnemonico)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("acli_mnemonico");
            entity.Property(e => e.AcliNPagina)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("acli_n_pagina");
            entity.Property(e => e.AcliNomeRitenute)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("acli_nome_ritenute");
            entity.Property(e => e.AcliPartitaIva)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("acli_partita_iva");
            entity.Property(e => e.AcliPercConai)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("acli_perc_conai");
            entity.Property(e => e.AcliPercSconto)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("acli_perc_sconto");
            entity.Property(e => e.AcliPersFisica)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("acli_pers_fisica");
            entity.Property(e => e.AcliPersona1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("acli_persona_1");
            entity.Property(e => e.AcliPersona2)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("acli_persona_2");
            entity.Property(e => e.AcliPersona3)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("acli_persona_3");
            entity.Property(e => e.AcliPersona4)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("acli_persona_4");
            entity.Property(e => e.AcliPorto)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_porto");
            entity.Property(e => e.AcliPrEsterno)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("acli_pr_esterno");
            entity.Property(e => e.AcliPrInterno)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("acli_pr_interno");
            entity.Property(e => e.AcliProvContabile)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("acli_prov_contabile");
            entity.Property(e => e.AcliRagSoc1)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("acli_rag_soc_1");
            entity.Property(e => e.AcliRagSoc2)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("acli_rag_soc_2");
            entity.Property(e => e.AcliSpedAMezzo)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_sped_a_mezzo");
            entity.Property(e => e.AcliTipoListino)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("acli_tipo_listino");
            entity.Property(e => e.AcliTipologia)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_tipologia");
            entity.Property(e => e.AcliTrasf1)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_trasf_1");
            entity.Property(e => e.AcliTrasf2)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_trasf_2");
            entity.Property(e => e.AcliTrasf3)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_trasf_3");
            entity.Property(e => e.AcliUfficioIva)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("acli_ufficio_iva");
            entity.Property(e => e.AcliUtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("acli_utente_ins");
            entity.Property(e => e.AcliUtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("acli_utente_um");
            entity.Property(e => e.AcliValFido)
                .HasColumnType("numeric(17, 3)")
                .HasColumnName("acli_val_fido");
            entity.Property(e => e.AcliValFidoAgg)
                .HasColumnType("numeric(17, 3)")
                .HasColumnName("acli_val_fido_agg");
            entity.Property(e => e.AcliVettore1)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_vettore_1");
            entity.Property(e => e.AcliVettore2)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("acli_vettore_2");
        });

        modelBuilder.Entity<FlussoCrrg>(entity =>
        {
            entity.HasKey(e => new { e.CrrgCSrl, e.CrrgCDitta }).HasName("crrg_chiave");

            entity.ToTable("flusso_crrg", "dba", tb =>
            {
                tb.HasTrigger("t_flusso_crrg_agg_residuo_delete");
                tb.HasTrigger("t_flusso_crrg_agg_residuo_insert");
                tb.HasTrigger("t_flusso_crrg_agg_residuo_update");
                tb.HasTrigger("t_flusso_crrg_ai");
                tb.HasTrigger("t_flusso_crrg_au");
                tb.HasTrigger("trg_ss_crrg_del");
                tb.HasTrigger("trg_ss_crrg_del_cdccom");
                tb.HasTrigger("trg_ss_crrg_ins");
                tb.HasTrigger("trg_ss_crrg_ins_cdccom");
                tb.HasTrigger("trg_ss_crrg_mod");
                tb.HasTrigger("trg_ss_crrg_mod_cdccom");
            });

            entity.HasIndex(e => new { e.CrrgTstDoc, e.CrrgPrfDoc, e.CrrgADoc, e.CrrgNDoc, e.CrrgPosDoc, e.CrrgPrgDoc, e.CrrgNOper, e.CrrgTRis, e.CrrgCRis, e.CrrgDtt }, "crrg_chiave_alt_1").HasFillFactor(90);

            entity.HasIndex(e => e.CrrgCSrlPadre, "crrg_chiave_alt_2").HasFillFactor(90);

            entity.HasIndex(e => new { e.CrrgFlgElab, e.Crrg01Ac, e.CrrgCSrl }, "crrg_chiave_alt_3").HasFillFactor(90);

            entity.HasIndex(e => e.CrrgN0dIdxolcm, "crrg_chiave_alt_4").HasFillFactor(90);

            entity.HasIndex(e => new { e.CrrgTRis, e.CrrgCRis, e.CrrgCDitta }, "crrg_chiave_alt_5");

            entity.HasIndex(e => new { e.CrrgFlgEsito, e.CrrgCDitta }, "crrg_chiave_alt_6");

            entity.HasIndex(e => new { e.CrrgRifCliente, e.CrrgCDitta }, "crrg_chiave_alt_7");

            entity.HasIndex(e => new { e.CrrgM1ObjtypeOdl, e.CrrgM1DocentryOdl, e.CrrgCDitta }, "crrg_chiave_alt_8");

            entity.Property(e => e.CrrgCSrl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("crrg_c_srl");
            entity.Property(e => e.CrrgCDitta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("crrg_c_ditta");
            entity.Property(e => e.Crrg01Ac)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("crrg_0_1_ac");
            entity.Property(e => e.CrrgAComp)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("crrg_a_comp");
            entity.Property(e => e.CrrgADoc)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("crrg_a_doc");
            entity.Property(e => e.CrrgApp)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("crrg_app");
            entity.Property(e => e.CrrgCAddetto)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("crrg_c_addetto");
            entity.Property(e => e.CrrgCCaus)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("crrg_c_caus");
            entity.Property(e => e.CrrgCFormato)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("crrg_c_formato");
            entity.Property(e => e.CrrgCInsieme)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("crrg_c_insieme");
            entity.Property(e => e.CrrgCLtt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("crrg_c_ltt");
            entity.Property(e => e.CrrgCPart)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("crrg_c_part");
            entity.Property(e => e.CrrgCRis)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("crrg_c_ris");
            entity.Property(e => e.CrrgCSeq)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("crrg_c_seq");
            entity.Property(e => e.CrrgCSrlFiglio)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("crrg_c_srl_figlio");
            entity.Property(e => e.CrrgCSrlPadre)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("crrg_c_srl_padre");
            entity.Property(e => e.CrrgCcausfdc)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("crrg_ccausfdc");
            entity.Property(e => e.CrrgCdl)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("crrg_cdl");
            entity.Property(e => e.CrrgCmaatt)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("crrg_cmaatt");
            entity.Property(e => e.CrrgDtIns)
                .HasColumnType("datetime")
                .HasColumnName("crrg_dt_ins");
            entity.Property(e => e.CrrgDtUm)
                .HasColumnType("datetime")
                .HasColumnName("crrg_dt_um");
            entity.Property(e => e.CrrgDtt)
                .HasColumnType("datetime")
                .HasColumnName("crrg_dtt");
            entity.Property(e => e.CrrgDttApertura)
                .HasColumnType("datetime")
                .HasColumnName("crrg_dtt_apertura");
            entity.Property(e => e.CrrgDttChiusura)
                .HasColumnType("datetime")
                .HasColumnName("crrg_dtt_chiusura");
            entity.Property(e => e.CrrgDttFine)
                .HasColumnType("datetime")
                .HasColumnName("crrg_dtt_fine");
            entity.Property(e => e.CrrgDttIni)
                .HasColumnType("datetime")
                .HasColumnName("crrg_dtt_ini");
            entity.Property(e => e.CrrgDttRipr)
                .HasColumnType("datetime")
                .HasColumnName("crrg_dtt_ripr");
            entity.Property(e => e.CrrgDttSosp)
                .HasColumnType("datetime")
                .HasColumnName("crrg_dtt_sosp");
            entity.Property(e => e.CrrgErr1)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("crrg_err_1");
            entity.Property(e => e.CrrgErr2)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("crrg_err_2");
            entity.Property(e => e.CrrgErrDt)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("crrg_err_dt");
            entity.Property(e => e.CrrgErrExt)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("crrg_err_ext");
            entity.Property(e => e.CrrgErrExtDt)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("crrg_err_ext_dt");
            entity.Property(e => e.CrrgErrOper)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("crrg_err_oper");
            entity.Property(e => e.CrrgErrPres)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("crrg_err_pres");
            entity.Property(e => e.CrrgErrQta)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("crrg_err_qta");
            entity.Property(e => e.CrrgErrRis)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("crrg_err_ris");
            entity.Property(e => e.CrrgErrRisDt)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("crrg_err_ris_dt");
            entity.Property(e => e.CrrgFlgCheck)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("crrg_flg_check");
            entity.Property(e => e.CrrgFlgDomina)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("crrg_flg_domina");
            entity.Property(e => e.CrrgFlgElab)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("crrg_flg_elab");
            entity.Property(e => e.CrrgFlgElabPresidio)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("crrg_flg_elab_presidio");
            entity.Property(e => e.CrrgFlgErrClear)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("crrg_flg_err_clear");
            entity.Property(e => e.CrrgFlgEsito)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("crrg_flg_esito");
            entity.Property(e => e.CrrgFlgQtaRilavora)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("crrg_flg_qta_rilavora");
            entity.Property(e => e.CrrgGrpcdlEff)
                .HasDefaultValueSql("((0))")
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("crrg_grpcdl_eff");
            entity.Property(e => e.CrrgGrpcdlPrev)
                .HasDefaultValueSql("((0))")
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("crrg_grpcdl_prev");
            entity.Property(e => e.CrrgM1DocentryOdl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("crrg_m1_docentry_odl");
            entity.Property(e => e.CrrgM1ObjtypeOdl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("crrg_m1_objtype_odl");
            entity.Property(e => e.CrrgMeseComp)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("crrg_mese_comp");
            entity.Property(e => e.CrrgMod)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("crrg_mod");
            entity.Property(e => e.CrrgN0dIdxolcm)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("crrg_n0d_idxolcm");
            entity.Property(e => e.CrrgNDoc)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("crrg_n_doc");
            entity.Property(e => e.CrrgNOper)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("crrg_n_oper");
            entity.Property(e => e.CrrgNOperOlca)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("crrg_n_oper_olca");
            entity.Property(e => e.CrrgNRif)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("crrg_n_rif");
            entity.Property(e => e.CrrgNTerm)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("crrg_n_term");
            entity.Property(e => e.CrrgNote)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("crrg_note");
            entity.Property(e => e.CrrgNumVerbale)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("crrg_num_verbale");
            entity.Property(e => e.CrrgPercComp)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("crrg_perc_comp");
            entity.Property(e => e.CrrgPosDoc)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("crrg_pos_doc");
            entity.Property(e => e.CrrgPrfDoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("crrg_prf_doc");
            entity.Property(e => e.CrrgPrgDoc)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("crrg_prg_doc");
            entity.Property(e => e.CrrgQtaPrel)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("crrg_qta_prel");
            entity.Property(e => e.CrrgQtaScarto)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("crrg_qta_scarto");
            entity.Property(e => e.CrrgQtaVrst)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("crrg_qta_vrst");
            entity.Property(e => e.CrrgQtaXInsieme)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("crrg_qta_x_insieme");
            entity.Property(e => e.CrrgRifCliente)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("crrg_rif_cliente");
            entity.Property(e => e.CrrgTOper)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("crrg_t_oper");
            entity.Property(e => e.CrrgTOperFdc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("crrg_t_oper_fdc");
            entity.Property(e => e.CrrgTRis)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("crrg_t_ris");
            entity.Property(e => e.CrrgTmAttrIncr)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("crrg_tm_attr_incr");
            entity.Property(e => e.CrrgTmRunIncr)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("crrg_tm_run_incr");
            entity.Property(e => e.CrrgTmRunIncrProd)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("crrg_tm_run_incr_prod");
            entity.Property(e => e.CrrgTstDoc)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("crrg_tst_doc");
            entity.Property(e => e.CrrgTurno)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("crrg_turno");
            entity.Property(e => e.CrrgTurnoData)
                .HasColumnType("datetime")
                .HasColumnName("crrg_turno_data");
            entity.Property(e => e.CrrgUtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("crrg_utente_ins");
            entity.Property(e => e.CrrgUtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("crrg_utente_um");
        });

        modelBuilder.Entity<FlussoTatv>(entity =>
        {
            entity.HasKey(e => new { e.TatvId, e.TatvCDitta }).HasName("tatv_chiave");

            entity.ToTable("flusso_tatv", tb =>
            {
                tb.HasTrigger("t_flusso_tatv_ai");
                tb.HasTrigger("t_flusso_tatv_dt_rich_cli_ins");
                tb.HasTrigger("t_flusso_tatv_dt_rich_cli_upd");
            });

            entity.HasIndex(e => e.TatvCPartApp, "tatv_chiave_alt_1");

            entity.Property(e => e.TatvId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("tatv_id");
            entity.Property(e => e.TatvCDitta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("tatv_c_ditta");
            entity.Property(e => e.TatvAComm)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tatv_a_comm");
            entity.Property(e => e.TatvAOli)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tatv_a_oli");
            entity.Property(e => e.TatvBuildCandidate)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tatv_build_candidate");
            entity.Property(e => e.TatvBuildRelease)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tatv_build_release");
            entity.Property(e => e.TatvCCli)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("tatv_c_cli");
            entity.Property(e => e.TatvCPart)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tatv_c_part");
            entity.Property(e => e.TatvCPartApp)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tatv_c_part_app");
            entity.Property(e => e.TatvCausAtt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("tatv_caus_att");
            entity.Property(e => e.TatvCdl)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("tatv_cdl");
            entity.Property(e => e.TatvCdlDelivery)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("tatv_cdl_delivery");
            entity.Property(e => e.TatvCdlFunzionale)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("tatv_cdl_funzionale");
            entity.Property(e => e.TatvCdlProgetto)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("tatv_cdl_progetto");
            entity.Property(e => e.TatvDataRichCliOrig)
                .HasColumnType("datetime")
                .HasColumnName("tatv_data_rich_cli_orig");
            entity.Property(e => e.TatvDesc)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("tatv_desc");
            entity.Property(e => e.TatvDtChiusura)
                .HasColumnType("datetime")
                .HasColumnName("tatv_dt_chiusura");
            entity.Property(e => e.TatvDtConsPrev)
                .HasColumnType("datetime")
                .HasColumnName("tatv_dt_cons_prev");
            entity.Property(e => e.TatvDtConsRic)
                .HasColumnType("datetime")
                .HasColumnName("tatv_dt_cons_ric");
            entity.Property(e => e.TatvDtDeliveryEndTest)
                .HasColumnType("datetime")
                .HasColumnName("tatv_dt_delivery_end_test");
            entity.Property(e => e.TatvDtIns)
                .HasColumnType("datetime")
                .HasColumnName("tatv_dt_ins");
            entity.Property(e => e.TatvDtPrevConsFunz)
                .HasColumnType("datetime")
                .HasColumnName("tatv_dt_prev_cons_funz");
            entity.Property(e => e.TatvDtReg)
                .HasColumnType("datetime")
                .HasColumnName("tatv_dt_reg");
            entity.Property(e => e.TatvDtRev)
                .HasColumnType("datetime")
                .HasColumnName("tatv_dt_rev");
            entity.Property(e => e.TatvDtUm)
                .HasColumnType("datetime")
                .HasColumnName("tatv_dt_um");
            entity.Property(e => e.TatvEspRev)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("tatv_esp_rev");
            entity.Property(e => e.TatvFlgOfferta)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tatv_flg_offerta");
            entity.Property(e => e.TatvFlgValidConfermato)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tatv_flg_valid_confermato");
            entity.Property(e => e.TatvIdPadre)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("tatv_id_padre");
            entity.Property(e => e.TatvIndR1)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("tatv_ind_r1");
            entity.Property(e => e.TatvIndR2)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("tatv_ind_r2");
            entity.Property(e => e.TatvIndR3)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("tatv_ind_r3");
            entity.Property(e => e.TatvIndR4)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("tatv_ind_r4");
            entity.Property(e => e.TatvN0dIdxge21)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("tatv_n0d_idxge21");
            entity.Property(e => e.TatvN0dIdxge22)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("tatv_n0d_idxge22");
            entity.Property(e => e.TatvNComm)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("tatv_n_comm");
            entity.Property(e => e.TatvNOli)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("tatv_n_oli");
            entity.Property(e => e.TatvNote)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("tatv_note");
            entity.Property(e => e.TatvNumVerbale)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tatv_num_verbale");
            entity.Property(e => e.TatvPrfComm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tatv_prf_comm");
            entity.Property(e => e.TatvPrfOli)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tatv_prf_oli");
            entity.Property(e => e.TatvPrior)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tatv_prior");
            entity.Property(e => e.TatvReleaseNotes)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("tatv_release_notes");
            entity.Property(e => e.TatvResFreezeGg)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("tatv_res_freeze_gg");
            entity.Property(e => e.TatvResFreezeTestGg)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("tatv_res_freeze_test_gg");
            entity.Property(e => e.TatvResiduoGg)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("tatv_residuo_gg");
            entity.Property(e => e.TatvResiduoGgTest)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("tatv_residuo_gg_test");
            entity.Property(e => e.TatvRifCliente)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tatv_rif_cliente");
            entity.Property(e => e.TatvStimaFunzGg)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("tatv_stima_funz_gg");
            entity.Property(e => e.TatvStimaGg)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("tatv_stima_gg");
            entity.Property(e => e.TatvStimaTestGg)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("tatv_stima_test_gg");
            entity.Property(e => e.TatvSviluppoFittizio)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tatv_sviluppo_fittizio");
            entity.Property(e => e.TatvTag)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("tatv_tag");
            entity.Property(e => e.TatvTestAggregato)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tatv_test_aggregato");
            entity.Property(e => e.TatvTestConfermato)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tatv_test_confermato");
            entity.Property(e => e.TatvTestCritico)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tatv_test_critico");
            entity.Property(e => e.TatvTestFittizio)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tatv_test_fittizio");
            entity.Property(e => e.TatvTipo)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("tatv_tipo");
            entity.Property(e => e.TatvTplapp)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tatv_tplapp");
            entity.Property(e => e.TatvTplatt)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tatv_tplatt");
            entity.Property(e => e.TatvTstComm)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("tatv_tst_comm");
            entity.Property(e => e.TatvTstOli)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("tatv_tst_oli");
            entity.Property(e => e.TatvUtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("tatv_utente_ins");
            entity.Property(e => e.TatvUtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("tatv_utente_um");
            entity.Property(e => e.TatvVincoloCli)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tatv_vincolo_cli");
        });

        modelBuilder.Entity<FlussoTbcp>(entity =>
        {
            entity.HasKey(e => new { e.TbcpTstComm, e.TbcpPrfComm, e.TbcpAComm, e.TbcpNComm, e.TbcpCDitta }).HasName("tbcp_chiave");

            entity.ToTable("flusso_tbcp", tb =>
            {
                tb.HasTrigger("trg_ss_tbcp_ins");
                tb.HasTrigger("trg_ss_tbcp_mod");
                tb.HasTrigger("trg_ss_tbcp_paag_del");
                tb.HasTrigger("trg_ss_tbcp_paag_mod");
            });

            entity.Property(e => e.TbcpTstComm)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("tbcp_tst_comm");
            entity.Property(e => e.TbcpPrfComm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_prf_comm");
            entity.Property(e => e.TbcpAComm)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_a_comm");
            entity.Property(e => e.TbcpNComm)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("tbcp_n_comm");
            entity.Property(e => e.TbcpCDitta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("tbcp_c_ditta");
            entity.Property(e => e.TbcpACommBase)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_a_comm_base");
            entity.Property(e => e.TbcpACommPd)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_a_comm_pd");
            entity.Property(e => e.TbcpADocDest)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_a_doc_dest");
            entity.Property(e => e.TbcpCA1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tbcp_c_a_1");
            entity.Property(e => e.TbcpCA2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tbcp_c_a_2");
            entity.Property(e => e.TbcpCCli)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("tbcp_c_cli");
            entity.Property(e => e.TbcpCCostoGest)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_c_costo_gest");
            entity.Property(e => e.TbcpCDest)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tbcp_c_dest");
            entity.Property(e => e.TbcpCMagazzino)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_c_magazzino");
            entity.Property(e => e.TbcpCN1)
                .HasColumnType("numeric(16, 4)")
                .HasColumnName("tbcp_c_n_1");
            entity.Property(e => e.TbcpCN2)
                .HasColumnType("numeric(16, 4)")
                .HasColumnName("tbcp_c_n_2");
            entity.Property(e => e.TbcpCPart)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tbcp_c_part");
            entity.Property(e => e.TbcpCPartUt)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tbcp_c_part_ut");
            entity.Property(e => e.TbcpCUnicodMax)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_c_unicod_max");
            entity.Property(e => e.TbcpCarea)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_carea");
            entity.Property(e => e.TbcpCdc)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("tbcp_cdc");
            entity.Property(e => e.TbcpClassePrior)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("tbcp_classe_prior");
            entity.Property(e => e.TbcpCostoBudgetAcq)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_costo_budget_acq");
            entity.Property(e => e.TbcpCostoBudgetEst)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_costo_budget_est");
            entity.Property(e => e.TbcpCostoBudgetInt)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_costo_budget_int");
            entity.Property(e => e.TbcpCostoBudgetVari)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_costo_budget_vari");
            entity.Property(e => e.TbcpCostoP)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_costo_p");
            entity.Property(e => e.TbcpCostoPOre)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("tbcp_costo_p_ore");
            entity.Property(e => e.TbcpDesc)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("tbcp_desc");
            entity.Property(e => e.TbcpDest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_dest");
            entity.Property(e => e.TbcpDocAllegato)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tbcp_doc_allegato");
            entity.Property(e => e.TbcpDtConf)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_conf");
            entity.Property(e => e.TbcpDtConsRic)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_cons_ric");
            entity.Property(e => e.TbcpDtEmi)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_emi");
            entity.Property(e => e.TbcpDtEspRevTestCo)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_esp_rev_test_co");
            entity.Property(e => e.TbcpDtEv)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_ev");
            entity.Property(e => e.TbcpDtFPrev)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_f_prev");
            entity.Property(e => e.TbcpDtFabb)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_fabb");
            entity.Property(e => e.TbcpDtFineProgett)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_fine_progett");
            entity.Property(e => e.TbcpDtIniProg)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_ini_prog");
            entity.Property(e => e.TbcpDtIns)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_ins");
            entity.Property(e => e.TbcpDtPrevEvasa)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_prev_evasa");
            entity.Property(e => e.TbcpDtPrior)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_prior");
            entity.Property(e => e.TbcpDtRif)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_rif");
            entity.Property(e => e.TbcpDtStudioPrel)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_studio_prel");
            entity.Property(e => e.TbcpDtUltRilascioDb)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_ult_rilascio_db");
            entity.Property(e => e.TbcpDtUm)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_dt_um");
            entity.Property(e => e.TbcpEspRevCo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_esp_rev_co");
            entity.Property(e => e.TbcpEspRevDb)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("tbcp_esp_rev_db");
            entity.Property(e => e.TbcpEspRevTestCo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("tbcp_esp_rev_test_co");
            entity.Property(e => e.TbcpFfllA1)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_a_1");
            entity.Property(e => e.TbcpFfllA10)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_a_10");
            entity.Property(e => e.TbcpFfllA2)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_a_2");
            entity.Property(e => e.TbcpFfllA3)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_a_3");
            entity.Property(e => e.TbcpFfllA4)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_a_4");
            entity.Property(e => e.TbcpFfllA5)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_a_5");
            entity.Property(e => e.TbcpFfllA6)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_a_6");
            entity.Property(e => e.TbcpFfllA7)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_a_7");
            entity.Property(e => e.TbcpFfllA8)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_a_8");
            entity.Property(e => e.TbcpFfllA9)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_a_9");
            entity.Property(e => e.TbcpFfllD1)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_ffll_d_1");
            entity.Property(e => e.TbcpFfllD2)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_ffll_d_2");
            entity.Property(e => e.TbcpFfllD3)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_ffll_d_3");
            entity.Property(e => e.TbcpFfllD4)
                .HasColumnType("datetime")
                .HasColumnName("tbcp_ffll_d_4");
            entity.Property(e => e.TbcpFfllG1)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_g_1");
            entity.Property(e => e.TbcpFfllG2)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_g_2");
            entity.Property(e => e.TbcpFfllG3)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_g_3");
            entity.Property(e => e.TbcpFfllG4)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_g_4");
            entity.Property(e => e.TbcpFfllN1)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_ffll_n_1");
            entity.Property(e => e.TbcpFfllN10)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_ffll_n_10");
            entity.Property(e => e.TbcpFfllN2)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_ffll_n_2");
            entity.Property(e => e.TbcpFfllN3)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_ffll_n_3");
            entity.Property(e => e.TbcpFfllN4)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_ffll_n_4");
            entity.Property(e => e.TbcpFfllN5)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_ffll_n_5");
            entity.Property(e => e.TbcpFfllN6)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_ffll_n_6");
            entity.Property(e => e.TbcpFfllN7)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_ffll_n_7");
            entity.Property(e => e.TbcpFfllN8)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_ffll_n_8");
            entity.Property(e => e.TbcpFfllN9)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_ffll_n_9");
            entity.Property(e => e.TbcpFfllT1)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("tbcp_ffll_t_1");
            entity.Property(e => e.TbcpFlgColl)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_flg_coll");
            entity.Property(e => e.TbcpFlgCommCompleta)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_flg_comm_completa");
            entity.Property(e => e.TbcpFlgDaEvadere)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_flg_da_evadere");
            entity.Property(e => e.TbcpFlgMrp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_flg_mrp");
            entity.Property(e => e.TbcpFlgTotDest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_flg_tot_dest");
            entity.Property(e => e.TbcpId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("tbcp_id");
            entity.Property(e => e.TbcpIndImpianto)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tbcp_ind_impianto");
            entity.Property(e => e.TbcpLeadPrevGg)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("tbcp_lead_prev_gg");
            entity.Property(e => e.TbcpM1Idlivellofase)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tbcp_m1_idlivellofase");
            entity.Property(e => e.TbcpM1Project)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tbcp_m1_project");
            entity.Property(e => e.TbcpN0dIdxge21)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("tbcp_n0d_idxge21");
            entity.Property(e => e.TbcpN0dIdxge22)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("tbcp_n0d_idxge22");
            entity.Property(e => e.TbcpNCommBase)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("tbcp_n_comm_base");
            entity.Property(e => e.TbcpNCommPd)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("tbcp_n_comm_pd");
            entity.Property(e => e.TbcpNDocDest)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("tbcp_n_doc_dest");
            entity.Property(e => e.TbcpNFaseMax)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_n_fase_max");
            entity.Property(e => e.TbcpNStrutt)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("tbcp_n_strutt");
            entity.Property(e => e.TbcpNUltSpec)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_n_ult_spec");
            entity.Property(e => e.TbcpNote)
                .HasMaxLength(600)
                .IsUnicode(false)
                .HasColumnName("tbcp_note");
            entity.Property(e => e.TbcpOffPrev)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tbcp_off_prev");
            entity.Property(e => e.TbcpOreBudgetInt)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("tbcp_ore_budget_int");
            entity.Property(e => e.TbcpPrfCommBase)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_prf_comm_base");
            entity.Property(e => e.TbcpPrfCommPd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_prf_comm_pd");
            entity.Property(e => e.TbcpPrfDocDest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_prf_doc_dest");
            entity.Property(e => e.TbcpPrfMatricola)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("tbcp_prf_matricola");
            entity.Property(e => e.TbcpProjectManager)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("tbcp_project_manager");
            entity.Property(e => e.TbcpQta)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("tbcp_qta");
            entity.Property(e => e.TbcpQtaEvasa)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("tbcp_qta_evasa");
            entity.Property(e => e.TbcpQtaRic)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("tbcp_qta_ric");
            entity.Property(e => e.TbcpRMax)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("tbcp_r_max");
            entity.Property(e => e.TbcpRicavoP)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("tbcp_ricavo_p");
            entity.Property(e => e.TbcpRifCliente)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tbcp_rif_cliente");
            entity.Property(e => e.TbcpRifDisegno)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("tbcp_rif_disegno");
            entity.Property(e => e.TbcpStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_status");
            entity.Property(e => e.TbcpStatusCpl)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_status_cpl");
            entity.Property(e => e.TbcpTImpianto)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tbcp_t_impianto");
            entity.Property(e => e.TbcpTplcomm)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_tplcomm");
            entity.Property(e => e.TbcpTstCommBase)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("tbcp_tst_comm_base");
            entity.Property(e => e.TbcpTstCommPd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("tbcp_tst_comm_pd");
            entity.Property(e => e.TbcpTstDocDest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("tbcp_tst_doc_dest");
            entity.Property(e => e.TbcpUltSuffMatricola)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_ult_suff_matricola");
            entity.Property(e => e.TbcpUtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("tbcp_utente_ins");
            entity.Property(e => e.TbcpUtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("tbcp_utente_um");
        });

        modelBuilder.Entity<Mtxde00>(entity =>
        {
            entity.HasKey(e => new { e.Xde00C00Ctabella, e.Xde00N00IdDato, e.Xde00N0dIdxtali, e.Xde00C00Cditta }).HasName("xde00_chiave");

            entity.ToTable("mtxde00");

            entity.Property(e => e.Xde00C00Ctabella)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("xde00_c00_ctabella");
            entity.Property(e => e.Xde00N00IdDato)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("xde00_n00_id_dato");
            entity.Property(e => e.Xde00N0dIdxtali)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("xde00_n0d_idxtali");
            entity.Property(e => e.Xde00C00Cditta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("xde00_c00_cditta");
            entity.Property(e => e.Xde00C00CtabellaOr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("xde00_c00_ctabella_or");
            entity.Property(e => e.Xde00C00Descrid)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("xde00_c00_descrid");
            entity.Property(e => e.Xde00C00UtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("xde00_c00_utente_ins");
            entity.Property(e => e.Xde00C00UtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("xde00_c00_utente_um");
            entity.Property(e => e.Xde00Cm0Desc)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("xde00_cm0_desc");
            entity.Property(e => e.Xde00Cm0Note)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("xde00_cm0_note");
            entity.Property(e => e.Xde00Dt0DtIns)
                .HasColumnType("datetime")
                .HasColumnName("xde00_dt0_dt_ins");
            entity.Property(e => e.Xde00Dt0DtUm)
                .HasColumnType("datetime")
                .HasColumnName("xde00_dt0_dt_um");
        });

        modelBuilder.Entity<Mtxge22>(entity =>
        {
            entity.HasKey(e => new { e.Xge22N00Id, e.Xge22C00Cditta }).HasName("xge22_chiave");

            entity.ToTable("mtxge22");

            entity.Property(e => e.Xge22N00Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("xge22_n00_id");
            entity.Property(e => e.Xge22C00Cditta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("xge22_c00_cditta");
            entity.Property(e => e.Xge22C00Ctabella)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("xge22_c00_ctabella");
            entity.Property(e => e.Xge22C00CtabellaOr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("xge22_c00_ctabella_or");
            entity.Property(e => e.Xge22C00UtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("xge22_c00_utente_ins");
            entity.Property(e => e.Xge22C00UtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("xge22_c00_utente_um");
            entity.Property(e => e.Xge22Dt0DtIns)
                .HasColumnType("datetime")
                .HasColumnName("xge22_dt0_dt_ins");
            entity.Property(e => e.Xge22Dt0DtUm)
                .HasColumnType("datetime")
                .HasColumnName("xge22_dt0_dt_um");
            entity.Property(e => e.Xge22N00IdDato)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("xge22_n00_id_dato");
            entity.Property(e => e.Xge22N0dIdxzz12)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("xge22_n0d_idxzz12");

            entity.HasOne(d => d.Xge22).WithMany(p => p.Mtxge22s)
                .HasForeignKey(d => new { d.Xge22N0dIdxzz12, d.Xge22C00Cditta })
                .HasConstraintName("fk_xge22_stav");
        });

        modelBuilder.Entity<Mtxzz12>(entity =>
        {
            entity.HasKey(e => new { e.Xzz12N00Id, e.Xzz12C00Cditta }).HasName("xzz12_chiave");

            entity.ToTable("mtxzz12");

            entity.Property(e => e.Xzz12N00Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("xzz12_n00_id");
            entity.Property(e => e.Xzz12C00Cditta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("xzz12_c00_cditta");
            entity.Property(e => e.Xzz12C00Cprfc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("xzz12_c00_cprfc");
            entity.Property(e => e.Xzz12C00CtabellaOr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("xzz12_c00_ctabella_or");
            entity.Property(e => e.Xzz12C00UtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("xzz12_c00_utente_ins");
            entity.Property(e => e.Xzz12C00UtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("xzz12_c00_utente_um");
            entity.Property(e => e.Xzz12Dt0DtIns)
                .HasColumnType("datetime")
                .HasColumnName("xzz12_dt0_dt_ins");
            entity.Property(e => e.Xzz12Dt0DtUm)
                .HasColumnType("datetime")
                .HasColumnName("xzz12_dt0_dt_um");
            entity.Property(e => e.Xzz12N01Cod)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("xzz12_n01_cod");
        });

        modelBuilder.Entity<Tbcli>(entity =>
        {
            entity.HasKey(e => e.CliCod).HasName("tbcli_chiave");

            entity.ToTable("tbcli");

            entity.HasIndex(e => e.CliCard, "idx_tbcli_1");

            entity.Property(e => e.CliCod)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cli_cod");
            entity.Property(e => e.CliCap)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("cli_cap");
            entity.Property(e => e.CliCard)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("cli_card");
            entity.Property(e => e.CliCog)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('n.d.')")
                .HasColumnName("cli_cog");
            entity.Property(e => e.CliCom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("cli_com");
            entity.Property(e => e.CliInd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("cli_ind");
            entity.Property(e => e.CliNom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cli_nom");
            entity.Property(e => e.CliTel)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("cli_tel");
        });

		modelBuilder.Entity<FlussoCito>(entity =>
		{
			entity.HasKey(e => new { e.CitoCodice, e.CitoCDitta }).HasName("cito_chiave");

			entity.ToTable("flusso_cito");

			entity.Property(e => e.CitoCodice)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("cito_codice");
			entity.Property(e => e.CitoCDitta)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("cito_c_ditta");
			entity.Property(e => e.CitoCSemiliv)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("cito_c_semiliv");
			entity.Property(e => e.CitoCdlE)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("cito_cdl_e");
			entity.Property(e => e.CitoCdlI)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("cito_cdl_i");
			entity.Property(e => e.CitoClasse)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("cito_classe");
			entity.Property(e => e.CitoCmaatt)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("cito_cmaatt");
			entity.Property(e => e.CitoCprov)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("cito_cprov");
			entity.Property(e => e.CitoDescrizione)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("cito_descrizione");
			entity.Property(e => e.CitoDtIns)
				.HasColumnType("datetime")
				.HasColumnName("cito_dt_ins");
			entity.Property(e => e.CitoDtUm)
				.HasColumnType("datetime")
				.HasColumnName("cito_dt_um");
			entity.Property(e => e.CitoFasctrl)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("cito_fasctrl");
			entity.Property(e => e.CitoFlgSemiliv)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("cito_flg_semiliv");
			entity.Property(e => e.CitoFlgUm)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("cito_flg_um");
			entity.Property(e => e.CitoGrp1)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("cito_grp_1");
			entity.Property(e => e.CitoGrpCosto)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("cito_grp_costo");
			entity.Property(e => e.CitoId)
				.ValueGeneratedOnAdd()
				.HasColumnType("numeric(12, 0)")
				.HasColumnName("cito_id");
			entity.Property(e => e.CitoNote)
				.HasMaxLength(240)
				.IsUnicode(false)
				.HasColumnName("cito_note");
			entity.Property(e => e.CitoOperSuccE)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("cito_oper_succ_e");
			entity.Property(e => e.CitoOperSuccI)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("cito_oper_succ_i");
			entity.Property(e => e.CitoParTec)
				.HasMaxLength(20)
				.IsUnicode(false)
				.HasColumnName("cito_par_tec");
			entity.Property(e => e.CitoPercCplPrec)
				.HasColumnType("numeric(5, 2)")
				.HasColumnName("cito_perc_cpl_prec");
			entity.Property(e => e.CitoQtaLottoEco)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("cito_qta_lotto_eco");
			entity.Property(e => e.CitoQtaLottoMax)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("cito_qta_lotto_max");
			entity.Property(e => e.CitoQtaLottoMin)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("cito_qta_lotto_min");
			entity.Property(e => e.CitoQtaRif)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("cito_qta_rif");
			entity.Property(e => e.CitoQtaScRunE)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("cito_qta_sc_run_e");
			entity.Property(e => e.CitoQtaScRunI)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("cito_qta_sc_run_i");
			entity.Property(e => e.CitoQtaUnit)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("cito_qta_unit");
			entity.Property(e => e.CitoRunSecUniE)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("cito_run_sec_uni_e");
			entity.Property(e => e.CitoRunSecUniI)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("cito_run_sec_uni_i");
			entity.Property(e => e.CitoSeqVisAvan)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("cito_seq_vis_avan");
			entity.Property(e => e.CitoSuffPart)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("cito_suff_part");
			entity.Property(e => e.CitoTAliq)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("cito_t_aliq");
			entity.Property(e => e.CitoTAliqRunE)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("cito_t_aliq_run_e");
			entity.Property(e => e.CitoTAliqRunI)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("cito_t_aliq_run_i");
			entity.Property(e => e.CitoTAttr)
				.HasMaxLength(20)
				.IsUnicode(false)
				.HasColumnName("cito_t_attr");
			entity.Property(e => e.CitoTOperFdc)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("cito_t_oper_fdc");
			entity.Property(e => e.CitoTRis)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("cito_t_ris");
			entity.Property(e => e.CitoTmAggSecE)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("cito_tm_agg_sec_e");
			entity.Property(e => e.CitoTmAggSecI)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("cito_tm_agg_sec_i");
			entity.Property(e => e.CitoTmAttrSecE)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("cito_tm_attr_sec_e");
			entity.Property(e => e.CitoTmAttrSecI)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("cito_tm_attr_sec_i");
			entity.Property(e => e.CitoTmiatt)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("cito_tmiatt");
			entity.Property(e => e.CitoTpartint)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("cito_tpartint");
			entity.Property(e => e.CitoUtenteIns)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("cito_utente_ins");
			entity.Property(e => e.CitoUtenteUm)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("cito_utente_um");
		});

		modelBuilder.Entity<FlussoOlca>(entity =>
		{
			entity.HasKey(e => new { e.OlcaTstDoc, e.OlcaPrfDoc, e.OlcaADoc, e.OlcaNDoc, e.OlcaNRigaDoc, e.OlcaNFrazDoc, e.OlcaNOper, e.OlcaCDitta }).HasName("olca_chiave");

			entity.ToTable("flusso_olca", tb => tb.HasTrigger("t_flusso_olca_ci1s_ci1p_ad"));

			entity.HasIndex(e => new { e.OlcaTstDoc, e.OlcaPrfDoc, e.OlcaADoc, e.OlcaNDoc, e.OlcaNRigaDoc, e.OlcaNFrazDoc, e.OlcaOrdinatore, e.OlcaCDitta }, "olca_chiave_alt_1").HasFillFactor(90);

			entity.HasIndex(e => new { e.OlcaId, e.OlcaCDitta }, "olca_chiave_alt_2")
				.IsUnique()
				.HasFillFactor(90);

			entity.HasIndex(e => new { e.OlcaCdl, e.OlcaCDitta }, "olca_chiave_alt_3");

			entity.Property(e => e.OlcaTstDoc)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("olca_tst_doc");
			entity.Property(e => e.OlcaPrfDoc)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("olca_prf_doc");
			entity.Property(e => e.OlcaADoc)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("olca_a_doc");
			entity.Property(e => e.OlcaNDoc)
				.HasColumnType("numeric(8, 0)")
				.HasColumnName("olca_n_doc");
			entity.Property(e => e.OlcaNRigaDoc)
				.HasColumnType("numeric(6, 0)")
				.HasColumnName("olca_n_riga_doc");
			entity.Property(e => e.OlcaNFrazDoc)
				.HasColumnType("numeric(2, 0)")
				.HasColumnName("olca_n_fraz_doc");
			entity.Property(e => e.OlcaNOper)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("olca_n_oper");
			entity.Property(e => e.OlcaCDitta)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("olca_c_ditta");
			entity.Property(e => e.OlcaCMagPrel)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("olca_c_mag_prel");
			entity.Property(e => e.OlcaCProv)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("olca_c_prov");
			entity.Property(e => e.OlcaCSemiliv)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("olca_c_semiliv");
			entity.Property(e => e.OlcaCdl)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("olca_cdl");
			entity.Property(e => e.OlcaClasse)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("olca_classe");
			entity.Property(e => e.OlcaClassePrior)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("olca_classe_prior");
			entity.Property(e => e.OlcaCmaatt)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("olca_cmaatt");
			entity.Property(e => e.OlcaCrrgQtaRilavora)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_crrg_qta_rilavora");
			entity.Property(e => e.OlcaCrrgQtaScarto)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_crrg_qta_scarto");
			entity.Property(e => e.OlcaCrrgQtaVrst)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_crrg_qta_vrst");
			entity.Property(e => e.OlcaDesc)
				.HasMaxLength(2000)
				.IsUnicode(false)
				.HasColumnName("olca_desc");
			entity.Property(e => e.OlcaDescOper)
				.HasMaxLength(2000)
				.IsUnicode(false)
				.HasColumnName("olca_desc_oper");
			entity.Property(e => e.OlcaDtFApp)
				.HasColumnType("datetime")
				.HasColumnName("olca_dt_f_app");
			entity.Property(e => e.OlcaDtFApt)
				.HasColumnType("datetime")
				.HasColumnName("olca_dt_f_apt");
			entity.Property(e => e.OlcaDtFSO)
				.HasColumnType("datetime")
				.HasColumnName("olca_dt_f_s_o");
			entity.Property(e => e.OlcaDtFSched)
				.HasColumnType("datetime")
				.HasColumnName("olca_dt_f_sched");
			entity.Property(e => e.OlcaDtIApp)
				.HasColumnType("datetime")
				.HasColumnName("olca_dt_i_app");
			entity.Property(e => e.OlcaDtIApt)
				.HasColumnType("datetime")
				.HasColumnName("olca_dt_i_apt");
			entity.Property(e => e.OlcaDtISched)
				.HasColumnType("datetime")
				.HasColumnName("olca_dt_i_sched");
			entity.Property(e => e.OlcaDtIns)
				.HasColumnType("datetime")
				.HasColumnName("olca_dt_ins");
			entity.Property(e => e.OlcaDtPrior)
				.HasColumnType("datetime")
				.HasColumnName("olca_dt_prior");
			entity.Property(e => e.OlcaDtUm)
				.HasColumnType("datetime")
				.HasColumnName("olca_dt_um");
			entity.Property(e => e.OlcaDuraLavSec)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("olca_dura_lav_sec");
			entity.Property(e => e.OlcaDuraMancSec)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("olca_dura_manc_sec");
			entity.Property(e => e.OlcaFlgAnti)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("olca_flg_anti");
			entity.Property(e => e.OlcaFlgEvaso)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("olca_flg_evaso");
			entity.Property(e => e.OlcaFlgMulti)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("olca_flg_multi");
			entity.Property(e => e.OlcaFlgSched)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("olca_flg_sched");
			entity.Property(e => e.OlcaId)
				.ValueGeneratedOnAdd()
				.HasColumnType("numeric(12, 0)")
				.HasColumnName("olca_id");
			entity.Property(e => e.OlcaLivOper)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("olca_liv_oper");
			entity.Property(e => e.OlcaNCicloOrig)
				.HasColumnType("numeric(8, 0)")
				.HasColumnName("olca_n_ciclo_orig");
			entity.Property(e => e.OlcaNOperPadre)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("olca_n_oper_padre");
			entity.Property(e => e.OlcaOraFApp)
				.HasColumnType("numeric(4, 2)")
				.HasColumnName("olca_ora_f_app");
			entity.Property(e => e.OlcaOraFApt)
				.HasColumnType("numeric(4, 2)")
				.HasColumnName("olca_ora_f_apt");
			entity.Property(e => e.OlcaOraFSO)
				.HasColumnType("numeric(4, 2)")
				.HasColumnName("olca_ora_f_s_o");
			entity.Property(e => e.OlcaOraFSched)
				.HasColumnType("numeric(4, 2)")
				.HasColumnName("olca_ora_f_sched");
			entity.Property(e => e.OlcaOraIApp)
				.HasColumnType("numeric(4, 2)")
				.HasColumnName("olca_ora_i_app");
			entity.Property(e => e.OlcaOraIApt)
				.HasColumnType("numeric(4, 2)")
				.HasColumnName("olca_ora_i_apt");
			entity.Property(e => e.OlcaOraISched)
				.HasColumnType("numeric(4, 2)")
				.HasColumnName("olca_ora_i_sched");
			entity.Property(e => e.OlcaOrdinatore)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("olca_ordinatore");
			entity.Property(e => e.OlcaPercComp)
				.HasColumnType("numeric(5, 2)")
				.HasColumnName("olca_perc_comp");
			entity.Property(e => e.OlcaPercCplPrec)
				.HasColumnType("numeric(5, 2)")
				.HasColumnName("olca_perc_cpl_prec");
			entity.Property(e => e.OlcaQtaAnti)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_qta_anti");
			entity.Property(e => e.OlcaQtaFabb)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_qta_fabb");
			entity.Property(e => e.OlcaQtaImp)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_qta_imp");
			entity.Property(e => e.OlcaQtaInOrd)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_qta_in_ord");
			entity.Property(e => e.OlcaQtaInOrdP)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_qta_in_ord_p");
			entity.Property(e => e.OlcaQtaLEcon)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_qta_l_econ");
			entity.Property(e => e.OlcaQtaManc)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_qta_manc");
			entity.Property(e => e.OlcaQtaScRun)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_qta_sc_run");
			entity.Property(e => e.OlcaQtaTot)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_qta_tot");
			entity.Property(e => e.OlcaQtaUni)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("olca_qta_uni");
			entity.Property(e => e.OlcaRunSecTot)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("olca_run_sec_tot");
			entity.Property(e => e.OlcaRunSecUni)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("olca_run_sec_uni");
			entity.Property(e => e.OlcaSuffPart)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("olca_suff_part");
			entity.Property(e => e.OlcaSwCritico)
				.HasColumnType("numeric(1, 0)")
				.HasColumnName("olca_sw_critico");
			entity.Property(e => e.OlcaTAliq)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("olca_t_aliq");
			entity.Property(e => e.OlcaTAliqRun)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("olca_t_aliq_run");
			entity.Property(e => e.OlcaTOper)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("olca_t_oper");
			entity.Property(e => e.OlcaTRis)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("olca_t_ris");
			entity.Property(e => e.OlcaTmAggSec)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("olca_tm_agg_sec");
			entity.Property(e => e.OlcaTmAntiSec)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("olca_tm_anti_sec");
			entity.Property(e => e.OlcaTmAttrSec)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("olca_tm_attr_sec");
			entity.Property(e => e.OlcaTmLEconSec)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("olca_tm_l_econ_sec");
			entity.Property(e => e.OlcaTpartint)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("olca_tpartint");
			entity.Property(e => e.OlcaUtenteIns)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("olca_utente_ins");
			entity.Property(e => e.OlcaUtenteUm)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("olca_utente_um");
		});

		modelBuilder.Entity<Mtxpa01>(entity =>
		{
			entity.HasKey(e => new { e.Xpa01N00Id, e.Xpa01C00Cditta }).HasName("xpa01_chiave");

			entity.ToTable("mtxpa01");

			entity.HasIndex(e => e.Xpa01Cu0Cmaatt, "xpa01_chiave_alt_1")
				.IsUnique()
				.HasFillFactor(90);

			entity.Property(e => e.Xpa01N00Id)
				.ValueGeneratedOnAdd()
				.HasColumnType("numeric(12, 0)")
				.HasColumnName("xpa01_n00_id");
			entity.Property(e => e.Xpa01C00Cditta)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("xpa01_c00_cditta");
			entity.Property(e => e.Xpa01C00Cord)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("xpa01_c00_cord");
			entity.Property(e => e.Xpa01C00CtabellaOr)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("xpa01_c00_ctabella_or");
			entity.Property(e => e.Xpa01C00UtenteIns)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("xpa01_c00_utente_ins");
			entity.Property(e => e.Xpa01C00UtenteUm)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("xpa01_c00_utente_um");
			entity.Property(e => e.Xpa01Cu0Cmaatt)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("xpa01_cu0_cmaatt");
			entity.Property(e => e.Xpa01CutSetcpaag)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("xpa01_cut_setcpaag");
			entity.Property(e => e.Xpa01Dt0DtIns)
				.HasColumnType("datetime")
				.HasColumnName("xpa01_dt0_dt_ins");
			entity.Property(e => e.Xpa01Dt0DtUm)
				.HasColumnType("datetime")
				.HasColumnName("xpa01_dt0_dt_um");
		});


		modelBuilder.Entity<FlussoTbpn>(entity =>
		{
			entity.HasKey(e => new { e.TbpnCPart, e.TbpnCDitta }).HasName("tbpn_chiave");

			entity.ToTable("flusso_tbpn");

			entity.Property(e => e.TbpnCPart)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_part");
			entity.Property(e => e.TbpnCDitta)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_ditta");
			entity.Property(e => e.TbpnAltezza)
				.HasColumnType("numeric(9, 3)")
				.HasColumnName("tbpn_altezza");
			entity.Property(e => e.TbpnBarcode)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("tbpn_barcode");
			entity.Property(e => e.TbpnCAliqDogana)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_aliq_dogana");
			entity.Property(e => e.TbpnCAliqIva)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_aliq_iva");
			entity.Property(e => e.TbpnCClssCosto)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_clss_costo");
			entity.Property(e => e.TbpnCCoindCosto)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_coind_costo");
			entity.Property(e => e.TbpnCCoindRicavo)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_coind_ricavo");
			entity.Property(e => e.TbpnCContoCosto)
				.HasMaxLength(20)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_conto_costo");
			entity.Property(e => e.TbpnCContoRicavo)
				.HasMaxLength(20)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_conto_ricavo");
			entity.Property(e => e.TbpnCDiseForUt)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_dise_for_ut");
			entity.Property(e => e.TbpnCDisegno)
				.HasMaxLength(255)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_disegno");
			entity.Property(e => e.TbpnCDisegnoUt)
				.HasMaxLength(20)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_disegno_ut");
			entity.Property(e => e.TbpnCElementoBase)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_elemento_base");
			entity.Property(e => e.TbpnCFamiglia)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_famiglia");
			entity.Property(e => e.TbpnCForUt)
				.HasMaxLength(8)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_for_ut");
			entity.Property(e => e.TbpnCFormato)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_formato");
			entity.Property(e => e.TbpnCFornPref)
				.HasMaxLength(8)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_forn_pref");
			entity.Property(e => e.TbpnCGrps)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_grps");
			entity.Property(e => e.TbpnCImballo)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tbpn_c_imballo");
			entity.Property(e => e.TbpnCImmagine)
				.HasMaxLength(255)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_immagine");
			entity.Property(e => e.TbpnCLivAz)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_liv_az");
			entity.Property(e => e.TbpnCLocP)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_loc_p");
			entity.Property(e => e.TbpnCLocS)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_loc_s");
			entity.Property(e => e.TbpnCMagCar)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tbpn_c_mag_car");
			entity.Property(e => e.TbpnCNote)
				.HasColumnType("numeric(12, 0)")
				.HasColumnName("tbpn_c_note");
			entity.Property(e => e.TbpnCPartPlm)
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_part_plm");
			entity.Property(e => e.TbpnCPartRif)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_part_rif");
			entity.Property(e => e.TbpnCProv)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_prov");
			entity.Property(e => e.TbpnCRaggr)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_raggr");
			entity.Property(e => e.TbpnCRaggrTec)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_raggr_tec");
			entity.Property(e => e.TbpnCRecod)
				.HasMaxLength(24)
				.IsUnicode(false)
				.HasColumnName("tbpn_c_recod");
			entity.Property(e => e.TbpnCapProdGg)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("tbpn_cap_prod_gg");
			entity.Property(e => e.TbpnCdl)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("tbpn_cdl");
			entity.Property(e => e.TbpnClProfitto)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_cl_profitto");
			entity.Property(e => e.TbpnClasseOmg)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("tbpn_classe_omg");
			entity.Property(e => e.TbpnClasseRicambi)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_classe_ricambi");
			entity.Property(e => e.TbpnClsAbc)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_cls_abc");
			entity.Property(e => e.TbpnCoeffCq)
				.HasColumnType("numeric(3, 0)")
				.HasColumnName("tbpn_coeff_cq");
			entity.Property(e => e.TbpnContenutoNetto)
				.HasColumnType("numeric(13, 6)")
				.HasColumnName("tbpn_contenuto_netto");
			entity.Property(e => e.TbpnDesc)
				.HasMaxLength(2000)
				.IsUnicode(false)
				.HasColumnName("tbpn_desc");
			entity.Property(e => e.TbpnDtIns)
				.HasColumnType("datetime")
				.HasColumnName("tbpn_dt_ins");
			entity.Property(e => e.TbpnDtUm)
				.HasColumnType("datetime")
				.HasColumnName("tbpn_dt_um");
			entity.Property(e => e.TbpnEspRev)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("tbpn_esp_rev");
			entity.Property(e => e.TbpnFattConv)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("tbpn_fatt_conv");
			entity.Property(e => e.TbpnFfllA1)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_a_1");
			entity.Property(e => e.TbpnFfllA10)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_a_10");
			entity.Property(e => e.TbpnFfllA2)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_a_2");
			entity.Property(e => e.TbpnFfllA3)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_a_3");
			entity.Property(e => e.TbpnFfllA4)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_a_4");
			entity.Property(e => e.TbpnFfllA5)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_a_5");
			entity.Property(e => e.TbpnFfllA6)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_a_6");
			entity.Property(e => e.TbpnFfllA7)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_a_7");
			entity.Property(e => e.TbpnFfllA8)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_a_8");
			entity.Property(e => e.TbpnFfllA9)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_a_9");
			entity.Property(e => e.TbpnFfllD1)
				.HasColumnType("datetime")
				.HasColumnName("tbpn_ffll_d_1");
			entity.Property(e => e.TbpnFfllD2)
				.HasColumnType("datetime")
				.HasColumnName("tbpn_ffll_d_2");
			entity.Property(e => e.TbpnFfllD3)
				.HasColumnType("datetime")
				.HasColumnName("tbpn_ffll_d_3");
			entity.Property(e => e.TbpnFfllD4)
				.HasColumnType("datetime")
				.HasColumnName("tbpn_ffll_d_4");
			entity.Property(e => e.TbpnFfllG1)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_g_1");
			entity.Property(e => e.TbpnFfllG2)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_g_2");
			entity.Property(e => e.TbpnFfllG3)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_g_3");
			entity.Property(e => e.TbpnFfllG4)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_g_4");
			entity.Property(e => e.TbpnFfllN1)
				.HasColumnType("numeric(18, 5)")
				.HasColumnName("tbpn_ffll_n_1");
			entity.Property(e => e.TbpnFfllN10)
				.HasColumnType("numeric(18, 5)")
				.HasColumnName("tbpn_ffll_n_10");
			entity.Property(e => e.TbpnFfllN2)
				.HasColumnType("numeric(18, 5)")
				.HasColumnName("tbpn_ffll_n_2");
			entity.Property(e => e.TbpnFfllN3)
				.HasColumnType("numeric(18, 5)")
				.HasColumnName("tbpn_ffll_n_3");
			entity.Property(e => e.TbpnFfllN4)
				.HasColumnType("numeric(18, 5)")
				.HasColumnName("tbpn_ffll_n_4");
			entity.Property(e => e.TbpnFfllN5)
				.HasColumnType("numeric(18, 5)")
				.HasColumnName("tbpn_ffll_n_5");
			entity.Property(e => e.TbpnFfllN6)
				.HasColumnType("numeric(18, 5)")
				.HasColumnName("tbpn_ffll_n_6");
			entity.Property(e => e.TbpnFfllN7)
				.HasColumnType("numeric(18, 5)")
				.HasColumnName("tbpn_ffll_n_7");
			entity.Property(e => e.TbpnFfllN8)
				.HasColumnType("numeric(18, 5)")
				.HasColumnName("tbpn_ffll_n_8");
			entity.Property(e => e.TbpnFfllN9)
				.HasColumnType("numeric(18, 5)")
				.HasColumnName("tbpn_ffll_n_9");
			entity.Property(e => e.TbpnFfllT1)
				.HasMaxLength(2000)
				.IsUnicode(false)
				.HasColumnName("tbpn_ffll_t_1");
			entity.Property(e => e.TbpnFlgCicloLe)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_ciclo_le");
			entity.Property(e => e.TbpnFlgCicloLi)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_ciclo_li");
			entity.Property(e => e.TbpnFlgClavAttivo)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_clav_attivo");
			entity.Property(e => e.TbpnFlgCosti)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_costi");
			entity.Property(e => e.TbpnFlgCtrl)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_ctrl");
			entity.Property(e => e.TbpnFlgDecimali)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_decimali");
			entity.Property(e => e.TbpnFlgDest)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasDefaultValueSql("('V')")
				.HasColumnName("tbpn_flg_dest");
			entity.Property(e => e.TbpnFlgDimLtt)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_dim_ltt");
			entity.Property(e => e.TbpnFlgFiscale)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_fiscale");
			entity.Property(e => e.TbpnFlgIdent)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_ident");
			entity.Property(e => e.TbpnFlgLtt)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_ltt");
			entity.Property(e => e.TbpnFlgMatricolaProvv)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_matricola_provv");
			entity.Property(e => e.TbpnFlgMic)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_mic");
			entity.Property(e => e.TbpnFlgMrp)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_mrp");
			entity.Property(e => e.TbpnFlgObsoleto)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_obsoleto");
			entity.Property(e => e.TbpnFlgOrdiniAperti)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_ordini_aperti");
			entity.Property(e => e.TbpnFlgOrdiniAuto)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_ordini_auto");
			entity.Property(e => e.TbpnFlgPrelievi)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_prelievi");
			entity.Property(e => e.TbpnFlgPrelievoComp)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_prelievo_comp");
			entity.Property(e => e.TbpnFlgRipLottoMax)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_rip_lotto_max");
			entity.Property(e => e.TbpnFlgSemiliv)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_semiliv");
			entity.Property(e => e.TbpnFlgSituaz)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_situaz");
			entity.Property(e => e.TbpnFlgStampaEtichetta)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_flg_stampa_etichetta");
			entity.Property(e => e.TbpnGgCompatta)
				.HasColumnType("numeric(3, 0)")
				.HasColumnName("tbpn_gg_compatta");
			entity.Property(e => e.TbpnGgCtrlLivRiord)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tbpn_gg_ctrl_liv_riord");
			entity.Property(e => e.TbpnGgToll)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tbpn_gg_toll");
			entity.Property(e => e.TbpnGrp1)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_grp_1");
			entity.Property(e => e.TbpnGrp2)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_grp_2");
			entity.Property(e => e.TbpnGrp3)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_grp_3");
			entity.Property(e => e.TbpnGrp4)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_grp_4");
			entity.Property(e => e.TbpnGrpCosto)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("tbpn_grp_costo");
			entity.Property(e => e.TbpnGrpMps)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_grp_mps");
			entity.Property(e => e.TbpnGrpMrp)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_grp_mrp");
			entity.Property(e => e.TbpnGrpOrdoper)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("tbpn_grp_ordoper");
			entity.Property(e => e.TbpnGrpVar)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_grp_var");
			entity.Property(e => e.TbpnId)
				.ValueGeneratedOnAdd()
				.HasColumnType("numeric(12, 0)")
				.HasColumnName("tbpn_id");
			entity.Property(e => e.TbpnIdNoco)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("tbpn_id_noco");
			entity.Property(e => e.TbpnLarghezza)
				.HasColumnType("numeric(9, 3)")
				.HasColumnName("tbpn_larghezza");
			entity.Property(e => e.TbpnLeQtaScgl)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("tbpn_le_qta_scgl");
			entity.Property(e => e.TbpnLeRunSecUn)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("tbpn_le_run_sec_un");
			entity.Property(e => e.TbpnLeTAliq)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_le_t_aliq");
			entity.Property(e => e.TbpnLeTmAggSec)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("tbpn_le_tm_agg_sec");
			entity.Property(e => e.TbpnLiQtaScgl)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("tbpn_li_qta_scgl");
			entity.Property(e => e.TbpnLiRunSecUn)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("tbpn_li_run_sec_un");
			entity.Property(e => e.TbpnLiTAliq)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_li_t_aliq");
			entity.Property(e => e.TbpnLiTmAggSec)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("tbpn_li_tm_agg_sec");
			entity.Property(e => e.TbpnLiTmAttSec)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("tbpn_li_tm_att_sec");
			entity.Property(e => e.TbpnLunghezza)
				.HasColumnType("numeric(9, 3)")
				.HasColumnName("tbpn_lunghezza");
			entity.Property(e => e.TbpnM1UmVolume)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("tbpn_m1_um_volume");
			entity.Property(e => e.TbpnM1Volume)
				.HasColumnType("numeric(12, 3)")
				.HasColumnName("tbpn_m1_volume");
			entity.Property(e => e.TbpnNDecimali)
				.HasColumnType("numeric(2, 0)")
				.HasColumnName("tbpn_n_decimali");
			entity.Property(e => e.TbpnNDimLtt)
				.HasColumnType("numeric(1, 0)")
				.HasColumnName("tbpn_n_dim_ltt");
			entity.Property(e => e.TbpnNumPerScltt)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tbpn_num_per_scltt");
			entity.Property(e => e.TbpnNumPerSclttRip)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tbpn_num_per_scltt_rip");
			entity.Property(e => e.TbpnParprogram)
				.HasMaxLength(200)
				.IsUnicode(false)
				.HasColumnName("tbpn_parprogram");
			entity.Property(e => e.TbpnPercCtrl)
				.HasColumnType("numeric(5, 2)")
				.HasColumnName("tbpn_perc_ctrl");
			entity.Property(e => e.TbpnPercScarto)
				.HasColumnType("numeric(5, 2)")
				.HasColumnName("tbpn_perc_scarto");
			entity.Property(e => e.TbpnPeso)
				.HasColumnType("numeric(9, 3)")
				.HasColumnName("tbpn_peso");
			entity.Property(e => e.TbpnPesoLordo)
				.HasColumnType("numeric(9, 3)")
				.HasColumnName("tbpn_peso_lordo");
			entity.Property(e => e.TbpnRifCertifica)
				.HasMaxLength(16)
				.IsUnicode(false)
				.HasColumnName("tbpn_rif_certifica");
			entity.Property(e => e.TbpnRifDisegno)
				.HasMaxLength(16)
				.IsUnicode(false)
				.HasColumnName("tbpn_rif_disegno");
			entity.Property(e => e.TbpnStatoLogDef)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_stato_log_def");
			entity.Property(e => e.TbpnTApprovv)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_t_approvv");
			entity.Property(e => e.TbpnTBarcode)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_t_barcode");
			entity.Property(e => e.TbpnTCertifica)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tbpn_t_certifica");
			entity.Property(e => e.TbpnTCodifCicli)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("tbpn_t_codif_cicli");
			entity.Property(e => e.TbpnTCodifDb)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("tbpn_t_codif_db");
			entity.Property(e => e.TbpnTCodifica)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("tbpn_t_codifica");
			entity.Property(e => e.TbpnTColld)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tbpn_t_colld");
			entity.Property(e => e.TbpnTFattConv)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_t_fatt_conv");
			entity.Property(e => e.TbpnTLegameCicloLe)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("tbpn_t_legame_ciclo_le");
			entity.Property(e => e.TbpnTLegameCicloLi)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("tbpn_t_legame_ciclo_li");
			entity.Property(e => e.TbpnTLtt)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_t_ltt");
			entity.Property(e => e.TbpnTPartInsieme)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_t_part_insieme");
			entity.Property(e => e.TbpnTVersInsieme)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_t_vers_insieme");
			entity.Property(e => e.TbpnTmEmOrdine)
				.HasColumnType("numeric(5, 2)")
				.HasColumnName("tbpn_tm_em_ordine");
			entity.Property(e => e.TbpnTmProtez)
				.HasColumnType("numeric(3, 0)")
				.HasColumnName("tbpn_tm_protez");
			entity.Property(e => e.TbpnTmaGg)
				.HasColumnType("numeric(5, 2)")
				.HasColumnName("tbpn_tma_gg");
			entity.Property(e => e.TbpnTpscltt)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_tpscltt");
			entity.Property(e => e.TbpnUm1)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("tbpn_um_1");
			entity.Property(e => e.TbpnUm2)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("tbpn_um_2");
			entity.Property(e => e.TbpnUmAltezza)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("tbpn_um_altezza");
			entity.Property(e => e.TbpnUmLarghezza)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("tbpn_um_larghezza");
			entity.Property(e => e.TbpnUmLunghezza)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("tbpn_um_lunghezza");
			entity.Property(e => e.TbpnUmPeso)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("tbpn_um_peso");
			entity.Property(e => e.TbpnUtenteIns)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("tbpn_utente_ins");
			entity.Property(e => e.TbpnUtenteUm)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("tbpn_utente_um");
			entity.Property(e => e.TbpnVettRilascio)
				.HasMaxLength(20)
				.IsUnicode(false)
				.HasColumnName("tbpn_vett_rilascio");
			entity.Property(e => e.TbpnYtiDataProd)
				.HasColumnType("datetime")
				.HasColumnName("tbpn_yti_data_prod");
			entity.Property(e => e.TbpnYtiFlgMagDim)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_yti_flg_mag_dim");
			entity.Property(e => e.TbpnYtiMtMaxBobina)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("tbpn_yti_mt_max_bobina");
			entity.Property(e => e.TbpnYtiMtrlSupp)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("tbpn_yti_mtrl_supp");
			entity.Property(e => e.TbpnYtiSrlSuppTGabbia)
				.HasColumnType("numeric(12, 0)")
				.HasColumnName("tbpn_yti_srl_supp_t_gabbia");
			entity.Property(e => e.TbpnYtiSrlSuppTTelaio)
				.HasColumnType("numeric(12, 0)")
				.HasColumnName("tbpn_yti_srl_supp_t_telaio");
			entity.Property(e => e.TbpnYtiSrlSuppUso)
				.HasColumnType("numeric(12, 0)")
				.HasColumnName("tbpn_yti_srl_supp_uso");
			entity.Property(e => e.TbpnYtiSuppDiam)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("tbpn_yti_supp_diam");
			entity.Property(e => e.TbpnYtiSuppDiamFlangia)
				.HasColumnType("numeric(13, 4)")
				.HasColumnName("tbpn_yti_supp_diam_flangia");
			entity.Property(e => e.TbpnYtiSuppProprietario)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbpn_yti_supp_proprietario");
		});


        modelBuilder.Entity<FlussoOrpa>(entity =>
        {
            entity.HasKey(e => new { e.OrpaTstDoc, e.OrpaPrfDoc, e.OrpaADoc, e.OrpaNDoc, e.OrpaCDitta }).HasName("orpa_chiave");

            entity.ToTable("flusso_orpa", tb =>
            {
                tb.HasTrigger("t_flusso_orpa_ai");
                tb.HasTrigger("trg_ss_orpa_paag_mod");
            });

            entity.HasIndex(e => new { e.OrpaCCliFor, e.OrpaTstDoc, e.OrpaPrfDoc, e.OrpaADoc, e.OrpaNDoc }, "orpa_chiave_alt_1").HasFillFactor(90);

            entity.HasIndex(e => new { e.OrpaFlgStatus, e.OrpaTstDoc, e.OrpaPrfDoc, e.OrpaADoc, e.OrpaNDoc }, "orpa_chiave_alt_2").HasFillFactor(90);

            entity.HasIndex(e => e.OrpaId, "orpa_chiave_alt_3");

            entity.HasIndex(e => e.OrpaIdRma, "orpa_chiave_alt_4");

            entity.HasIndex(e => new { e.OrpaTstDocOrig, e.OrpaPrfDocOrig, e.OrpaADocOrig, e.OrpaNDocOrig, e.OrpaCDitta }, "orpa_chiave_alt_5");

            entity.Property(e => e.OrpaTstDoc)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpa_tst_doc");
            entity.Property(e => e.OrpaPrfDoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_prf_doc");
            entity.Property(e => e.OrpaADoc)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_a_doc");
            entity.Property(e => e.OrpaNDoc)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpa_n_doc");
            entity.Property(e => e.OrpaCDitta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpa_c_ditta");
            entity.Property(e => e.OrpaAComm)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_a_comm");
            entity.Property(e => e.OrpaADocOrig)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_a_doc_orig");
            entity.Property(e => e.OrpaAcconto)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpa_acconto");
            entity.Property(e => e.OrpaAccontoVi)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpa_acconto_vi");
            entity.Property(e => e.OrpaAspettoBeni)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("orpa_aspetto_beni");
            entity.Property(e => e.OrpaAssegnaProvvigioni)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_assegna_provvigioni");
            entity.Property(e => e.OrpaAttne)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("orpa_attne");
            entity.Property(e => e.OrpaAutTrasp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_aut_trasp");
            entity.Property(e => e.OrpaBollaArrivoMerce)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("orpa_bolla_arrivo_merce");
            entity.Property(e => e.OrpaCA1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orpa_c_a_1");
            entity.Property(e => e.OrpaCA2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orpa_c_a_2");
            entity.Property(e => e.OrpaCA3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orpa_c_a_3");
            entity.Property(e => e.OrpaCAgente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("orpa_c_agente");
            entity.Property(e => e.OrpaCAreaAcq)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_c_area_acq");
            entity.Property(e => e.OrpaCAreaDest)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_c_area_dest");
            entity.Property(e => e.OrpaCAtex)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpa_c_atex");
            entity.Property(e => e.OrpaCAvDoc)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpa_c_av_doc");
            entity.Property(e => e.OrpaCBancaApp)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("orpa_c_banca_app");
            entity.Property(e => e.OrpaCCausTrasp)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpa_c_caus_trasp");
            entity.Property(e => e.OrpaCCliFatt)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("orpa_c_cli_fatt");
            entity.Property(e => e.OrpaCCliFor)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("orpa_c_cli_for");
            entity.Property(e => e.OrpaCEsenzione)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("orpa_c_esenzione");
            entity.Property(e => e.OrpaCF1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_c_f_1");
            entity.Property(e => e.OrpaCF2)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_c_f_2");
            entity.Property(e => e.OrpaCF3)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_c_f_3");
            entity.Property(e => e.OrpaCF4)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_c_f_4");
            entity.Property(e => e.OrpaCF5)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_c_f_5");
            entity.Property(e => e.OrpaCGrp)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("orpa_c_grp");
            entity.Property(e => e.OrpaCImb)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_c_imb");
            entity.Property(e => e.OrpaCImballoEst)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("orpa_c_imballo_est");
            entity.Property(e => e.OrpaCIntesta)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("orpa_c_intesta");
            entity.Property(e => e.OrpaCLingua)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpa_c_lingua");
            entity.Property(e => e.OrpaCLivAz)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("orpa_c_liv_az");
            entity.Property(e => e.OrpaCLive)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_c_live");
            entity.Property(e => e.OrpaCN1)
                .HasColumnType("numeric(16, 4)")
                .HasColumnName("orpa_c_n_1");
            entity.Property(e => e.OrpaCN2)
                .HasColumnType("numeric(16, 4)")
                .HasColumnName("orpa_c_n_2");
            entity.Property(e => e.OrpaCN3)
                .HasColumnType("numeric(16, 4)")
                .HasColumnName("orpa_c_n_3");
            entity.Property(e => e.OrpaCPackDoc)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("orpa_c_pack_doc");
            entity.Property(e => e.OrpaCPagamento)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("orpa_c_pagamento");
            entity.Property(e => e.OrpaCPagamentoAgg)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("orpa_c_pagamento_agg");
            entity.Property(e => e.OrpaCPenale)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_c_penale");
            entity.Property(e => e.OrpaCProvDestIntra)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orpa_c_prov_dest_intra");
            entity.Property(e => e.OrpaCProvOrigineIntra)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orpa_c_prov_origine_intra");
            entity.Property(e => e.OrpaCResa)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_c_resa");
            entity.Property(e => e.OrpaCServizio)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_c_servizio");
            entity.Property(e => e.OrpaCSil)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpa_c_sil");
            entity.Property(e => e.OrpaCSoggDest)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("orpa_c_sogg_dest");
            entity.Property(e => e.OrpaCSoggProv)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("orpa_c_sogg_prov");
            entity.Property(e => e.OrpaCTesto)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("orpa_c_testo");
            entity.Property(e => e.OrpaCTesto1)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("orpa_c_testo_1");
            entity.Property(e => e.OrpaCTesto2)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("orpa_c_testo_2");
            entity.Property(e => e.OrpaCValuta)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_c_valuta");
            entity.Property(e => e.OrpaCambioEm)
                .HasColumnType("numeric(12, 6)")
                .HasColumnName("orpa_cambio_em");
            entity.Property(e => e.OrpaCambioIntra)
                .HasColumnType("numeric(10, 6)")
                .HasColumnName("orpa_cambio_intra");
            entity.Property(e => e.OrpaCc)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("orpa_cc");
            entity.Property(e => e.OrpaCertifSil)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_certif_sil");
            entity.Property(e => e.OrpaCertificazione)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("orpa_certificazione");
            entity.Property(e => e.OrpaCig)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("orpa_cig");
            entity.Property(e => e.OrpaCittaResa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("orpa_citta_resa");
            entity.Property(e => e.OrpaCod1)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("orpa_cod_1");
            entity.Property(e => e.OrpaCodTOrdCli)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpa_cod_t_ord_cli");
            entity.Property(e => e.OrpaConsegna)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("orpa_consegna");
            entity.Property(e => e.OrpaCu0Cbudg)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("orpa_cu0_cbudg");
            entity.Property(e => e.OrpaCup)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("orpa_cup");
            entity.Property(e => e.OrpaDataDecPagamento)
                .HasColumnType("datetime")
                .HasColumnName("orpa_data_dec_pagamento");
            entity.Property(e => e.OrpaDataFattRif)
                .HasColumnType("datetime")
                .HasColumnName("orpa_data_fatt_rif");
            entity.Property(e => e.OrpaDocOrigineEst)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpa_doc_origine_est");
            entity.Property(e => e.OrpaDtAut)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_aut");
            entity.Property(e => e.OrpaDtCalcoloScad)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_calcolo_scad");
            entity.Property(e => e.OrpaDtConsDef)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_cons_def");
            entity.Property(e => e.OrpaDtEmis)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_emis");
            entity.Property(e => e.OrpaDtExpArxivar)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_exp_arxivar");
            entity.Property(e => e.OrpaDtFabb)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_fabb");
            entity.Property(e => e.OrpaDtFinVal)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_fin_val");
            entity.Property(e => e.OrpaDtGiornale)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_giornale");
            entity.Property(e => e.OrpaDtIniTr)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_ini_tr");
            entity.Property(e => e.OrpaDtIniVal)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_ini_val");
            entity.Property(e => e.OrpaDtIns)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_ins");
            entity.Property(e => e.OrpaDtInvio)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_invio");
            entity.Property(e => e.OrpaDtLiquidazioneIva)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_liquidazione_iva");
            entity.Property(e => e.OrpaDtRegIva)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_reg_iva");
            entity.Property(e => e.OrpaDtRevDoc)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_rev_doc");
            entity.Property(e => e.OrpaDtRevLdr)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_rev_ldr");
            entity.Property(e => e.OrpaDtRevT)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_rev_t");
            entity.Property(e => e.OrpaDtRicevDoc)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_ricev_doc");
            entity.Property(e => e.OrpaDtStp)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_stp");
            entity.Property(e => e.OrpaDtUm)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_um");
            entity.Property(e => e.OrpaDtVsRif)
                .HasColumnType("datetime")
                .HasColumnName("orpa_dt_vs_rif");
            entity.Property(e => e.OrpaEspRevDoc)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpa_esp_rev_doc");
            entity.Property(e => e.OrpaEspRevLdr)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpa_esp_rev_ldr");
            entity.Property(e => e.OrpaEspRevT)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpa_esp_rev_t");
            entity.Property(e => e.OrpaFlgAddBolli)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_add_bolli");
            entity.Property(e => e.OrpaFlgAddSbanc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_add_sbanc");
            entity.Property(e => e.OrpaFlgArrotondaFil)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_arrotonda_fil");
            entity.Property(e => e.OrpaFlgAutorizzaFil)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_autorizza_fil");
            entity.Property(e => e.OrpaFlgCee)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_cee");
            entity.Property(e => e.OrpaFlgEscludiCosto)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_escludi_costo");
            entity.Property(e => e.OrpaFlgExpXml)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_exp_xml");
            entity.Property(e => e.OrpaFlgFatt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_fatt");
            entity.Property(e => e.OrpaFlgFuoriBudget)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_fuori_budget");
            entity.Property(e => e.OrpaFlgMagazzino)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_magazzino");
            entity.Property(e => e.OrpaFlgNoBid)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_no_bid");
            entity.Property(e => e.OrpaFlgPagAnticipato)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_pag_anticipato");
            entity.Property(e => e.OrpaFlgProcessato1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_processato_1");
            entity.Property(e => e.OrpaFlgProcessato2)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_processato_2");
            entity.Property(e => e.OrpaFlgRipFatt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_rip_fatt");
            entity.Property(e => e.OrpaFlgSaldoTrg)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_saldo_trg");
            entity.Property(e => e.OrpaFlgStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_flg_status");
            entity.Property(e => e.OrpaGaranzia)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("orpa_garanzia");
            entity.Property(e => e.OrpaGgFranchiEst)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_gg_franchi_est");
            entity.Property(e => e.OrpaGgPenaleEst)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_gg_penale_est");
            entity.Property(e => e.OrpaId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpa_id");
            entity.Property(e => e.OrpaIdArxivar)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("orpa_id_arxivar");
            entity.Property(e => e.OrpaIdCondConsIntra)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("orpa_id_cond_cons_intra");
            entity.Property(e => e.OrpaIdNaturaIntra)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("orpa_id_natura_intra");
            entity.Property(e => e.OrpaIdNazDestIntra)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("orpa_id_naz_dest_intra");
            entity.Property(e => e.OrpaIdNazOrigineIntra)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("orpa_id_naz_origine_intra");
            entity.Property(e => e.OrpaIdRma)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("orpa_id_rma");
            entity.Property(e => e.OrpaIdTrasportoIntra)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("orpa_id_trasporto_intra");
            entity.Property(e => e.OrpaIdValutaIntra)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("orpa_id_valuta_intra");
            entity.Property(e => e.OrpaIndPCap)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("orpa_ind_p_cap");
            entity.Property(e => e.OrpaIndPCitta)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("orpa_ind_p_citta");
            entity.Property(e => e.OrpaIndPNaz)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpa_ind_p_naz");
            entity.Property(e => e.OrpaIndPProv)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("orpa_ind_p_prov");
            entity.Property(e => e.OrpaIndPRagSoc)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("orpa_ind_p_rag_soc");
            entity.Property(e => e.OrpaIndPVia)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("orpa_ind_p_via");
            entity.Property(e => e.OrpaIndSCap)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("orpa_ind_s_cap");
            entity.Property(e => e.OrpaIndSCitta)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("orpa_ind_s_citta");
            entity.Property(e => e.OrpaIndSNaz)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpa_ind_s_naz");
            entity.Property(e => e.OrpaIndSProv)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("orpa_ind_s_prov");
            entity.Property(e => e.OrpaIndSRagSoc)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("orpa_ind_s_rag_soc");
            entity.Property(e => e.OrpaIndSVia)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("orpa_ind_s_via");
            entity.Property(e => e.OrpaIndSpedFa)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_ind_sped_fa");
            entity.Property(e => e.OrpaIndSpedMat)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_ind_sped_mat");
            entity.Property(e => e.OrpaIndiceRevisioneFilemaker)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("orpa_indice_revisione_filemaker");
            entity.Property(e => e.OrpaIntesta)
                .HasMaxLength(600)
                .IsUnicode(false)
                .HasColumnName("orpa_intesta");
            entity.Property(e => e.OrpaM1Currcode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpa_m1_currcode");
            entity.Property(e => e.OrpaM1Currname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpa_m1_currname");
            entity.Property(e => e.OrpaM1Docentry)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpa_m1_docentry");
            entity.Property(e => e.OrpaM1Objtype)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpa_m1_objtype");
            entity.Property(e => e.OrpaM1Project)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpa_m1_project");
            entity.Property(e => e.OrpaM1Warehouse)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("orpa_m1_warehouse");
            entity.Property(e => e.OrpaMarkup)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("orpa_markup");
            entity.Property(e => e.OrpaMezzoArrivoMerce)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("orpa_mezzo_arrivo_merce");
            entity.Property(e => e.OrpaMezzoSped)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("orpa_mezzo_sped");
            entity.Property(e => e.OrpaMm1InvioMassivo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_mm1_invio_massivo");
            entity.Property(e => e.OrpaModErogazioneIntra)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("orpa_mod_erogazione_intra");
            entity.Property(e => e.OrpaModIncassoIntra)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("orpa_mod_incasso_intra");
            entity.Property(e => e.OrpaMtbf)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_mtbf");
            entity.Property(e => e.OrpaN0dIdxge21)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpa_n0d_idxge21");
            entity.Property(e => e.OrpaN0dIdxge22)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpa_n0d_idxge22");
            entity.Property(e => e.OrpaNBancaliMerce)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_n_bancali_merce");
            entity.Property(e => e.OrpaNColli)
                .HasDefaultValueSql("((0))")
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("orpa_n_colli");
            entity.Property(e => e.OrpaNComm)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpa_n_comm");
            entity.Property(e => e.OrpaNDocOrig)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpa_n_doc_orig");
            entity.Property(e => e.OrpaNomeFileXml)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("orpa_nome_file_xml");
            entity.Property(e => e.OrpaNomePc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpa_nome_pc");
            entity.Property(e => e.OrpaNote)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("orpa_note");
            entity.Property(e => e.OrpaNoteAgente)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("orpa_note_agente");
            entity.Property(e => e.OrpaNumeroFattRif)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpa_numero_fatt_rif");
            entity.Property(e => e.OrpaOraIniTr)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("orpa_ora_ini_tr");
            entity.Property(e => e.OrpaPaeseDest)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("orpa_paese_dest");
            entity.Property(e => e.OrpaPaeseIncassoIntra)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpa_paese_incasso_intra");
            entity.Property(e => e.OrpaPagamentoEst)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("orpa_pagamento_est");
            entity.Property(e => e.OrpaPagamentoEstAgg)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("orpa_pagamento_est_agg");
            entity.Property(e => e.OrpaPaklCSrl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpa_pakl_c_srl");
            entity.Property(e => e.OrpaPenaleEst)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("orpa_penale_est");
            entity.Property(e => e.OrpaPerc1)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("orpa_perc_1");
            entity.Property(e => e.OrpaPercSc)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("orpa_perc_sc");
            entity.Property(e => e.OrpaPercSc3)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("orpa_perc_sc_3");
            entity.Property(e => e.OrpaPercScPag)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("orpa_perc_sc_pag");
            entity.Property(e => e.OrpaPercTrasportoIntra)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("orpa_perc_trasporto_intra");
            entity.Property(e => e.OrpaPercorsoNomeFileXml)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("orpa_percorso_nome_file_xml");
            entity.Property(e => e.OrpaPesoLKg)
                .HasDefaultValueSql("((0))")
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("orpa_peso_l_kg");
            entity.Property(e => e.OrpaPesoNKg)
                .HasDefaultValueSql("((0))")
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("orpa_peso_n_kg");
            entity.Property(e => e.OrpaPosComm)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("orpa_pos_comm");
            entity.Property(e => e.OrpaPrfComm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_prf_comm");
            entity.Property(e => e.OrpaPrfDocOrig)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_prf_doc_orig");
            entity.Property(e => e.OrpaProgressivoIndSped)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpa_progressivo_ind_sped");
            entity.Property(e => e.OrpaProvMerce)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("orpa_prov_merce");
            entity.Property(e => e.OrpaProvv1)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("orpa_provv_1");
            entity.Property(e => e.OrpaProvv2)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("orpa_provv_2");
            entity.Property(e => e.OrpaProvv3)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("orpa_provv_3");
            entity.Property(e => e.OrpaQtaTot)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("orpa_qta_tot");
            entity.Property(e => e.OrpaResaEst)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("orpa_resa_est");
            entity.Property(e => e.OrpaRevVsRif)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpa_rev_vs_rif");
            entity.Property(e => e.OrpaScontoIncd)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpa_sconto_incd");
            entity.Property(e => e.OrpaSerialiLight)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_seriali_light");
            entity.Property(e => e.OrpaSpBanc)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpa_sp_banc");
            entity.Property(e => e.OrpaSpBancVi)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpa_sp_banc_vi");
            entity.Property(e => e.OrpaSpImb)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpa_sp_imb");
            entity.Property(e => e.OrpaSpImbVi)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpa_sp_imb_vi");
            entity.Property(e => e.OrpaSpTrasp)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpa_sp_trasp");
            entity.Property(e => e.OrpaSpTraspVi)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpa_sp_trasp_vi");
            entity.Property(e => e.OrpaTDest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_t_dest");
            entity.Property(e => e.OrpaTEmis)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_t_emis");
            entity.Property(e => e.OrpaTFattNota)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_t_fatt_nota");
            entity.Property(e => e.OrpaTInvio)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("orpa_t_invio");
            entity.Property(e => e.OrpaTOrdCli)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("orpa_t_ord_cli");
            entity.Property(e => e.OrpaTSoggDest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_t_sogg_dest");
            entity.Property(e => e.OrpaTSoggProv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_t_sogg_prov");
            entity.Property(e => e.OrpaTcons)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_tcons");
            entity.Property(e => e.OrpaTipcatdoc)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpa_tipcatdoc");
            entity.Property(e => e.OrpaTipoDocXml)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("orpa_tipo_doc_xml");
            entity.Property(e => e.OrpaTipologiaDoc)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpa_tipologia_doc");
            entity.Property(e => e.OrpaTordoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_tordoc");
            entity.Property(e => e.OrpaTsogc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpa_tsogc");
            entity.Property(e => e.OrpaTstComm)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpa_tst_comm");
            entity.Property(e => e.OrpaTstDocOrig)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpa_tst_doc_orig");
            entity.Property(e => e.OrpaUtente1)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orpa_utente_1");
            entity.Property(e => e.OrpaUtente2)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orpa_utente_2");
            entity.Property(e => e.OrpaUtente3)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orpa_utente_3");
            entity.Property(e => e.OrpaUtente4)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orpa_utente_4");
            entity.Property(e => e.OrpaUtente5)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orpa_utente_5");
            entity.Property(e => e.OrpaUtenteAut)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orpa_utente_aut");
            entity.Property(e => e.OrpaUtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orpa_utente_ins");
            entity.Property(e => e.OrpaUtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orpa_utente_um");
            entity.Property(e => e.OrpaValTot)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpa_val_tot");
            entity.Property(e => e.OrpaVettore1)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpa_vettore_1");
            entity.Property(e => e.OrpaVettore2)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpa_vettore_2");
            entity.Property(e => e.OrpaVettore3)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpa_vettore_3");
            entity.Property(e => e.OrpaVsRif)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("orpa_vs_rif");
        });

        modelBuilder.Entity<FlussoOrpb>(entity =>
        {
            entity.HasKey(e => new { e.OrpbTstDoc, e.OrpbPrfDoc, e.OrpbADoc, e.OrpbNDoc, e.OrpbPosDoc, e.OrpbCDitta }).HasName("orpb_chiave");

            entity.ToTable("flusso_orpb", tb =>
            {
                tb.HasTrigger("trg_ss_orpb_del_cdccom");
                tb.HasTrigger("trg_ss_orpb_ins");
                tb.HasTrigger("trg_ss_orpb_mod");
                tb.HasTrigger("trg_ss_orpb_mod_cdccom");
                tb.HasTrigger("trg_ss_orpb_paag_ins");
                tb.HasTrigger("trg_ss_orpb_paag_mod");
            });

            entity.HasIndex(e => new { e.OrpbCPart, e.OrpbTstDoc, e.OrpbPrfDoc, e.OrpbADoc, e.OrpbNDoc, e.OrpbPosDoc }, "orpb_chiave_alt_1").HasFillFactor(90);

            entity.HasIndex(e => new { e.OrpbTstDocOrig, e.OrpbPrfDocOrig, e.OrpbADocOrig, e.OrpbNDocOrig, e.OrpbPosDocOrig, e.OrpbTstDoc, e.OrpbPrfDoc, e.OrpbADoc, e.OrpbNDoc, e.OrpbPosDoc }, "orpb_chiave_alt_2").HasFillFactor(90);

            entity.HasIndex(e => new { e.OrpbTstDocBe, e.OrpbPrfDocBe, e.OrpbADocBe, e.OrpbNDocBe, e.OrpbPosDocBe, e.OrpbCDitta }, "orpb_chiave_alt_3");

            entity.HasIndex(e => new { e.OrpbId, e.OrpbCDitta }, "orpb_chiave_alt_4").IsUnique();

            entity.HasIndex(e => new { e.OrpbCDitta, e.OrpbTstDocOrd, e.OrpbPrfDocOrd, e.OrpbADocOrd, e.OrpbNDocOrd, e.OrpbPosDocOrd }, "orpb_chiave_alt_5");

            entity.Property(e => e.OrpbTstDoc)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpb_tst_doc");
            entity.Property(e => e.OrpbPrfDoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_prf_doc");
            entity.Property(e => e.OrpbADoc)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_a_doc");
            entity.Property(e => e.OrpbNDoc)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpb_n_doc");
            entity.Property(e => e.OrpbPosDoc)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("orpb_pos_doc");
            entity.Property(e => e.OrpbCDitta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpb_c_ditta");
            entity.Property(e => e.OrpbADocBe)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_a_doc_be");
            entity.Property(e => e.OrpbADocDest)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_a_doc_dest");
            entity.Property(e => e.OrpbADocOrd)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_a_doc_ord");
            entity.Property(e => e.OrpbADocOrig)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_a_doc_orig");
            entity.Property(e => e.OrpbAltItem)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpb_alt_item");
            entity.Property(e => e.OrpbCA1)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("orpb_c_a_1");
            entity.Property(e => e.OrpbCA2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orpb_c_a_2");
            entity.Property(e => e.OrpbCA3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orpb_c_a_3");
            entity.Property(e => e.OrpbCA4)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orpb_c_a_4");
            entity.Property(e => e.OrpbCAgente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("orpb_c_agente");
            entity.Property(e => e.OrpbCCertcoll)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpb_c_certcoll");
            entity.Property(e => e.OrpbCCogest)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("orpb_c_cogest");
            entity.Property(e => e.OrpbCCoind)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("orpb_c_coind");
            entity.Property(e => e.OrpbCD1)
                .HasColumnType("datetime")
                .HasColumnName("orpb_c_d_1");
            entity.Property(e => e.OrpbCD2)
                .HasColumnType("datetime")
                .HasColumnName("orpb_c_d_2");
            entity.Property(e => e.OrpbCD3)
                .HasColumnType("datetime")
                .HasColumnName("orpb_c_d_3");
            entity.Property(e => e.OrpbCDest)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("orpb_c_dest");
            entity.Property(e => e.OrpbCDestCliFor)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("orpb_c_dest_cli_for");
            entity.Property(e => e.OrpbCDisegno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orpb_c_disegno");
            entity.Property(e => e.OrpbCDisegno1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orpb_c_disegno_1");
            entity.Property(e => e.OrpbCImb)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_c_imb");
            entity.Property(e => e.OrpbCItem)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("orpb_c_item");
            entity.Property(e => e.OrpbCIva)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("orpb_c_iva");
            entity.Property(e => e.OrpbCLivAz)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("orpb_c_liv_az");
            entity.Property(e => e.OrpbCLive)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_c_live");
            entity.Property(e => e.OrpbCMatricola)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpb_c_matricola");
            entity.Property(e => e.OrpbCN1)
                .HasColumnType("numeric(16, 4)")
                .HasColumnName("orpb_c_n_1");
            entity.Property(e => e.OrpbCN2)
                .HasColumnType("numeric(16, 4)")
                .HasColumnName("orpb_c_n_2");
            entity.Property(e => e.OrpbCN3)
                .HasColumnType("numeric(16, 4)")
                .HasColumnName("orpb_c_n_3");
            entity.Property(e => e.OrpbCN4)
                .HasColumnType("numeric(16, 4)")
                .HasColumnName("orpb_c_n_4");
            entity.Property(e => e.OrpbCNazProvMerce)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpb_c_naz_prov_merce");
            entity.Property(e => e.OrpbCPart)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("orpb_c_part");
            entity.Property(e => e.OrpbCPartAlt)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("orpb_c_part_alt");
            entity.Property(e => e.OrpbCProv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_c_prov");
            entity.Property(e => e.OrpbCTesto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orpb_c_testo");
            entity.Property(e => e.OrpbCTesto1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orpb_c_testo_1");
            entity.Property(e => e.OrpbCVersDb)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("orpb_c_vers_db");
            entity.Property(e => e.OrpbCarea)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_carea");
            entity.Property(e => e.OrpbCig)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("orpb_cig");
            entity.Property(e => e.OrpbConsegna)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("orpb_consegna");
            entity.Property(e => e.OrpbContoCg)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpb_conto_cg");
            entity.Property(e => e.OrpbCostoRif)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_costo_rif");
            entity.Property(e => e.OrpbCup)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("orpb_cup");
            entity.Property(e => e.OrpbDataFattRif)
                .HasColumnType("datetime")
                .HasColumnName("orpb_data_fatt_rif");
            entity.Property(e => e.OrpbDesc)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("orpb_desc");
            entity.Property(e => e.OrpbDim1)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("orpb_dim_1");
            entity.Property(e => e.OrpbDim2)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("orpb_dim_2");
            entity.Property(e => e.OrpbDocEstEv)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpb_doc_est_ev");
            entity.Property(e => e.OrpbDocstatsped)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_docstatsped");
            entity.Property(e => e.OrpbDtFinVal)
                .HasColumnType("datetime")
                .HasColumnName("orpb_dt_fin_val");
            entity.Property(e => e.OrpbDtIniVal)
                .HasColumnType("datetime")
                .HasColumnName("orpb_dt_ini_val");
            entity.Property(e => e.OrpbDtIns)
                .HasColumnType("datetime")
                .HasColumnName("orpb_dt_ins");
            entity.Property(e => e.OrpbDtRevDoc)
                .HasColumnType("datetime")
                .HasColumnName("orpb_dt_rev_doc");
            entity.Property(e => e.OrpbDtSaldo)
                .HasColumnType("datetime")
                .HasColumnName("orpb_dt_saldo");
            entity.Property(e => e.OrpbDtUm)
                .HasColumnType("datetime")
                .HasColumnName("orpb_dt_um");
            entity.Property(e => e.OrpbEspRevDb)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpb_esp_rev_db");
            entity.Property(e => e.OrpbEspRevDoc)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("orpb_esp_rev_doc");
            entity.Property(e => e.OrpbFattConv)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("orpb_fatt_conv");
            entity.Property(e => e.OrpbFlgEmissOrd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_flg_emiss_ord");
            entity.Property(e => e.OrpbFlgFuoriBudget)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_flg_fuori_budget");
            entity.Property(e => e.OrpbFlgGaranzia)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_flg_garanzia");
            entity.Property(e => e.OrpbFlgMrp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_flg_mrp");
            entity.Property(e => e.OrpbFlgPakl)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_flg_pakl");
            entity.Property(e => e.OrpbFlgPrezzoUm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_flg_prezzo_um");
            entity.Property(e => e.OrpbFlgQc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_flg_qc");
            entity.Property(e => e.OrpbFlgSaldo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_flg_saldo");
            entity.Property(e => e.OrpbFlgStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_flg_status");
            entity.Property(e => e.OrpbFlgUm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_flg_um");
            entity.Property(e => e.OrpbId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpb_id");
            entity.Property(e => e.OrpbIdfilemaker)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpb_idfilemaker");
            entity.Property(e => e.OrpbImballoEst)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("orpb_imballo_est");
            entity.Property(e => e.OrpbImportoOmaggiLi)
                .HasColumnType("numeric(16, 4)")
                .HasColumnName("orpb_importo_omaggi_li");
            entity.Property(e => e.OrpbImportoOmaggiPr)
                .HasColumnType("numeric(16, 4)")
                .HasColumnName("orpb_importo_omaggi_pr");
            entity.Property(e => e.OrpbItemCli)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpb_item_cli");
            entity.Property(e => e.OrpbLeadtFix)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("orpb_leadt_fix");
            entity.Property(e => e.OrpbM1ACommRif)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_m1_a_comm_rif");
            entity.Property(e => e.OrpbM1CMag)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_m1_c_mag");
            entity.Property(e => e.OrpbM1Docentry)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpb_m1_docentry");
            entity.Property(e => e.OrpbM1Linenum)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpb_m1_linenum");
            entity.Property(e => e.OrpbM1Molo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("orpb_m1_molo");
            entity.Property(e => e.OrpbM1NCommRif)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpb_m1_n_comm_rif");
            entity.Property(e => e.OrpbM1NuovaRev)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("orpb_m1_nuova_rev");
            entity.Property(e => e.OrpbM1Objtype)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpb_m1_objtype");
            entity.Property(e => e.OrpbM1OrdinatoreDoc)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("orpb_m1_ordinatore_doc");
            entity.Property(e => e.OrpbM1PrfCommRif)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_m1_prf_comm_rif");
            entity.Property(e => e.OrpbM1Project)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpb_m1_project");
            entity.Property(e => e.OrpbM1Stabilimento)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("orpb_m1_stabilimento");
            entity.Property(e => e.OrpbM1Treetype)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_m1_treetype");
            entity.Property(e => e.OrpbM1TstCommRif)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpb_m1_tst_comm_rif");
            entity.Property(e => e.OrpbM1Warehouse)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("orpb_m1_warehouse");
            entity.Property(e => e.OrpbMarkup)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("orpb_markup");
            entity.Property(e => e.OrpbMatricolaIniziale)
                .HasColumnType("numeric(16, 0)")
                .HasColumnName("orpb_matricola_iniziale");
            entity.Property(e => e.OrpbN0dIdxbg02)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpb_n0d_idxbg02");
            entity.Property(e => e.OrpbNCiclo)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpb_n_ciclo");
            entity.Property(e => e.OrpbNDocBe)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpb_n_doc_be");
            entity.Property(e => e.OrpbNDocDest)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpb_n_doc_dest");
            entity.Property(e => e.OrpbNDocOrd)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpb_n_doc_ord");
            entity.Property(e => e.OrpbNDocOrig)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("orpb_n_doc_orig");
            entity.Property(e => e.OrpbNOperSo)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_n_oper_so");
            entity.Property(e => e.OrpbNRigaDettaglioXml)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("orpb_n_riga_dettaglio_xml");
            entity.Property(e => e.OrpbNomePc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpb_nome_pc");
            entity.Property(e => e.OrpbNote)
                .HasMaxLength(4000)
                .IsUnicode(false)
                .HasColumnName("orpb_note");
            entity.Property(e => e.OrpbNote2)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("orpb_note_2");
            entity.Property(e => e.OrpbNumeroFattRif)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orpb_numero_fatt_rif");
            entity.Property(e => e.OrpbPaklCSrl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("orpb_pakl_c_srl");
            entity.Property(e => e.OrpbPercIva)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("orpb_perc_iva");
            entity.Property(e => e.OrpbPercProvv)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("orpb_perc_provv");
            entity.Property(e => e.OrpbPercSc)
                .HasColumnType("numeric(13, 10)")
                .HasColumnName("orpb_perc_sc");
            entity.Property(e => e.OrpbPercSc1)
                .HasColumnType("numeric(13, 10)")
                .HasColumnName("orpb_perc_sc_1");
            entity.Property(e => e.OrpbPercSc2)
                .HasColumnType("numeric(13, 10)")
                .HasColumnName("orpb_perc_sc_2");
            entity.Property(e => e.OrpbPercSc3)
                .HasColumnType("numeric(13, 10)")
                .HasColumnName("orpb_perc_sc_3");
            entity.Property(e => e.OrpbPosDocBe)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("orpb_pos_doc_be");
            entity.Property(e => e.OrpbPosDocDest)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("orpb_pos_doc_dest");
            entity.Property(e => e.OrpbPosDocOrd)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("orpb_pos_doc_ord");
            entity.Property(e => e.OrpbPosDocOrig)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("orpb_pos_doc_orig");
            entity.Property(e => e.OrpbPrfDocBe)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_prf_doc_be");
            entity.Property(e => e.OrpbPrfDocDest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_prf_doc_dest");
            entity.Property(e => e.OrpbPrfDocOrd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_prf_doc_ord");
            entity.Property(e => e.OrpbPrfDocOrig)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_prf_doc_orig");
            entity.Property(e => e.OrpbPrgDocMin)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("orpb_prg_doc_min");
            entity.Property(e => e.OrpbPrgDocOrig)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("orpb_prg_doc_orig");
            entity.Property(e => e.OrpbProvv1)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("orpb_provv_1");
            entity.Property(e => e.OrpbProvv2)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("orpb_provv_2");
            entity.Property(e => e.OrpbProvv3)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("orpb_provv_3");
            entity.Property(e => e.OrpbProvvigioniMod)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_provvigioni_mod");
            entity.Property(e => e.OrpbQtaEvasaTrg)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("orpb_qta_evasa_trg");
            entity.Property(e => e.OrpbQtaEvasaUmaTrg)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("orpb_qta_evasa_uma_trg");
            entity.Property(e => e.OrpbQtaInEtichetta)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("orpb_qta_in_etichetta");
            entity.Property(e => e.OrpbQtaInevasaTrg)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("orpb_qta_inevasa_trg");
            entity.Property(e => e.OrpbQtaInevasaUmaTrg)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("orpb_qta_inevasa_uma_trg");
            entity.Property(e => e.OrpbQtaTotaleTrg)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("orpb_qta_totale_trg");
            entity.Property(e => e.OrpbQtaTotaleUmaTrg)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("orpb_qta_totale_uma_trg");
            entity.Property(e => e.OrpbRangeMatricole)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("orpb_range_matricole");
            entity.Property(e => e.OrpbRmaAccountproductid)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("orpb_rma_accountproductid");
            entity.Property(e => e.OrpbStatogg)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("orpb_statogg");
            entity.Property(e => e.OrpbTDest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_t_dest");
            entity.Property(e => e.OrpbTFattConv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_t_fatt_conv");
            entity.Property(e => e.OrpbTOper)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("orpb_t_oper");
            entity.Property(e => e.OrpbTPrzBase)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("orpb_t_prz_base");
            entity.Property(e => e.OrpbTRiga)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("orpb_t_riga");
            entity.Property(e => e.OrpbTstDocBe)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpb_tst_doc_be");
            entity.Property(e => e.OrpbTstDocDest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpb_tst_doc_dest");
            entity.Property(e => e.OrpbTstDocOrd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpb_tst_doc_ord");
            entity.Property(e => e.OrpbTstDocOrig)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpb_tst_doc_orig");
            entity.Property(e => e.OrpbUm)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpb_um");
            entity.Property(e => e.OrpbUmAlt)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("orpb_um_alt");
            entity.Property(e => e.OrpbUtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orpb_utente_ins");
            entity.Property(e => e.OrpbUtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orpb_utente_um");
            entity.Property(e => e.OrpbVestLUniLi)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vest_l_uni_li");
            entity.Property(e => e.OrpbVestLUniPr)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vest_l_uni_pr");
            entity.Property(e => e.OrpbVestTotLi)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpb_vest_tot_li");
            entity.Property(e => e.OrpbVestTotPr)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpb_vest_tot_pr");
            entity.Property(e => e.OrpbVestUniLi)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vest_uni_li");
            entity.Property(e => e.OrpbVestUniPr)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vest_uni_pr");
            entity.Property(e => e.OrpbVintTotLi)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpb_vint_tot_li");
            entity.Property(e => e.OrpbVintTotPr)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpb_vint_tot_pr");
            entity.Property(e => e.OrpbVintUniLi)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vint_uni_li");
            entity.Property(e => e.OrpbVintUniPr)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vint_uni_pr");
            entity.Property(e => e.OrpbVmatLUniLAlt)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vmat_l_uni_l_alt");
            entity.Property(e => e.OrpbVmatLUniLi)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vmat_l_uni_li");
            entity.Property(e => e.OrpbVmatLUniPr)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vmat_l_uni_pr");
            entity.Property(e => e.OrpbVmatLUniPrAlt)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vmat_l_uni_pr_alt");
            entity.Property(e => e.OrpbVmatTotLi)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpb_vmat_tot_li");
            entity.Property(e => e.OrpbVmatTotPr)
                .HasColumnType("numeric(18, 5)")
                .HasColumnName("orpb_vmat_tot_pr");
            entity.Property(e => e.OrpbVmatUniLi)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vmat_uni_li");
            entity.Property(e => e.OrpbVmatUniLiAlt)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vmat_uni_li_alt");
            entity.Property(e => e.OrpbVmatUniPr)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vmat_uni_pr");
            entity.Property(e => e.OrpbVmatUniPrAlt)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vmat_uni_pr_alt");
            entity.Property(e => e.OrpbVmatUniPrMarkup)
                .HasColumnType("numeric(18, 6)")
                .HasColumnName("orpb_vmat_uni_pr_markup");
            entity.Property(e => e.OrpbVsRif)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("orpb_vs_rif");
            entity.Property(e => e.OrpbVsRif2)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("orpb_vs_rif_2");
            entity.Property(e => e.OrpbVsRif3)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("orpb_vs_rif_3");
        });

        modelBuilder.Entity<FlussoUsr1>(entity =>
        {
            entity.HasKey(e => new { e.Usr1CSrl, e.Usr1CDitta }).HasName("usr1_chiave");

            entity.ToTable("flusso_usr1", tb =>
            {
                tb.HasTrigger("trg_ss_usr1_gdpr_del");
                tb.HasTrigger("trg_ss_usr1_gdpr_ins");
                tb.HasTrigger("trg_ss_usr1_gdpr_mod");
            });

            entity.HasIndex(e => new { e.Usr1CUtente, e.Usr1CDitta }, "usr1_chiave_alt_1").IsUnique();

            entity.HasIndex(e => new { e.Usr1Username, e.Usr1CDitta }, "usr1_chiave_alt_2").IsUnique();

            entity.Property(e => e.Usr1CSrl)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("usr1_c_srl");
            entity.Property(e => e.Usr1CDitta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("usr1_c_ditta");
            entity.Property(e => e.Usr1AttivaCockpit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("usr1_attiva_cockpit");
            entity.Property(e => e.Usr1Backcolor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("usr1_backcolor");
            entity.Property(e => e.Usr1CLingua)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("usr1_c_lingua");
            entity.Property(e => e.Usr1CRisorsa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("usr1_c_risorsa");
            entity.Property(e => e.Usr1CUtente)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("usr1_c_utente");
            entity.Property(e => e.Usr1Ctrl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("usr1_ctrl");
            entity.Property(e => e.Usr1Desc)
                .HasMaxLength(160)
                .IsUnicode(false)
                .HasColumnName("usr1_desc");
            entity.Property(e => e.Usr1DtIns)
                .HasColumnType("datetime")
                .HasColumnName("usr1_dt_ins");
            entity.Property(e => e.Usr1DtRifPassw)
                .HasColumnType("datetime")
                .HasColumnName("usr1_dt_rif_passw");
            entity.Property(e => e.Usr1DtUm)
                .HasColumnType("datetime")
                .HasColumnName("usr1_dt_um");
            entity.Property(e => e.Usr1EMail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usr1_e_mail");
            entity.Property(e => e.Usr1FirmaEmail)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("usr1_firma_email");
            entity.Property(e => e.Usr1FlgCessato)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("usr1_flg_cessato");
            entity.Property(e => e.Usr1FlgEtichetteEstere)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("usr1_flg_etichette_estere");
            entity.Property(e => e.Usr1FlgWauth)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("usr1_flg_wauth");
            entity.Property(e => e.Usr1GestEmail)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .HasColumnName("usr1_gest_email");
            entity.Property(e => e.Usr1GgModPassw)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("usr1_gg_mod_passw");
            entity.Property(e => e.Usr1IdSicurezza)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("usr1_id_sicurezza");
            entity.Property(e => e.Usr1ImgMdi)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("usr1_img_mdi");
            entity.Property(e => e.Usr1ImgUtente)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("usr1_img_utente");
            entity.Property(e => e.Usr1InfoRelease)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("usr1_info_release");
            entity.Property(e => e.Usr1MdiBackcolor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("usr1_mdi_backcolor");
            entity.Property(e => e.Usr1MmTimeout)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("usr1_mm_timeout");
            entity.Property(e => e.Usr1NFax)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("usr1_n_fax");
            entity.Property(e => e.Usr1NTel)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("usr1_n_tel");
            entity.Property(e => e.Usr1ParametriWeb)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("usr1_parametri_web");
            entity.Property(e => e.Usr1Password)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("usr1_password");
            entity.Property(e => e.Usr1PersonalSettings)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("usr1_personal_settings");
            entity.Property(e => e.Usr1PwdManagerM1)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("usr1_pwd_manager_m1");
            entity.Property(e => e.Usr1TAccesso)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("usr1_t_accesso");
            entity.Property(e => e.Usr1TUtente)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("usr1_t_utente");
            entity.Property(e => e.Usr1Theme)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("usr1_theme");
            entity.Property(e => e.Usr1Token)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("usr1_token");
            entity.Property(e => e.Usr1UserManagerM1)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("usr1_user_manager_m1");
            entity.Property(e => e.Usr1Username)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("usr1_username");
            entity.Property(e => e.Usr1UtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("usr1_utente_ins");
            entity.Property(e => e.Usr1UtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("usr1_utente_um");
            entity.Property(e => e.Usr1WindowsUser)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("usr1_windows_user");
        });


        modelBuilder.Entity<PscCo01>(entity =>
        {
            entity.HasKey(e => new { e.CDitta, e.IdDoc, e.Grpcdl }).HasName("PSC_CO01_KEY");

            entity.ToTable("PSC_CO01", "dba");

            entity.Property(e => e.CDitta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("C_DITTA");
            entity.Property(e => e.IdDoc)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_DOC");
            entity.Property(e => e.Grpcdl)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("GRPCDL");
            entity.Property(e => e.DtIns)
                .HasColumnType("datetime")
                .HasColumnName("DT_INS");
            entity.Property(e => e.DtUm)
                .HasColumnType("datetime")
                .HasColumnName("DT_UM");
            entity.Property(e => e.HhAcq)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("HH_ACQ");
            entity.Property(e => e.Note)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("NOTE");
            entity.Property(e => e.TFatt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("T_FATT");
            entity.Property(e => e.UtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("UTENTE_INS");
            entity.Property(e => e.UtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("UTENTE_UM");
        });

        modelBuilder.Entity<PscCo02>(entity =>
        {
            entity.HasKey(e => new { e.CDitta, e.IdDoc, e.CRisorsa }).HasName("PSC_CO02_KEY");

            entity.ToTable("PSC_CO02", "dba");

            entity.Property(e => e.CDitta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("C_DITTA");
            entity.Property(e => e.IdDoc)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_DOC");
            entity.Property(e => e.CRisorsa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("C_RISORSA");
            entity.Property(e => e.DtIns)
                .HasColumnType("datetime")
                .HasColumnName("DT_INS");
            entity.Property(e => e.DtUm)
                .HasColumnType("datetime")
                .HasColumnName("DT_UM");
            entity.Property(e => e.Grpcdl)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("GRPCDL");
            entity.Property(e => e.UtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("UTENTE_INS");
            entity.Property(e => e.UtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("UTENTE_UM");
        });

        modelBuilder.Entity<PscCo03>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CDitta }).HasName("PSC_CO03_KEY");

            entity.ToTable("PSC_CO03", "dba");

            entity.HasIndex(e => new { e.CDitta, e.IdDoc, e.Grpcdl }, "PSC_CO03_KEY_ALT_2");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.CDitta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("C_DITTA");
            entity.Property(e => e.DtIns)
                .HasColumnType("datetime")
                .HasColumnName("DT_INS");
            entity.Property(e => e.DtRicarica)
                .HasColumnType("datetime")
                .HasColumnName("DT_RICARICA");
            entity.Property(e => e.DtUm)
                .HasColumnType("datetime")
                .HasColumnName("DT_UM");
            entity.Property(e => e.Grpcdl)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("GRPCDL");
            entity.Property(e => e.HhAcq)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("HH_ACQ");
            entity.Property(e => e.IdDoc)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_DOC");
            entity.Property(e => e.Note)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("NOTE");
            entity.Property(e => e.RifOfferta)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("RIF_OFFERTA");
            entity.Property(e => e.UtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("UTENTE_INS");
            entity.Property(e => e.UtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("UTENTE_UM");

            entity.HasOne(d => d.PscCo01).WithMany(p => p.PscCo03s)
                .HasForeignKey(d => new { d.CDitta, d.IdDoc, d.Grpcdl })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PSC_CO03_FK_01");
        });


        //VISTE

        modelBuilder.Entity<Mvxpa01>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("mvxpa01");

			entity.Property(e => e.Id)
			    .HasColumnType("numeric(12, 0)")
				.HasColumnName("id");

			entity.Property(e => e.Cmaatt)
				.HasColumnName("cmaatt");

			entity.Property(e => e.Lingua)
			    .HasColumnType("numeric(12, 0)")
				.HasColumnName("lingua");                

			entity.Property(e => e.DescrizioneRid)
				.HasColumnName("descrizione_rid");
			entity.Property(e => e.Descrizione)
				.HasColumnName("descrizione");
			entity.Property(e => e.Note)
				.HasColumnName("note");

			entity.Property(e => e.Cditta)
				.HasColumnName("cditta");

			entity.Property(e => e.CtabellaOr)
				.HasColumnName("ctabella_or");

			entity.Property(e => e.Setcpaag)
				.HasColumnName("setcpaag");

			entity.Property(e => e.UtenteIns)
				.HasColumnName("utente_ins");
			entity.Property(e => e.DtIns)
				.HasColumnType("datetime")
				.HasColumnName("dt_ins");

			entity.Property(e => e.UtenteUm)
				.HasColumnName("utente_um");
			entity.Property(e => e.DtUm)
				.HasColumnName("dt_um");

			entity.Property(e => e.Cord)
				.HasColumnName("cord");

		});


		modelBuilder.Entity<VsPpMonitorIsl>(entity =>
		{
			entity
				.HasNoKey()
				.ToView("vs_pp_monitor_ISL", "dba");

			entity.Property(e => e.AcliRagSoc1)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("acli_rag_soc_1");
			entity.Property(e => e.CambioStato).HasMaxLength(4000);
			entity.Property(e => e.CdlAnfu)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("cdl_anfu");
			entity.Property(e => e.CdlDeli)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("cdl_deli");
			entity.Property(e => e.CdlProg)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("cdl_prog");
			entity.Property(e => e.CldSvil)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("cld_svil");
			entity.Property(e => e.Conf)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("conf");
			entity.Property(e => e.Criticita)
				.HasMaxLength(20)
				.IsUnicode(false)
				.HasColumnName("criticita");
			entity.Property(e => e.CurrCdl)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("curr_cdl");
			entity.Property(e => e.CurrDt)
				.HasMaxLength(4000)
				.HasColumnName("curr_dt");
			entity.Property(e => e.DtAnfu)
				.HasMaxLength(4000)
				.HasColumnName("dt_anfu");
			entity.Property(e => e.DtDeli)
				.HasMaxLength(4000)
				.HasColumnName("dt_deli");
			entity.Property(e => e.DtDeliOrig)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("dt_deli_orig");
			entity.Property(e => e.DtIns)
				.HasMaxLength(4000)
				.HasColumnName("dt_ins");
			entity.Property(e => e.DtPian1)
				.HasMaxLength(4000)
				.HasColumnName("dt_pian_1");
			entity.Property(e => e.DtReg)
				.HasMaxLength(4000)
				.HasColumnName("dt_reg");
			entity.Property(e => e.DtRic)
				.HasMaxLength(4000)
				.HasColumnName("dt_ric");
			entity.Property(e => e.DtSvil)
				.HasMaxLength(4000)
				.HasColumnName("dt_svil");
			entity.Property(e => e.DtSvilOri)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("dt_svil_ori");
			entity.Property(e => e.DtValiLast)
				.HasMaxLength(4000)
				.HasColumnName("dt_vali_last");
			entity.Property(e => e.DtVeri1)
				.HasMaxLength(4000)
				.HasColumnName("dt_veri_1");
			entity.Property(e => e.DtVeriLast)
				.HasMaxLength(4000)
				.HasColumnName("dt_veri_last");
			entity.Property(e => e.Flag)
				.HasMaxLength(7)
				.IsUnicode(false)
				.HasColumnName("flag");
			entity.Property(e => e.GgDeli)
				.HasColumnType("numeric(6, 2)")
				.HasColumnName("gg_deli");
			entity.Property(e => e.GgDeliDone)
				.HasColumnType("numeric(38, 6)")
				.HasColumnName("gg_deli_done");
			entity.Property(e => e.GgDeliRes)
				.HasColumnType("numeric(6, 2)")
				.HasColumnName("gg_deli_res");
			entity.Property(e => e.GgSvil)
				.HasColumnType("numeric(6, 2)")
				.HasColumnName("gg_svil");
			entity.Property(e => e.GgSvilDone)
				.HasColumnType("numeric(38, 6)")
				.HasColumnName("gg_svil_done");
			entity.Property(e => e.GgSvilRes)
				.HasColumnType("numeric(6, 2)")
				.HasColumnName("gg_svil_res");
			entity.Property(e => e.RifCli)
				.HasMaxLength(300)
				.IsUnicode(false);
			entity.Property(e => e.Stato)
				.HasMaxLength(20)
				.IsUnicode(false);
			entity.Property(e => e.TatvCCli)
				.HasMaxLength(8)
				.IsUnicode(false)
				.HasColumnName("tatv_c_cli");
			entity.Property(e => e.TatvCPart)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("tatv_c_part");
			entity.Property(e => e.TatvCPartApp)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("tatv_c_part_app");
			entity.Property(e => e.TatvDesc)
				.HasMaxLength(8000)
				.IsUnicode(false)
				.HasColumnName("tatv_desc");
			entity.Property(e => e.TatvNote)
				.HasMaxLength(8000)
				.IsUnicode(false)
				.HasColumnName("tatv_note");
			entity.Property(e => e.TbcpAComm)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tbcp_a_comm");
			entity.Property(e => e.TbcpCCli)
				.HasMaxLength(8)
				.IsUnicode(false)
				.HasColumnName("tbcp_c_cli");
			entity.Property(e => e.TbcpDesc)
				.HasMaxLength(2000)
				.IsUnicode(false)
				.HasColumnName("tbcp_desc");
			entity.Property(e => e.TbcpNComm)
				.HasColumnType("numeric(8, 0)")
				.HasColumnName("tbcp_n_comm");
			entity.Property(e => e.TbcpPrfComm)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tbcp_prf_comm");
			entity.Property(e => e.TbcpTstComm)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("tbcp_tst_comm");
			entity.Property(e => e.TestFittizio)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("test_fittizio");
			entity.Property(e => e.Tipo)
				.HasMaxLength(3)
				.IsUnicode(false);
			entity.Property(e => e.Vinc)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("vinc");
            entity.Property(e => e.TatvFlgOfferta)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tatv_flg_offerta");
		});


		modelBuilder.Entity<Mvxzz12>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("mvxzz12");

            entity.Property(e => e.Cditta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("cditta");
            entity.Property(e => e.Cod)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("cod");
            entity.Property(e => e.Cprfc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cprfc");
            entity.Property(e => e.CtabellaOr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ctabella_or");
            entity.Property(e => e.Descrizione)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("descrizione");
            entity.Property(e => e.DescrizioneRidotta)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("descrizione_ridotta");
            entity.Property(e => e.DtIns)
                .HasColumnType("datetime")
                .HasColumnName("dt_ins");
            entity.Property(e => e.DtUm)
                .HasColumnType("datetime")
                .HasColumnName("dt_um");
            entity.Property(e => e.Id)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("id");
            entity.Property(e => e.Lingua)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("lingua");
            entity.Property(e => e.Note)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.UtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("utente_ins");
            entity.Property(e => e.UtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("utente_um");
        });
        modelBuilder.Entity<FlussoMacc>(entity =>
        {
            entity.HasKey(e => new { e.MaccTipo, e.MaccCMatricola, e.MaccCDitta }).HasName("macc_chiave");

            entity.ToTable("flusso_macc", "dba", tb =>
            {
                tb.HasTrigger("t_flusso_macc_tcdl_ad");
                tb.HasTrigger("t_flusso_macc_tcdl_ai");
                tb.HasTrigger("t_flusso_macc_tcdl_au");
                tb.HasTrigger("trg_ss_macc_gdpr_del");
                tb.HasTrigger("trg_ss_macc_gdpr_ins");
                tb.HasTrigger("trg_ss_macc_gdpr_mod");
            });

            entity.HasIndex(e => new { e.MaccCCdl, e.MaccTipo, e.MaccCMatricola }, "macc_chiave_alt_1").HasFillFactor(90);

            entity.HasIndex(e => new { e.MaccId, e.MaccCDitta }, "macc_chiave_alt_2");

            entity.Property(e => e.MaccTipo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("macc_tipo");
            entity.Property(e => e.MaccCMatricola)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("macc_c_matricola");
            entity.Property(e => e.MaccCDitta)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("macc_c_ditta");
            entity.Property(e => e.MaccCCdl)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("macc_c_cdl");
            entity.Property(e => e.MaccDesc)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("macc_desc");
            entity.Property(e => e.MaccDtIns)
                .HasColumnType("datetime")
                .HasColumnName("macc_dt_ins");
            entity.Property(e => e.MaccDtUm)
                .HasColumnType("datetime")
                .HasColumnName("macc_dt_um");
            entity.Property(e => e.MaccFlgAttivo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("macc_flg_attivo");
            entity.Property(e => e.MaccFlgComdir)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("macc_flg_comdir");
            entity.Property(e => e.MaccFlgGrp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("macc_flg_grp");
            entity.Property(e => e.MaccFlgMag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("macc_flg_mag");
            entity.Property(e => e.MaccFlgPresidio)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("macc_flg_presidio");
            entity.Property(e => e.MaccGrpCosto)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("macc_grp_costo");
            entity.Property(e => e.MaccId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("macc_id");
            entity.Property(e => e.MaccListaCaus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("macc_lista_caus");
            entity.Property(e => e.MaccNRis)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("macc_n_ris");
            entity.Property(e => e.MaccPassw)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("macc_passw");
            entity.Property(e => e.MaccPathParprogramDes)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("macc_path_parprogram_des");
            entity.Property(e => e.MaccPathParprogramSrc)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("macc_path_parprogram_src");
            entity.Property(e => e.MaccPortata)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("macc_portata");
            entity.Property(e => e.MaccStris)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("macc_stris");
            entity.Property(e => e.MaccTprocpers)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("macc_tprocpers");
            entity.Property(e => e.MaccUtenteIns)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("macc_utente_ins");
            entity.Property(e => e.MaccUtenteUm)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("macc_utente_um");
        });


        modelBuilder.Entity<VsConsXComm>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vs_cons_x_comm");

            entity.Property(e => e.AcliRagSoc1)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("acli_rag_soc_1");
            entity.Property(e => e.Descrizione)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("descrizione");
            entity.Property(e => e.DescrizioneRidotta)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("descrizione_ridotta");
            entity.Property(e => e.Hh001aBuc)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("HH001A_BUC");
            entity.Property(e => e.Hh001aPgm)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("HH001A_PGM");
            entity.Property(e => e.Hh001aPjm)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("HH001A_PJM");
            entity.Property(e => e.Hh001aSoa)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("HH001A_SOA");
            entity.Property(e => e.Hh001aSyd)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("HH001A_SYD");
            entity.Property(e => e.HhacqBuc)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("HHACQ_BUC");
            entity.Property(e => e.HhacqGde)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("HHACQ_GDE");
            entity.Property(e => e.HhacqGen)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("HHACQ_GEN");
            entity.Property(e => e.HhacqPgm)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("HHACQ_PGM");
            entity.Property(e => e.HhacqPjm)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("HHACQ_PJM");
            entity.Property(e => e.HhacqSoa)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("HHACQ_SOA");
            entity.Property(e => e.HhacqSyd)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("HHACQ_SYD");
            entity.Property(e => e.HhcrrgBuc)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_BUC");
            entity.Property(e => e.HhcrrgBucEff)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_BUC_EFF");
            entity.Property(e => e.HhcrrgBucEffNv)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_BUC_EFF_NV");
            entity.Property(e => e.HhcrrgGen)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_GEN");
            entity.Property(e => e.HhcrrgPgm)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_PGM");
            entity.Property(e => e.HhcrrgPgmEff)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_PGM_EFF");
            entity.Property(e => e.HhcrrgPgmEffNv)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_PGM_EFF_NV");
            entity.Property(e => e.HhcrrgPjm)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_PJM");
            entity.Property(e => e.HhcrrgPjmEff)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_PJM_EFF");
            entity.Property(e => e.HhcrrgPjmEffNv)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_PJM_EFF_NV");
            entity.Property(e => e.HhcrrgSoa)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_SOA");
            entity.Property(e => e.HhcrrgSoaEff)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_SOA_EFF");
            entity.Property(e => e.HhcrrgSoaEffNv)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_SOA_EFF_NV");
            entity.Property(e => e.HhcrrgSyd)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_SYD");
            entity.Property(e => e.HhcrrgSydEff)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_SYD_EFF");
            entity.Property(e => e.HhcrrgSydEffNv)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("HHCRRG_SYD_EFF_NV");
            entity.Property(e => e.TbcpAComm)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_a_comm");
            entity.Property(e => e.TbcpCCli)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("tbcp_c_cli");
            entity.Property(e => e.TbcpCarea)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("tbcp_carea");
            entity.Property(e => e.TbcpDesc)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("tbcp_desc");
            entity.Property(e => e.TbcpId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("tbcp_id");
            entity.Property(e => e.TbcpM1Project)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tbcp_m1_project");
            entity.Property(e => e.TbcpNComm)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("tbcp_n_comm");
            entity.Property(e => e.TbcpOffPrev)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tbcp_off_prev");
            entity.Property(e => e.TbcpPrfComm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_prf_comm");
            entity.Property(e => e.TbcpProjectManager)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("tbcp_project_manager");
            entity.Property(e => e.TbcpRifCliente)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tbcp_rif_cliente");
            entity.Property(e => e.TbcpStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tbcp_status");
            entity.Property(e => e.TbcpTstComm)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("tbcp_tst_comm");
            entity.Property(e => e.TfattBuc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("TFATT_BUC");
            entity.Property(e => e.TfattGde)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("TFATT_GDE");
            entity.Property(e => e.TfattGen)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("TFATT_GEN");
            entity.Property(e => e.TfattPgm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("TFATT_PGM");
            entity.Property(e => e.TfattPjm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("TFATT_PJM");
            entity.Property(e => e.TfattSoa)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("TFATT_SOA");
            entity.Property(e => e.TfattSyd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("TFATT_SYD");
            entity.Property(e => e.Usr1Desc)
                .HasMaxLength(160)
                .IsUnicode(false)
                .HasColumnName("usr1_desc");
        });

        modelBuilder.Entity<FlussoTcdl>(entity =>
		{
			entity.HasKey(e => new { e.TcdlCCdl, e.TcdlCDitta }).HasName("tcdl_chiave");

			entity.ToTable("flusso_tcdl", tb =>
			{
				tb.HasTrigger("t_flusso_tcdl_macc_ad");
				tb.HasTrigger("t_flusso_tcdl_macc_ai");
				tb.HasTrigger("t_flusso_tcdl_macc_au");
			});

			entity.Property(e => e.TcdlCCdl)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("tcdl_c_cdl");
			entity.Property(e => e.TcdlCDitta)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("tcdl_c_ditta");
			entity.Property(e => e.TcdlCCale)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("tcdl_c_cale");
			entity.Property(e => e.TcdlCCdc)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("tcdl_c_cdc");
			entity.Property(e => e.TcdlCGruppoSched)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("tcdl_c_gruppo_sched");
			entity.Property(e => e.TcdlCLivP)
				.HasMaxLength(12)
				.IsUnicode(false)
				.HasColumnName("tcdl_c_liv_p");
			entity.Property(e => e.TcdlCMagPrel)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tcdl_c_mag_prel");
			entity.Property(e => e.TcdlCMagVers)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tcdl_c_mag_vers");
			entity.Property(e => e.TcdlCProp)
				.HasMaxLength(8)
				.IsUnicode(false)
				.HasColumnName("tcdl_c_prop");
			entity.Property(e => e.TcdlCalgassris)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tcdl_calgassris");
			entity.Property(e => e.TcdlCalgschris)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tcdl_calgschris");
			entity.Property(e => e.TcdlColor)
				.HasColumnType("numeric(8, 0)")
				.HasColumnName("tcdl_color");
			entity.Property(e => e.TcdlDesc)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("tcdl_desc");
			entity.Property(e => e.TcdlDtIns)
				.HasColumnType("datetime")
				.HasColumnName("tcdl_dt_ins");
			entity.Property(e => e.TcdlDtUltimaSched)
				.HasColumnType("datetime")
				.HasColumnName("tcdl_dt_ultima_sched");
			entity.Property(e => e.TcdlDtUm)
				.HasColumnType("datetime")
				.HasColumnName("tcdl_dt_um");
			entity.Property(e => e.TcdlFlgDett)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tcdl_flg_dett");
			entity.Property(e => e.TcdlFlgIe)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tcdl_flg_ie");
			entity.Property(e => e.TcdlFlgSched)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tcdl_flg_sched");
			entity.Property(e => e.TcdlFlgSchedMano)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tcdl_flg_sched_mano");
			entity.Property(e => e.TcdlGrpcdl)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tcdl_grpcdl");
			entity.Property(e => e.TcdlHhMedMese)
				.HasColumnType("numeric(10, 2)")
				.HasColumnName("tcdl_hh_med_mese");
			entity.Property(e => e.TcdlHhPrevMese)
				.HasColumnType("numeric(10, 2)")
				.HasColumnName("tcdl_hh_prev_mese");
			entity.Property(e => e.TcdlHhTurno)
				.HasColumnType("numeric(4, 2)")
				.HasColumnName("tcdl_hh_turno");
			entity.Property(e => e.TcdlId)
				.ValueGeneratedOnAdd()
				.HasColumnType("numeric(12, 0)")
				.HasColumnName("tcdl_id");
			entity.Property(e => e.TcdlNEntProd)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("tcdl_n_ent_prod");
			entity.Property(e => e.TcdlPercEff)
				.HasColumnType("numeric(5, 2)")
				.HasColumnName("tcdl_perc_eff");
			entity.Property(e => e.TcdlTDomina)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tcdl_t_domina");
			entity.Property(e => e.TcdlTProp)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("tcdl_t_prop");
			entity.Property(e => e.TcdlTmAggSec)
				.HasColumnType("numeric(10, 0)")
				.HasColumnName("tcdl_tm_agg_sec");
			entity.Property(e => e.TcdlUtenteIns)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("tcdl_utente_ins");
			entity.Property(e => e.TcdlUtenteUm)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("tcdl_utente_um");
		});

		modelBuilder.Entity<PscQual>(entity =>
		{
			entity.HasKey(e => new { e.TSoggetto, e.CSoggetto, e.TindProgressivo, e.CRisorsa, e.CDitta }).HasName("PSC_QUAL_chiave");

			entity.ToTable("PSC_QUAL");

			entity.Property(e => e.TSoggetto)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("T_SOGGETTO");
			entity.Property(e => e.CSoggetto)
				.HasMaxLength(8)
				.IsUnicode(false)
				.HasColumnName("C_SOGGETTO");
			entity.Property(e => e.TindProgressivo)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("TIND_PROGRESSIVO");
			entity.Property(e => e.CRisorsa)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("C_RISORSA");
			entity.Property(e => e.CDitta)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("C_DITTA");
			entity.Property(e => e.DtIns)
				.HasColumnType("datetime")
				.HasColumnName("DT_INS");
			entity.Property(e => e.DtUm)
				.HasColumnType("datetime")
				.HasColumnName("DT_UM");
			entity.Property(e => e.Grpcdl)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("GRPCDL");
			entity.Property(e => e.UtenteIns)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("UTENTE_INS");
			entity.Property(e => e.UtenteUm)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("UTENTE_UM");
		});

		modelBuilder.Entity<VsPpCommAperteXCli>(entity =>
		{
			entity
				.HasNoKey()
				.ToView("vs_pp_comm_aperte_x_cli");

			entity.Property(e => e.AcliRagSoc1)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("ACLI_RAG_SOC_1");
			entity.Property(e => e.CCliRag)
				.HasMaxLength(131)
				.IsUnicode(false)
				.HasColumnName("C_CLI_RAG");
			entity.Property(e => e.CommDescDd)
				.HasMaxLength(2093)
				.IsUnicode(false)
				.HasColumnName("COMM_DESC_DD");
			entity.Property(e => e.DescrizioneRidotta)
				.HasMaxLength(20)
				.IsUnicode(false)
				.HasColumnName("DESCRIZIONE_RIDOTTA");
			entity.Property(e => e.OrpbADoc)
				.HasColumnType("numeric(4, 0)")
				.HasColumnName("ORPB_A_DOC");
			entity.Property(e => e.OrpbNDoc)
				.HasColumnType("numeric(8, 0)")
				.HasColumnName("ORPB_N_DOC");
			entity.Property(e => e.OrpbPrfDoc)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("ORPB_PRF_DOC");
			entity.Property(e => e.OrpbTstDoc)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("ORPB_TST_DOC");
			entity.Property(e => e.TbcpCCli)
				.HasMaxLength(8)
				.IsUnicode(false)
				.HasColumnName("TBCP_C_CLI");
			entity.Property(e => e.TbcpId)
				.HasColumnType("numeric(12, 0)")
				.HasColumnName("TBCP_ID");
			entity.Property(e => e.TbcpM1Project)
				.HasMaxLength(20)
				.IsUnicode(false)
				.HasColumnName("TBCP_M1_PROJECT");
			entity.Property(e => e.TbcpOffPrev)
				.HasMaxLength(20)
				.IsUnicode(false)
				.HasColumnName("TBCP_OFF_PREV");
			entity.Property(e => e.TbcpProjectManager)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("TBCP_PROJECT_MANAGER");
			entity.Property(e => e.TbcpRifCliente)
				.HasMaxLength(300)
				.IsUnicode(false)
				.HasColumnName("TBCP_RIF_CLIENTE");
			entity.Property(e => e.Usr1Desc)
				.HasMaxLength(160)
				.IsUnicode(false)
				.HasColumnName("USR1_DESC");
		});

        modelBuilder.Entity<VsPpCalendarRis>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vs_pp_calendar_ris", "dba");

            entity.Property(e => e.CrrpDtt)
                .HasColumnType("date")
                .HasColumnName("crrp_dtt");
            entity.Property(e => e.CrrpDttW).HasColumnName("crrp_dtt_w");
            entity.Property(e => e.CrrpHhTot)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("crrp_hh_tot");
            entity.Property(e => e.CrrpMaccCMatricola)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("crrp_macc_c_matricola");
        });

		modelBuilder.Entity<Psc001a>(entity =>
		{
			entity.HasKey(e => new { e.Id, e.CDitta }).HasName("PSC_001A_chiave");

			entity.ToTable("PSC_001A");

			entity.Property(e => e.Id)
				.ValueGeneratedOnAdd()
				.HasColumnType("numeric(12, 0)")
				.HasColumnName("ID");
			entity.Property(e => e.CDitta)
				.HasMaxLength(2)
				.IsUnicode(false)
				.HasColumnName("C_DITTA");
			entity.Property(e => e.AttivitaSvolta)
				.HasMaxLength(2000)
				.IsUnicode(false)
				.HasColumnName("ATTIVITA_SVOLTA");
			entity.Property(e => e.CCliFatt)
				.HasMaxLength(8)
				.IsUnicode(false)
				.HasColumnName("C_CLI_FATT");
			entity.Property(e => e.CCliInt)
				.HasMaxLength(8)
				.IsUnicode(false)
				.HasColumnName("C_CLI_INT");
			entity.Property(e => e.CNazioneCliFatt)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("C_NAZIONE_CLI_FATT");
			entity.Property(e => e.CNazioneCliInt)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("C_NAZIONE_CLI_INT");
			entity.Property(e => e.CRespInt)
				.HasMaxLength(8)
				.IsUnicode(false)
				.HasColumnName("C_RESP_INT");
			entity.Property(e => e.CapCliFatt)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("CAP_CLI_FATT");
			entity.Property(e => e.CapCliInt)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("CAP_CLI_INT");
			entity.Property(e => e.CittaCliFatt)
				.HasMaxLength(60)
				.IsUnicode(false)
				.HasColumnName("CITTA_CLI_FATT");
			entity.Property(e => e.CittaCliInt)
				.HasMaxLength(60)
				.IsUnicode(false)
				.HasColumnName("CITTA_CLI_INT");
			entity.Property(e => e.DtFatt)
				.HasColumnType("datetime")
				.HasColumnName("DT_FATT");
			entity.Property(e => e.DtIns)
				.HasColumnType("datetime")
				.HasColumnName("DT_INS");
			entity.Property(e => e.DtIntervento)
				.HasColumnType("datetime")
				.HasColumnName("DT_INTERVENTO");
			entity.Property(e => e.DtUm)
				.HasColumnType("datetime")
				.HasColumnName("DT_UM");
			entity.Property(e => e.FlgFatt)
				.HasMaxLength(1)
				.IsUnicode(false)
				.HasColumnName("FLG_FATT");
			entity.Property(e => e.HhIntFatt)
				.HasColumnType("numeric(6, 2)")
				.HasColumnName("HH_INT_FATT");
			entity.Property(e => e.HhIntTot)
				.HasColumnType("numeric(6, 2)")
				.HasColumnName("HH_INT_TOT");
			entity.Property(e => e.HhInterruzione)
				.HasColumnType("numeric(6, 2)")
				.HasColumnName("HH_INTERRUZIONE");
			entity.Property(e => e.HhViaggio)
				.HasColumnType("numeric(6, 2)")
				.HasColumnName("HH_VIAGGIO");
			entity.Property(e => e.Km)
				.HasColumnType("numeric(6, 2)")
				.HasColumnName("KM");
			entity.Property(e => e.MotivoIntervento)
				.HasMaxLength(60)
				.IsUnicode(false)
				.HasColumnName("MOTIVO_INTERVENTO");
			entity.Property(e => e.NFatt)
				.HasColumnType("numeric(8, 0)")
				.HasColumnName("N_FATT");
			entity.Property(e => e.NomeRespInt)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("NOME_RESP_INT");
			entity.Property(e => e.OraFine)
				.HasColumnType("datetime")
				.HasColumnName("ORA_FINE");
			entity.Property(e => e.OraInizio)
				.HasColumnType("datetime")
				.HasColumnName("ORA_INIZIO");
			entity.Property(e => e.ProvinciaCliFatt)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("PROVINCIA_CLI_FATT");
			entity.Property(e => e.ProvinciaCliInt)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("PROVINCIA_CLI_INT");
			entity.Property(e => e.QualRespInt)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("QUAL_RESP_INT");
			entity.Property(e => e.RagSocCliFatt)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("RAG_SOC_CLI_FATT");
			entity.Property(e => e.RagSocCliInt)
				.HasMaxLength(120)
				.IsUnicode(false)
				.HasColumnName("RAG_SOC_CLI_INT");
			entity.Property(e => e.RifCliente)
				.HasMaxLength(300)
				.IsUnicode(false)
				.HasColumnName("RIF_CLIENTE");
			entity.Property(e => e.RifInterno)
				.HasMaxLength(60)
				.IsUnicode(false)
				.HasColumnName("RIF_INTERNO");
			entity.Property(e => e.TFatt)
				.HasMaxLength(3)
				.IsUnicode(false)
				.HasColumnName("T_FATT");
			entity.Property(e => e.TInt)
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("T_INT");
			entity.Property(e => e.UtenteIns)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("UTENTE_INS");
			entity.Property(e => e.UtenteUm)
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("UTENTE_UM");
			entity.Property(e => e.ViaCliFatt)
				.HasMaxLength(60)
				.IsUnicode(false)
				.HasColumnName("VIA_CLI_FATT");
			entity.Property(e => e.ViaCliInt)
				.HasMaxLength(60)
				.IsUnicode(false)
				.HasColumnName("VIA_CLI_INT");
		});

		OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
