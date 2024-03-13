namespace MepWeb.DTO.Request
{
    public class TbcpUpdateCampiOpzionaliRequest
    {
        public string TstComm { get; set; }
        public string PrfComm { get; set; }
        public decimal AComm { get; set; }
        public decimal NComm { get; set; }
        public string? Nota { get; set; }
        public string? Preoccupazione { get; set; }
        public decimal? Avanzamento { get; set; }
    }
}
