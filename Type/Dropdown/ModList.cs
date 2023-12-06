using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mep01Web.Type.Dropdown
{
    public class ModList
    {
        public List<SelectListItem> Moduli;

        public ModList()
        {
            Moduli = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "",
                    Value = ""
                }
            };
        }
    }
}
