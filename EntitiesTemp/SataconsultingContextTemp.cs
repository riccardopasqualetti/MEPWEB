using System;
using System.Collections.Generic;
using MepWeb.EntitiesTemp;
using Microsoft.EntityFrameworkCore;

namespace MepWeb.Infrastructure;

public partial class SataconsultingContextTemp : DbContext
{
    public SataconsultingContextTemp()
    {
    }

    public SataconsultingContextTemp(DbContextOptions<SataconsultingContextTemp> options)
        : base(options)
    {
    }

    public virtual DbSet<VsPpMonitorIsl> VsPpMonitorIsls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Main");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
