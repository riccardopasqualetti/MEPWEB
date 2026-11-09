using System;

namespace Mep01Web.Models;

/// <summary>
/// [231009] Account Teamwork collegato a un utente MEPWEB (login OAuth per utente).
/// TwAccessToken è cifrato con ASP.NET Data Protection.
/// </summary>
public partial class FlussoUsr1Tw
{
    /// <summary>Utente MEPWEB (flusso_usr1.usr1_c_srl).</summary>
    public decimal Usr1CSrl { get; set; }

    public long TwUserId { get; set; }

    public string TwApiEndpoint { get; set; } = null!;

    public string TwAccessToken { get; set; } = null!;

    public DateTime? DtIns { get; set; }

    public DateTime? DtUm { get; set; }
}
