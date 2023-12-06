using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class FlussoUsr1
{
    public decimal Usr1CSrl { get; set; }

    public string? Usr1CUtente { get; set; }

    public string Usr1CDitta { get; set; } = null!;

    public string? Usr1Password { get; set; }

    public string? Usr1Desc { get; set; }

    public string? Usr1TUtente { get; set; }

    public DateTime? Usr1DtIns { get; set; }

    public string? Usr1UtenteIns { get; set; }

    public DateTime? Usr1DtUm { get; set; }

    public string? Usr1UtenteUm { get; set; }

    public string? Usr1NTel { get; set; }

    public string? Usr1NFax { get; set; }

    public string? Usr1EMail { get; set; }

    public decimal Usr1CLingua { get; set; }

    public string Usr1GestEmail { get; set; } = null!;

    public DateTime? Usr1DtRifPassw { get; set; }

    public decimal Usr1GgModPassw { get; set; }

    public string Usr1Username { get; set; } = null!;

    public string? Usr1Token { get; set; }

    public string? Usr1IdSicurezza { get; set; }

    public string? Usr1ParametriWeb { get; set; }

    public string? Usr1TAccesso { get; set; }

    public string? Usr1FirmaEmail { get; set; }

    public string? Usr1UserManagerM1 { get; set; }

    public string? Usr1PwdManagerM1 { get; set; }

    public decimal Usr1MmTimeout { get; set; }

    public decimal Usr1Ctrl { get; set; }

    public decimal Usr1MdiBackcolor { get; set; }

    public decimal Usr1Backcolor { get; set; }

    public string? Usr1ImgUtente { get; set; }

    public string? Usr1ImgMdi { get; set; }

    public string? Usr1PersonalSettings { get; set; }

    public string? Usr1InfoRelease { get; set; }

    public string? Usr1FlgEtichetteEstere { get; set; }

    public string? Usr1Theme { get; set; }

    public string? Usr1CRisorsa { get; set; }

    public string? Usr1WindowsUser { get; set; }

    public decimal Usr1FlgWauth { get; set; }

    public string? Usr1AttivaCockpit { get; set; }

    public string? Usr1FlgCessato { get; set; }
}
