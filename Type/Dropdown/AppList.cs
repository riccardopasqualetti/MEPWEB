using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mep01Web.Type.Dropdown
{
    public class AppList
    {
        public List<SelectListItem> Applicativi;

        public AppList()
        {
			Applicativi = new List<SelectListItem>
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
