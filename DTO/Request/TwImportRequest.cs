namespace MepWeb.DTO.Request
{
    /// <summary>
    /// [231009] Registrazioni Teamwork da importare come consuntivi, con le scelte dell'utente.
    /// Ore, data, task e commessa non arrivano dal form: si rileggono da Teamwork al momento dell'import.
    /// </summary>
    public class TwImportRequest
    {
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
        public List<TwImportRiga> Righe { get; set; } = new();
    }

    public class TwImportRiga
    {
        public long TimelogId { get; set; }
        public bool Seleziona { get; set; }
        /// <summary>Operazione nel formato "NOper-TOper" (es. 50-M001).</summary>
        public string? NTOper { get; set; }
        public string? Causale { get; set; }
        public string? Macroattivita { get; set; }
        public string? Note { get; set; }
    }
}
