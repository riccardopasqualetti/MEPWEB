using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mep01Web.Type.Dropdown
{
    public class NTOperList
    {
        public List<SelectListItem> Operazioni;

        public NTOperList()
        {
            Operazioni = new List<SelectListItem>
            {
            };
        }
    }
}
