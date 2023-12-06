using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mep01Web.Type.Dropdown
{
    public class AcliList
    {
        public List<SelectListItem> Clienti;

        public AcliList()
        {
            Clienti = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "",
                    Value = "null"
                }
            };
        }
    }
}
