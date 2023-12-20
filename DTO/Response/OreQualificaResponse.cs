namespace MepWeb.DTO.Response
{
    public class OreQualificaResponse
    {
        public decimal IdDocumento { get; set; }
        public decimal Qualifica { get; set; }
        public string? DescrizioneQualifica { get; set; }
        public decimal? OreAcquistate { get; set; }
        public string? TipoFatturazione { get; set; }
        public string? Note { get; set; }
    }
}
