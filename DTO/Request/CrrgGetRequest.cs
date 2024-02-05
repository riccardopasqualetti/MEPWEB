using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mep01Web.DTO.Response;
using Mep01Web.Type.Dropdown;

namespace Mep01Web.DTO.Request
{
    public class CrrgGetRequest // : IValidatableObject
    {
        public CrrgGetRequest()
        {            
        }
        public CrrgGetRequest(decimal crrgSCRL)
        {
            CrrgCSrl = crrgSCRL;
        }
        // Id per Update e Delete
        public decimal CrrgCSrl { get; set; }
        public string CrrgCDitta { get; set; }

    }
}
