using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class PscCo03
{
    public decimal Id { get; set; }

    public string CDitta { get; set; } = null!;

    public decimal IdDoc { get; set; }

    public decimal Grpcdl { get; set; }

    public string? RifOfferta { get; set; }

    public DateTime? DtRicarica { get; set; }

    public decimal? HhAcq { get; set; }

    public string? Note { get; set; }

    public string? UtenteIns { get; set; }

    public DateTime? DtIns { get; set; }

    public string? UtenteUm { get; set; }

    public DateTime? DtUm { get; set; }

    public virtual PscCo01 PscCo01 { get; set; } = null!;
}
