using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mep01Web.Type.Dropdown
{
    public class CrrgCmaattList
    {
        public List<SelectListItem> Lista;

        public CrrgCmaattList()
        {
            Lista = new List<SelectListItem>
            {
            };
        }
    }
}
