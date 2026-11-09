using System;

namespace Mep01Web.Models;

/// <summary>
/// [231009] Collegamento tra una registrazione di tempo Teamwork e il consuntivo MEP (flusso_crrg).
/// Le proprietà Tw* sono la fotografia dei valori Teamwork all'import o all'ultimo allineamento;
/// le date sono in UTC, come restituite dalle API Teamwork.
/// </summary>
public partial class FlussoCrrgTw
{
    public long TwTimelogId { get; set; }

    public decimal CrrgCSrl { get; set; }

    public long TwTaskId { get; set; }

    public int TwMinutes { get; set; }

    public DateTime TwDate { get; set; }

    public string? TwCommessa { get; set; }

    public string? TwDescrizioneReg { get; set; }

    public DateTime TwUpdatedAt { get; set; }

    public string? Sigla { get; set; }
}
