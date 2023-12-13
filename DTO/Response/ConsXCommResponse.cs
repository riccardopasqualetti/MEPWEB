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
        public decimal?  C_PGM { get; set; }
        public decimal?  C_SOA { get; set; }
        public decimal?  C_PJM { get; set; }
        public decimal?  C_BUC { get; set; }
        public decimal?  C_SYD { get; set; }
        public decimal?  C_GEN { get; set; }
        public decimal?  CrrgPgm2 { get; set; }
        public decimal?  CrrgSoa3 { get; set; }
        public decimal?  CrrgPjm4 { get; set; }
        public decimal?  CrrgBuc8 { get; set; }
        public decimal?  CrrgSys7 { get; set; }
        public decimal?  CrrgGen { get; set; }
    }
}