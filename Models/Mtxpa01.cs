using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class Mtxpa01
{
    public decimal Xpa01N00Id { get; set; }

    public string Xpa01Cu0Cmaatt { get; set; } = null!;

    public string Xpa01C00Cditta { get; set; } = null!;

    public string? Xpa01C00CtabellaOr { get; set; }

    public string Xpa01CutSetcpaag { get; set; } = null!;

    public string? Xpa01C00UtenteIns { get; set; }

    public DateTime? Xpa01Dt0DtIns { get; set; }

    public string? Xpa01C00UtenteUm { get; set; }

    public DateTime? Xpa01Dt0DtUm { get; set; }

    public string? Xpa01C00Cord { get; set; }
}
