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

    /// <summary>[231009] Azioni della griglia "Da allineare".</summary>
    public enum TwAzioneAllineamento
    {
        /// <summary>Modificata in Teamwork: aggiorna il consuntivo (ore, data, note, commessa) o solo la fotografia.</summary>
        Aggiorna,
        /// <summary>Elimina solo il collegamento: il consuntivo resta e torna modificabile.</summary>
        Scollega,
        /// <summary>Cancellata in Teamwork: elimina consuntivo e collegamento.</summary>
        Elimina
    }

    /// <summary>[231009] Voce di una tendina (operazione, macroattività).</summary>
    public class TwOpzione
    {
        public string Valore { get; init; } = "";
        public string Testo { get; init; } = "";
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
        /// <summary>ISL con offerta o commessa di tipo B: la macroattività deve essere Verbale (3).</summary>
        public bool VerbaleObbligatorio { get; init; }
        /// <summary>Motivo per cui la registrazione non è importabile (commessa non trovata, chiusa, ISL ambigua...).</summary>
        public string? Errore { get; init; }

        public string Codice => TstDoc == null ? "" : $"{TstDoc}-{PrfDoc}-{ADoc:0}-{NDoc:0}";

        /// <summary>Su commessa (senza ISL) la causale è sempre CORI; su ISL CORI non è ammessa.</summary>
        public bool CausaleFissaCori => Isl == null;
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
        /// <summary>Note del consuntivo collegato (solo righe collegate a un consuntivo esistente).</summary>
        public string? NoteConsuntivo { get; set; }
        /// <summary>Cosa è cambiato su Teamwork rispetto all'import (solo per le registrazioni collegate).</summary>
        public List<string> Differenze { get; init; } = new();
        /// <summary>Modificata in Teamwork, ma senza effetti sul consuntivo (orario, fatturabile...): basta riallineare la fotografia.</summary>
        public bool SoloRiallineamento { get; init; }
        /// <summary>Null se importabile, altrimenti il motivo.</summary>
        public string? NonImportabile { get; init; }

        // Valori proposti per l'import (solo righe nuove e importabili)
        public string? PropostaNTOper { get; set; }
        public string? PropostaCausale { get; set; }
        public string? PropostaMacroattivita { get; set; }
        public string? PropostaNote { get; set; }
        /// <summary>Consuntivo inserito a mano che potrebbe essere la stessa attività (stessa data, commessa e durata simile).</summary>
        public string? AvvisoDoppione { get; set; }

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

        /// <summary>Operazioni disponibili per codice commessa (TST-P-AAAA-N), valore "NOper-TOper".</summary>
        public Dictionary<string, List<TwOpzione>> OperazioniPerCommessa { get; set; } = new();
        public List<TwOpzione> Macroattivita { get; set; } = new();
        public List<TwOpzione> CausaliIsl { get; set; } = new();
    }

    /// <summary>[231009] Consuntivo creato dall'import.</summary>
    public class TwImportato
    {
        public string Nome { get; init; } = "";
        public decimal CrrgCSrl { get; init; }
    }

    /// <summary>[231009] Esito dell'import: una riga per ogni registrazione selezionata.</summary>
    public class TwImportEsito
    {
        public List<TwImportato> Importate { get; set; } = new();
        public List<string> Errori { get; set; } = new();
    }
}
