using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mep01Web.Type.Dropdown
{
    public class CrrgGroupByList
    {
        public List<SelectListItem> Lista;

        public CrrgGroupByList()
        {
            Lista = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Data",
                    Value = "D"
                },
                new SelectListItem
                {
                    Text = "ISL",
                    Value = "I"
                },
                new SelectListItem
                {
                    Text = "Commessa",
                    Value = "C"
                },
            };
        }
    }
}
