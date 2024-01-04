namespace MepWeb.DTO.Request
{
    public class RegistroRicaricheCreateRequest
    {
        public decimal Id { get; set; }
        public decimal IdDocumento { get; set; }
        //public string DescrizioneQualifica { get; set; }
        public decimal? Qualifica { get; set; }
        public string? RiferimentoOfferta { get; set; }
        public DateTime? DataRicarica { get; set; }
        public decimal? OreAcquistate { get; set; }
        public string? Note { get; set; }
    }
}
