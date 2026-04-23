using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MepWeb.Controllers
{
    /// <summary>
    /// Controller API per ottenere informazioni di build e commit dell'applicazione.
    /// Non richiede autorizzazione.
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class BuildController : ControllerBase
    {
        /// <summary>
        /// Restituisce le informazioni di build, versione e commit dell'applicazione.
        /// </summary>
        /// <returns>Oggetto BuildResponse con i dettagli della build</returns>
        /// <response code="200">Informazioni di build restituite con successo</response>
        [HttpGet("info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BuildResponse> GetBuildInfo()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var buildResponse = GetAssemblyInformation(assembly);
            return Ok(buildResponse);
        }

        /// <summary>
        /// Estrae le informazioni di build e commit dall'assembly.
        /// </summary>
        /// <param name="assembly">L'assembly da cui estrarre le informazioni</param>
        /// <returns>Oggetto BuildResponse con i dati estratti</returns>
        private static BuildResponse GetAssemblyInformation(Assembly assembly)
        {
            var name = assembly.GetName().Name;
            var assemblyVersion = assembly.GetName().Version?.ToString();
            var info = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            string? buildVersion = null;
            string? commitSha = null;
            string? commitDate = null;

            if (!string.IsNullOrWhiteSpace(info))
            {
                // Format: 1.0.0+sha;date=isodate
                var plus = info.Split('+', 2);
                buildVersion = string.IsNullOrWhiteSpace(plus[0]) ? null : plus[0];

                if (plus.Length > 1)
                {
                    var parts = plus[1].Split(';', StringSplitOptions.RemoveEmptyEntries);

                    commitSha = parts.FirstOrDefault();

                    commitDate = parts
                        .FirstOrDefault(p => p.StartsWith("date=", StringComparison.OrdinalIgnoreCase))
                        ?.Substring("date=".Length);
                }
            }

            return new BuildResponse
            {
                Name = name,
                AssemblyVersion = assemblyVersion,
                InformationalVersion = info,
                CommitDate = commitDate,
                CommitSha = commitSha,
                BuildVersion = buildVersion
            };
        }
    }

    /// <summary>
    /// Informazioni di build e versione dell'applicazione.
    /// </summary>
    public class BuildResponse
    {
        /// <summary>
        /// Nome dell'assembly/applicazione.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Versione dell'assembly nel formato standard .NET (es: 1.0.0.0).
        /// </summary>
        public string? AssemblyVersion { get; set; }

        /// <summary>
        /// Versione informazionale completa che include hash del commit e data.
        /// Formato: {BuildVersion}+{CommitSha};date={CommitDate}
        /// Esempio: 1.0.0+abc123def;date=2024-01-15T10:30:45+01:00
        /// </summary>
        public string? InformationalVersion { get; set; }

        /// <summary>
        /// Data ISO 8601 del commit (estratta dalla InformationalVersion).
        /// </summary>
        public string? CommitDate { get; set; }

        /// <summary>
        /// Hash del commit Git (estratto dalla InformationalVersion).
        /// </summary>
        public string? CommitSha { get; set; }

        /// <summary>
        /// Versione di build (estratta dalla InformationalVersion).
        /// </summary>
        public string? BuildVersion { get; set; }
    }
}
