using System;
using System.Collections.Generic;

namespace MepWeb.Models;

public partial class Psc001a
{
    public decimal Id { get; set; }

    public string CDitta { get; set; } = null!;

    public DateTime? DtIntervento { get; set; }

    public string? CRespInt { get; set; }

    public string? NomeRespInt { get; set; }

    public string? QualRespInt { get; set; }

    public string? CCliFatt { get; set; }

    public string? RagSocCliFatt { get; set; }

    public string? ViaCliFatt { get; set; }

    public string? CapCliFatt { get; set; }

    public string? CittaCliFatt { get; set; }

    public string? ProvinciaCliFatt { get; set; }

    public string? CNazioneCliFatt { get; set; }

    public string? CCliInt { get; set; }

    public string? RagSocCliInt { get; set; }

    public string? ViaCliInt { get; set; }

    public string? CapCliInt { get; set; }

    public string? CittaCliInt { get; set; }

    public string? ProvinciaCliInt { get; set; }

    public string? CNazioneCliInt { get; set; }

    public DateTime? OraInizio { get; set; }

    public DateTime? OraFine { get; set; }

    public decimal HhInterruzione { get; set; }

    public decimal HhViaggio { get; set; }

    public decimal Km { get; set; }

    public string? RifCliente { get; set; }

    public string? RifInterno { get; set; }

    public string? MotivoIntervento { get; set; }

    public string? AttivitaSvolta { get; set; }

    public decimal HhIntTot { get; set; }

    public decimal HhIntFatt { get; set; }

    public string? TInt { get; set; }

    public string? TFatt { get; set; }

    public decimal NFatt { get; set; }

    public DateTime? DtFatt { get; set; }

    public string? FlgFatt { get; set; }

    public string? UtenteIns { get; set; }

    public DateTime? DtIns { get; set; }

    public string? UtenteUm { get; set; }

    public DateTime? DtUm { get; set; }
}
