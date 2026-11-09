namespace MepWeb.DTO.Response
{
    /// <summary>[231009] Stato di una registrazione Teamwork rispetto ai consuntivi MEP (ricavato, non salvato).</summary>
    public enum TwStatoRegistrazione
    {
        Nuova,
        Importata,
        ModificataInTeamwork,
        CancellataInTeamwork,
        ConsuntivoEliminatoInMep
    }

    /// <summary>[231009] Commessa MEP ricavata dal campo Commessa/ISL del task Teamwork.</summary>
    public class TwCommessaMep
    {
        public string? Isl { get; init; }
        public string? TstDoc { get; init; }
        public string? PrfDoc { get; init; }
        public decimal ADoc { get; init; }
        public decimal NDoc { get; init; }
        public string? Descrizione { get; init; }
        public bool Aperta { get; init; }
        /// <summary>Motivo per cui la registrazione non è importabile (commessa non trovata, chiusa, ISL ambigua...).</summary>
        public string? Errore { get; init; }

        public string Codice => TstDoc == null ? "" : $"{TstDoc}-{PrfDoc}-{ADoc:0}-{NDoc:0}";
    }

    /// <summary>[231009] Riga dell'anteprima di import.</summary>
    public class TwRigaAnteprima
    {
        public long TimelogId { get; init; }
        public DateTime DataOra { get; init; }   // ora locale
        public int Minuti { get; init; }
        public string? Descrizione { get; init; }
        public long? TaskId { get; init; }
        public string? TaskNome { get; init; }
        public string? CommessaTeamwork { get; init; }
        public TwCommessaMep? CommessaMep { get; init; }
        public TwStatoRegistrazione Stato { get; init; }
        public decimal? CrrgCSrl { get; init; }
        /// <summary>Cosa è cambiato su Teamwork rispetto all'import (solo per le registrazioni collegate).</summary>
        public List<string> Differenze { get; init; } = new();
        /// <summary>Null se importabile, altrimenti il motivo.</summary>
        public string? NonImportabile { get; init; }

        public string Durata => $"{Minuti / 60}:{Minuti % 60:00}";
    }

    /// <summary>[231009] Anteprima delle registrazioni Teamwork di un periodo per l'utente collegato.</summary>
    public class TwAnteprimaResponse
    {
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
        /// <summary>Registrazioni del periodo non ancora collegate a un consuntivo.</summary>
        public List<TwRigaAnteprima> Nuove { get; set; } = new();
        /// <summary>Registrazioni collegate e allineate.</summary>
        public List<TwRigaAnteprima> Importate { get; set; } = new();
        /// <summary>Registrazioni collegate che su Teamwork sono cambiate o sono state cancellate, o il cui consuntivo non c'è più.</summary>
        public List<TwRigaAnteprima> DaAllineare { get; set; } = new();
        /// <summary>Avvisi tecnici (es. campi personalizzati non restituiti da Teamwork).</summary>
        public List<string> Avvisi { get; set; } = new();
    }
}
