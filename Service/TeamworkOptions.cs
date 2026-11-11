namespace MepWeb.Service
{
    /// <summary>
    /// [231009] Configurazione del login Teamwork per utente (sezione "Teamwork" di appsettings).
    /// ClientSecret arriva da user-secrets (sviluppo) o dalla variabile d'ambiente Teamwork__ClientSecret (server).
    /// </summary>
    public class TeamworkOptions
    {
        public const string Sezione = "Teamwork";

        public string ClientId { get; set; } = "";
        public string ClientSecret { get; set; } = "";
        public string LoginUrl { get; set; } = "";
        public string TokenUrl { get; set; } = "";
        public string UserInfoUrl { get; set; } = "";

        /// <summary>Se vuoto, l'indirizzo di callback si calcola dalla richiesta corrente.</summary>
        public string RedirectUri { get; set; } = "";

        /// <summary>Id del campo personalizzato "Commessa/ISL" dei task Teamwork.</summary>
        public long CampoCommessaTaskId { get; set; } = 2956;

        /// <summary>
        /// Pagina del task su Teamwork, sul sito di <see cref="LoginUrl"/> (es. https://sataconsulting.teamwork.com/app/tasks/123).
        /// Null se LoginUrl non è configurato.
        /// </summary>
        public string? UrlTask(long taskId) =>
            Uri.TryCreate(LoginUrl, UriKind.Absolute, out var sito) ? $"{sito.GetLeftPart(UriPartial.Authority)}/app/tasks/{taskId}" : null;
    }
}
