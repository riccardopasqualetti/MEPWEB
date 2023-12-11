using System;
using System.Collections.Generic;

namespace Mep01Web.Models.Views;

public partial class Mvxzz12
{
    public decimal Id { get; set; }

    public string Cprfc { get; set; } = null!;

    public decimal Cod { get; set; }

    public decimal Lingua { get; set; }

    public string? DescrizioneRidotta { get; set; }

    public string? Descrizione { get; set; }

    public string? Note { get; set; }

    public string Cditta { get; set; } = null!;

    public string? CtabellaOr { get; set; }

    public string? UtenteIns { get; set; }

    public DateTime? DtIns { get; set; }

    public string? UtenteUm { get; set; }

    public DateTime? DtUm { get; set; }
}
