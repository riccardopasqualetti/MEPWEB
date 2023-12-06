using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mep01Web.Type.Dropdown
{
    public class CrrgCCausList
    {
        public List<SelectListItem> Lista;

        public CrrgCCausList()
        {
            Lista = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "CORI",
                    Value = "CORI"
                },
                new SelectListItem
                {
                    Text = "ANFU",
                    Value = "ANFU"
                },
                new SelectListItem
                {
                    Text = "SVIL",
                    Value = "SVIL"
                },
                new SelectListItem
                {
                    Text = "DELI",
                    Value = "DELI"
                }
            };
        }
    }
}
