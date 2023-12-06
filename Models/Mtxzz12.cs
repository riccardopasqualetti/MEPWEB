using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class Mtxzz12
{
    public decimal Xzz12N00Id { get; set; }

    public string Xzz12C00Cprfc { get; set; } = null!;

    public decimal Xzz12N01Cod { get; set; }

    public string Xzz12C00Cditta { get; set; } = null!;

    public string? Xzz12C00CtabellaOr { get; set; }

    public string? Xzz12C00UtenteIns { get; set; }

    public DateTime? Xzz12Dt0DtIns { get; set; }

    public string? Xzz12C00UtenteUm { get; set; }

    public DateTime? Xzz12Dt0DtUm { get; set; }

    public virtual ICollection<Mtxge22> Mtxge22s { get; set; } = new List<Mtxge22>();
}
