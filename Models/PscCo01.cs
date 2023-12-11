using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class PscCo01
{
    public string CDitta { get; set; } = null!;

    public decimal IdDoc { get; set; }

    public decimal Grpcdl { get; set; }

    public decimal? HhAcq { get; set; }

    public string? TFatt { get; set; }

    public string? Note { get; set; }

    public string? UtenteIns { get; set; }

    public DateTime? DtIns { get; set; }

    public string? UtenteUm { get; set; }

    public DateTime? DtUm { get; set; }

    public virtual ICollection<PscCo03> PscCo03s { get; set; } = new List<PscCo03>();
}
