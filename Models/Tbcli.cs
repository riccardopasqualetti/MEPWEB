using System;
using System.Collections.Generic;

namespace Mep01Web.Models;

public partial class Tbcli
{
    public string CliCod { get; set; } = null!;

    public string CliNom { get; set; } = null!;

    public string CliCog { get; set; } = null!;

    public string? CliInd { get; set; }

    public string? CliCom { get; set; }

    public string? CliCap { get; set; }

    public string? CliTel { get; set; }

    public string? CliCard { get; set; }
}
