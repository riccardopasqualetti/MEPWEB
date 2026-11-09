using System.Globalization;
using System.Text.Json;

namespace MepWeb.Service
{
    /// <summary>[231009] Lettura tollerante delle risposte JSON di Teamwork (campi mancanti = null).</summary>
    public static class TeamworkJson
    {
        public static JsonElement? Naviga(JsonElement e, params string[] percorso)
        {
            foreach (var nome in percorso)
            {
                if (e.ValueKind != JsonValueKind.Object || !e.TryGetProperty(nome, out var figlio))
                {
                    return null;
                }
                e = figlio;
            }
            return e;
        }

        public static string? Stringa(JsonElement e, params string[] percorso)
        {
            var v = Naviga(e, percorso);
            return v?.ValueKind switch
            {
                JsonValueKind.String => v.Value.GetString(),
                JsonValueKind.Number => v.Value.GetRawText(),
                _ => null
            };
        }

        public static long? Long(JsonElement e, params string[] percorso)
        {
            var v = Naviga(e, percorso);
            if (v?.ValueKind == JsonValueKind.Number && v.Value.TryGetInt64(out var n))
            {
                return n;
            }
            if (v?.ValueKind == JsonValueKind.String && long.TryParse(v.Value.GetString(), out var s))
            {
                return s;
            }
            return null;
        }

        public static bool Bool(JsonElement e, params string[] percorso)
        {
            var v = Naviga(e, percorso);
            return v?.ValueKind == JsonValueKind.True;
        }

        /// <summary>Data ISO 8601 di Teamwork (es. 2026-11-05T09:20:00Z) come DateTime UTC.</summary>
        public static DateTime? DataUtc(JsonElement e, params string[] percorso)
        {
            var s = Stringa(e, percorso);
            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }
            return DateTime.TryParse(s, CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out var d) ? d : null;
        }

        public static string ElencoCampi(JsonElement e) =>
            e.ValueKind == JsonValueKind.Object ? string.Join(", ", e.EnumerateObject().Select(p => p.Name)) : e.ValueKind.ToString();
    }
}
