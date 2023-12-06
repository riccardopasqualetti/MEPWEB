using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class Mtxge22
{
    public decimal Xge22N00Id { get; set; }

    public string? Xge22C00Ctabella { get; set; }

    public decimal Xge22N00IdDato { get; set; }

    public decimal? Xge22N0dIdxzz12 { get; set; }

    public string Xge22C00Cditta { get; set; } = null!;

    public string? Xge22C00CtabellaOr { get; set; }

    public string? Xge22C00UtenteIns { get; set; }

    public DateTime? Xge22Dt0DtIns { get; set; }

    public string? Xge22C00UtenteUm { get; set; }

    public DateTime? Xge22Dt0DtUm { get; set; }

    public virtual Mtxzz12? Xge22 { get; set; }
}
