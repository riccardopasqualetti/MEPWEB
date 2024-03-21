namespace MepWeb.DTO.Response
{
    public class ConsXCommResponse
    {
        public string    TBCP_TST_COMM { get; set; } = null!;
        public string    TBCP_PRF_COMM { get; set; } = null!;
        public decimal   TBCP_A_COMM { get; set; }
        public decimal   TBCP_N_COMM { get; set; }
        public string?   TbcpStatus { get; set; }
        public string?   TBCP_C_CLI { get; set; }
        public string?   ACLI_RAG_SOC_1 { get; set; }
        public string?   TBCP_OFF_PREV { get; set; }
        public string?   TBCP_RIF_CLIENTE { get; set; }
        public decimal   TbcpCarea { get; set; }
        public string?   DESCRIZIONE_RIDOTTA { get; set; }
        public string?   Descrizione { get; set; }
        public string?   TBCP_M1_PROJECT { get; set; }
        public string?   TBCP_DESC { get; set; }
        public string?   TbcpProjectManager { get; set; }
        public string?   USR1_DESC { get; set; }
        public decimal   TbcpId { get; set; }
        public decimal?  HHACQPGM { get; set; }
        public decimal?  HHACQSOA { get; set; }
        public decimal?  HHACQPJM { get; set; }
        public decimal?  HHACQBUC { get; set; }
        public decimal?  HHACQSYD { get; set; }
        public decimal?  HHACQGEN { get; set; }
		public decimal   HHACQGDE { get; set; }
        public string?   TFATTPGM { get; set; }
        public string?   TFATTSOA { get; set; }
        public string?   TFATTPJM { get; set; }
        public string?   TFATTBUC { get; set; }
        public string?   TFATTSYD { get; set; }
        public string?   TFATTGEN { get; set; }
		public string?   TFATTGDE { get; set; }
		//public decimal?  HHCRRGPGM { get; set; }
  //      public decimal?  HHCRRGSOA { get; set; }
  //      public decimal?  HHCRRGPJM { get; set; }
  //      public decimal?  HHCRRGBUC { get; set; }
  //      public decimal?  HHCRRGSYD { get; set; }
  //      public decimal?  HHCRRGGEN { get; set; }
        public decimal   HHCRRGPGMEFF { get; set; }
        public decimal   HHCRRGSOAEFF { get; set; }
        public decimal   HHCRRGPJMEFF { get; set; }
        public decimal   HHCRRGBUCEFF { get; set; }
        public decimal   HHCRRGSYDEFF { get; set; }
        public decimal   HHCRRGGENEFF { get; set; }
		public decimal   HHCRRGPGMEFFNV { get; set; }
		public decimal   HHCRRGSOAEFFNV { get; set; }
		public decimal   HHCRRGPJMEFFNV { get; set; }
		public decimal   HHCRRGBUCEFFNV { get; set; }
		public decimal   HHCRRGSYDEFFNV { get; set; }
        public decimal?  HH001APGM { get; set; }
        public decimal?  HH001ASOA { get; set; }
        public decimal?  HH001APJM { get; set; }
        public decimal?  HH001ABUC { get; set; }
        public decimal?  HH001ASYD { get; set; }
        public string?   Preoccupazione { get; set; }
        public decimal?  Avanzamento { get; set; }
        public string?   Note { get; set; }
    }
}