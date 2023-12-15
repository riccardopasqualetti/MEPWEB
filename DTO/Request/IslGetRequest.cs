using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mep01Web.DTO.Response;
using Mep01Web.Type.Dropdown;
using Mep01Web.Models;
using Mep01Web.Models.Views;

namespace Mep01Web.DTO.Request
{
    public class IslGetRequest : IValidatableObject
    {
        public IslGetRequest()
        {            
        }

        // Risorsa
        [DisplayName("Risorsa")]
        public string FilterIslCRis { get; set; }        
        // Data
        public DateTime FilterIslDttStart { get; set; }
        public DateTime FilterIslDttEnd { get; set; }

        public IEnumerable<VsPpMonitorIsl> IslList { get; set; }

        public string FilterHidden { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }

        public static implicit operator IslGetRequest(Task<IslGetRequest> v)
        {
            throw new NotImplementedException();
        }
    }
}
