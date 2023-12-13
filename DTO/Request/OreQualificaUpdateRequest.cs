namespace MepWeb.DTO.Request
{
    public class OreQualificaUpdateRequest
    {
        public decimal IdDocumento { get; set; }

        public decimal Qualifica { get; set; }

        public decimal? OreAcquistate { get; set; }

        public string? TipoFatturazione { get; set; }

        public string? Note { get; set; }
    }
}
