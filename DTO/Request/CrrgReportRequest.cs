using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mep01Web.DTO.Response;
using Mep01Web.Type.Dropdown;
using Mep01Web.Models;

namespace Mep01Web.DTO.Request
{
    public class CrrgReportRequest : IValidatableObject
    {
        public CrrgReportRequest()
        {            
        }

        // Risorsa
        [DisplayName("Risorsa")]
        public string FilterCrrgCRis { get; set; }        
        // Data
        public DateTime FilterCrrgDttStart { get; set; }
        public DateTime FilterCrrgDttEnd { get; set; }
        public string FilterRifCliente { get; set; }
        public string FilterCommCode { get; set; }

        public string FilterGroup { get; set; }

        public CrrgGroupByList FilterGroupList { get; set; } = new CrrgGroupByList();

        public IEnumerable<FlussoCrrg> CrrgList { get; set; }

        public string FilterHidden { get; set; }
        public string FilterMantain { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }

        public static implicit operator CrrgReportRequest(Task<CrrgReportRequest> v)
        {
            throw new NotImplementedException();
        }
    }
}
